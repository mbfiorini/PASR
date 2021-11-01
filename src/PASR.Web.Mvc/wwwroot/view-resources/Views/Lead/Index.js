(function ($) {
    
    //#region variables
    var _leadService = abp.services.app.lead,
        l = abp.localization.getSource('PASR'),
        _$modal = $('#LeadCreateModal'),
        _$modalEdit = $('#LeadEditModal'),
        _$form = $('#leadCreateForm'),
        _$formEdit = $('#leadEditForm'),
        _$table = $('#LeadsTable'),
        _$uTable = $('#UsersTable'),
        _userService = abp.services.app.user,
        _$userInput = _$modalEdit.find('[name="assignedUserName"]');
    //#endregion
    console.log(_$userInput);

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
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$leadsTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
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
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-lead" data-lead-id="${row.id}" data-toggle="modal" data-target="#LeadEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-lead" data-lead-id="${row.id}" data-lead-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
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
        _$userInput.each(function() { $(this).val(data.fullName) } );
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

    _$modalEdit.find('.save-button').on('click', (e) => {

        e.preventDefault();

        _$formEdit.validate({ debug: true });

        if (!_$formEdit.valid()) {
            return;
        }

        debugger;
        
        abp.ui.setBusy(_$modalEdit);

        var lead = _$formEdit.serializeFormToObject();

        console.log(lead);

        _leadService
            .update(lead)
            .done(function () {
                _$modalEdit.modal('hide');
                _$formEdit[0].reset();
                abp.event.trigger('lead.edited',l('SavedSuccessfully'));
                _$leadsTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modalEdit);
            });
    });

    $(document).on('click', '.delete-lead', function () {
        var leadId = $(this).attr("data-lead-id");
        var leadName = $(this).attr('data-lead-name');

        deleteLead(leadId, leadName);
    });

    $(document).on('click', '.edit-lead', function (e) {

        var leadId = $(this).attr("data-lead-id");

        e.preventDefault();

        _leadService.getLeadForEdit({id: leadId})
            .done(result => {
                _$formEdit.jsonToForm(result.lead/* , {

                    name: function (value) {
                        $('[name="name"]').val(value);
                    },
                    lastName: function (value) {
                        $('[name="lastName"]').val(value);
                    },
                    identityCode: function (value) {
                        $('[name="identityCode"]').val(value);
                    },
                    emailAddress: function (value) {
                        $('[name="emailAddress"]').val(value);
                    },
                    companyName: function (value) {
                        $('[name="companyName"]').val(value);
                    },
                    phoneNumber: function (value) {
                        $('[name="phoneNumber"]').val(value);
                    },
                    leadNotes: function (value) {
                        $('[name="leadNotes"]').val(value);
                    },
                    assignedUser: function (value) {

                        if (value) {
                            $('[name="assignedUser"]').each(function() {$(this).val(value.fullName)} );
                        } else {
                            $('[name="assignedUser"]').each(function() {$(this).val('Undefined')} );
                        };
                    },
                    priority: function (value) {
                        $('[name="priority"]').val(value);
                    },
                    id: function (value) {
                        $('[name="id"]').val(value);
                    }
                }*/);

            } )
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

    _$modalEdit.on('shown.bs.modal', () => {
        _$modalEdit.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {

    });
    //#endregion

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
})(jQuery);
