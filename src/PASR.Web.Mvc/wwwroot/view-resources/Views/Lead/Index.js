(function ($) {
    //#region variables
    var _leadService = abp.services.app.lead,
        _callService = abp.services.app.call,
        l = abp.localization.getSource('PASR'),
        _$modal = $('#LeadCreateModal'),
        _$editModal = $('#LeadEditModal'),
        _$form = $('#leadCreateForm'),
        _$formEdit = $('#leadEditForm'),
        _$table = $('#leadsTable'),
        _$uTable = $('#UsersTable'),
        _userService = abp.services.app.user,
        _$userInput = _$editModal.find('[name="assignedUserName"]'),
        _$callModal = $('#LeadCallModal'),
        _$callForm = $('#callForm'),
        _$callCountDown = $('#callCountdown'),
        callStart,
        callEnd,
        callLeadId;
    //#endregion    

    var _$leadsTable = _$table.DataTable({
        processing: true,
        serverSide: true,
        ordering: true,
        order: [[5, 'desc']],
        ajax: function (data, callback, settings) {
            var filter = $('#LeadsSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;
            abp.ui.setBusy(_$table);
            _leadService.getAll(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function (data) {
                //console.log(data);
                abp.ui.clearBusy(_$table);
            });
        },
        rowId: 'id',
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$leadsTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column',
                target: -1
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'details-control',
                defaultContent: '<i class="fas fa-chevron-down"></i>',
                orderable: false
            },
            {
                targets: 1,
                data: 'name',
                sortable: true
            },
            {
                targets: 2,
                data: 'lastName',
                sortable: true

            },
            {
                targets: 3,
                data: 'phoneNumber',
                sortable: false

            },
            {
                targets: 4,
                data: 'emailAddress',
                sortable: true

            },
            {
                targets: 5,
                data: 'priority',
                sortable: true,
                render: (data, type, row, meta) => {

                    switch (type) {

                        case "display":
                            return pasr.maps.priorityList[data];

                        case "filter":
                            return pasr.maps.priorityList[data];

                        default:
                            return data;
                    }

                }

            },
            {
                targets: 6,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {

                    if (abp.auth.isGranted('Update.Leads')) {
                        return [
                            `   <button type="button" class="btn btn-sm bg-secondary edit-lead" data-lead-id="${row.id}" data-toggle="modal" data-target="#LeadEditModal">`,
                            `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                            '   </button>',
                            `   <button type="button" class="btn btn-sm bg-danger delete-lead" data-lead-id="${row.id}" data-lead-name="${row.name}">`,
                            `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                            '   </button>',
                        ].join('');
                    } else {
                        return [
                            `   <button type="button" class="btn btn-sm bg-primary call-lead" data-lead-id="${row.id}" data-lead-phone="${row.phoneNumber}" data-lead-name="${row.name}">`,
                            `       <i class="fas fa-phone"></i> ${l('Call')}`,
                            '   </button>'
                        ].join('');
                    }

                }
            },
            {
                targets: 7,
                className: 'control',
                defaultContent: ''
            }
        ]
    });

    $('#leadsTable tbody').on('click', 'td.details-control', function () {

        let tr = $(this).closest('tr');
        let row = _$leadsTable.row(tr);

        if (row.child.isShown()) {

            // This row is already open - close it
            tr.destroyChildTable(row);
            tr.removeClass('shown');
            $(this).find('i').toggleClass('fa-flip-vertical');

        } else {
            // Open this row
            _$callsTable = tr.createChildTable(row);

            // tr.addClass('shown');
            let chevron = $(this).find('i');

            chevron.toggleClass('fa-flip-vertical');

            // This is the table we'll convert into a DataTable
            _$callsTable.DataTable({
                processing: false,
                serverSide: true,
                dom: 't',
                ordering: false,
                sortable: false,
                ajax: function (data, callback, settings) {
                    var filter = filter || {};
                    filter.id = row.id();
                    filter.maxResultCount = data.length;
                    filter.skipCount = data.start;
                    abp.ui.setBusy(_$callsTable);

                    _callService.getAll(filter)
                        .done(function (result) {
                            console.log(result);
                            callback({ data: result.items });
                        })
                        .always(function (data) {
                            console.log(data);
                            abp.ui.clearBusy(_$callsTable);
                        });
                },
                rowId: 'id',
                responsive: {
                    details: {
                        type: 'column',
                        target: -1
                    }
                },
                columnDefs: [
                    {
                        title: l('User'),
                        targets: 0,
                        data: 'user.userName'
                    },
                    {
                        title: l('Start'),
                        targets: 1,
                        data: 'callStartDateTime',
                        render: (data, type, row, meta) => {
                            return moment(data).format();
                        }
                    },
                    {
                        title: l('Duration'),
                        targets: 2,
                        data: 'duration'

                    },
                    {
                        title: l('CallResult'),
                        targets: 3,
                        data: 'callResult',
                        render: (data, type, row, meta) => pasr.maps.callResult[data]
                    },
                    {
                        title: l('ResultReason'),
                        targets: 4,
                        data: 'resultReason',
                        render: (data, type, row, meta) => pasr.maps.resultReason[data]
                    },
                    {
                        title: '',
                        className: 'control',
                        targets: 5,
                        data: null,
                    }
                ]
            });
        }
    });

    var _$usersTable = _$uTable.DataTable({
        paging: true,
        serverSide: true,
        ordering: true,
        sortable: true,
        ajax: function (data, callback, settings) {
            var filter = $('#UsersSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;
            abp.ui.setBusy(_$table);
            _userService.getAll(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function () {
                abp.ui.clearBusy(_$table);
            });
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$usersTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column',
                target: 4,
            }
        },
        columnDefs: [
            {
                targets: 0,
                data: 'userName',
            },
            {
                targets: 1,
                data: 'fullName',
            },
            {
                targets: 2,
                data: 'emailAddress',
                sortable: false
            },
            {
                targets: 3,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-primary assign-user" data-user-id="${row.id}" data-user-fname="${row.fullName}">`,
                        `       <i class="fas fa-user-check"></i> ${l('Assign')}`,
                        '   </button>'
                    ].join('');
                }
            },
            {
                targets: 4,
                orderable: false,
                sortable: false,
                className: 'control',
                defaultContent: '',
            }
        ]
    });

    function assignLead(userId, userName) {

        //Tá escondido no form da página 1
        leadId = _$formEdit.find('[name="id"]').val();
        debugger;
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToAssignTo'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _leadService.assignToUser({
                        id: leadId,
                        userId: userId
                    }).done(() => {
                        abp.event.trigger('user.assigned', {
                            id: userId,
                            fullName: userName
                        });
                        abp.notify.info(l('SuccessfullyAssigned'));
                        _$usersTable.ajax.reload();
                    }).fail((e) => { console.log(e); });
                }
            }
        );
    };

    $(document).on('click', '.assign-user', function (e) {
        e.preventDefault();

        var userId = $(this).attr("data-user-id");
        var userFullName = $(this).attr("data-user-fname");

        assignLead(userId, userFullName);
    });

    abp.event.on('user.assigned', (data) => {
        console.log('UserAssigned: ', data);
        console.log(_$userInput);
        _$userInput.each(function () { $(this).val(data.fullName) });
        _$usersTable.ajax.reload();
    });

    $('.btn-search').on('click', (e) => {
        _$usersTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$usersTable.ajax.reload();
            return false;
        }
    });

    _$modal.find('.save-button').on('click', (e) => {
        e.preventDefault();

        _$form.validate({ debug: true });

        if (!_$form.valid()) {
            return;
        }

        var lead = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _leadService
            .create(lead)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                _$leadsTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    _$editModal.find('.save-button').on('click', (e) => {

        e.preventDefault();

        _$formEdit.validate({ debug: true });

        if (!_$formEdit.valid()) {
            return;
        }

        debugger;

        abp.ui.setBusy(_$editModal);

        var lead = _$formEdit.serializeFormToObject();

        console.log(lead);

        _leadService
            .update(lead)
            .done(function () {
                _$editModal.modal('hide');
                _$formEdit[0].reset();
                abp.event.trigger('lead.edited', l('SavedSuccessfully'));
                _$leadsTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$editModal);
            });
    });

    $(document).on('click', '.delete-lead', function () {
        var leadId = $(this).attr("data-lead-id");
        var leadName = $(this).attr('data-lead-name');
        deleteLead(leadId, leadName);
    });

    $(document).on('click', '.call-lead', function () {

        callLeadId = $(this).attr("data-lead-id");
        let leadName = $(this).attr('data-lead-name');
        let leadPhone = $(this).attr('data-lead-phone');

        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToCallTo'),
                leadName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    $('#LeadCallModal').find('.modal-title').html(`${l('Call')} ${leadName} - ${leadPhone}`);
                    $('#LeadCallModal').modal('show');
                    callStart = moment().format();
                    //console.log("Inicio da ligação:", callStart);
                }
            }
        );
    });

    //#region callWorkflow
    $('#finishCall').on('click', function (e) {
        $(this).attr('disabled', true);
        _$callCountDown.countdown('pause');
        callEnd = moment().format();
        //console.log("Fim da ligação:", callEnd);
        //console.log(_$callCountDown.countdown('getTimes'));
        $('#submitCallButton').show(0);
        $('#significant').enableFormField(true).focus();
    });

    //Bootstraps the DatePicker for Date fields, with custom options
    $(function () {
        $(".datetime-picker").datetimepicker({
            minDate: moment(),
            locale: abp.localization.currentLanguage.name,
            icons: {
                time: "fa fa-clock",
                date: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down"
            }
        });
    });

    //Logic for manipulating form fields depending on the answer chosen
    $('#significant').change(function () {
        let opt = $(this).children("option:selected").val();
        switch (opt) {
            case 'true':
                $('#interest').enableFormField(true).focus();
                $('#nextContact').disableFormField();
                $('#resultReason').disableFormField();
                $('#callNotes').disableFormField();
                break;
            case 'false':
                $('#interest').disableFormField();
                $('#scheduled').disableFormField();
                $('#resultReason').enableFormField(true).focus();
                $('#nextContact').enableFormField(true);
                $('#callNotes').enableFormField().focus();
                break;
            default:
                break;
        }
    });

    $('#interest').change(function () {

        let opt = $(this).children("option:selected").val();
        switch (opt) {
            case 'true':
                $('#scheduled').enableFormField(true).focus();
                $('#resultReason').disableFormField();
                $('#callNotes').disableFormField();
                break;
            case 'false':
                $('#scheduled').disableFormField();
                $('#resultReason').enableFormField(true).focus();
                $('#callNotes').enableFormField();
                break;
            default:
                break;
        }
    });

    $('#scheduled').change(function () {

        let opt = $(this).children("option:selected").val();

        switch (opt) {
            case 'true':
                $('#resultReason').enableFormField(true).focus();
                $('#callNotes').enableFormField();
                $('#nextContact').disableFormField();
                break;
            case 'false':
                $('#resultReason').enableFormField(true).focus();
                $('#callNotes').enableFormField();
                $('#nextContact').enableFormField(true);
                break;
            default:
                break;
        }
    });

    $('#submitCallButton').on('click', (e) => {

        _$callForm.validate({ debug: true });

        if (!_$callForm.valid()) {
            return;
        }

        //Take into the DTO only the enabled (visible) fields
        var callDto = _$callForm.visibleFormToObject();

        //Fills the DTO fields with call start and callend datetimes onto the 
        callDto.callStartDateTime = callStart;
        callDto.callEndDateTime = callEnd;
        callDto.leadId = callLeadId;

        callDto.nextContact = $("#nextContact").data("DateTimePicker").viewDate().format();
        console.log(callDto);

        _callService.create(callDto).done(() => {
            abp.notify.info(l('CallSuccessfullyRegistered'));
            _$callModal.modal('hide');
            _$leadsTable.ajax.reload();
        });

    });
    //#endregion

    $(document).on('click', '.edit-lead', function (e) {

        var leadId = $(this).attr("data-lead-id");

        e.preventDefault();

        _leadService.getLeadForEdit({ id: leadId })
            .done(result => {
                _$formEdit.jsonToForm(result.lead);
            })
            .catch(e => {
                console.log(e);
                throw new Error('Ocorreu um erro no servidor!');
            });
    });

    abp.event.on('lead.edited', (message) => {
        abp.notify.info(message);
        _$leadsTable.ajax.reload();
    });

    function deleteLead(leadId, leadName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                leadName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _leadService.delete({
                        id: leadId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$leadsTable.ajax.reload();
                    });
                }
            }
        );
    };


    //#region model default focus input events
    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    _$editModal.on('shown.bs.modal', () => {
        _$editModal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => { });

    //Reset and fields 
    _$callModal.on('show.bs.modal', () => {

        //Empties the Form and Restart the Timer
        _$callForm.clearForm().find('.form-group').hide(500).removeAttr("required");

        $('#submitCallButton').hide(0);

        _$callCountDown.countdown('destroy');


        _$callCountDown.countdown({
            since: 0,
            format: 'hMS',
            layout: '<b>{hnn}{sep}{mnn}{sep}{snn}</b>'
        });

        $('#finishCall').removeAttr('disabled');

    }).on('hidden.bs.modal', () => {
    });
    //#endregion

    //#region search field behavior
    $('.btn-search').on('click', (e) => {
        _$leadsTable.ajax.reload();
        _$usersTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$leadsTable.ajax.reload();
            _$usersTable.ajax.reload();
            return false;
        }
    });
    //#endregion
})(jQuery);
