(function ($) {    
    //#region variables
    var _callService = abp.services.app.call,
        _callService = abp.services.app.call,
        l = abp.localization.getSource('PASR'),
        _$modal = $('#CallCreateModal'),
        _$editModal = $('#CallEditModal'),
        _$form = $('#callCreateForm'),
        _$formEdit = $('#callEditForm'),
        _$table = $('#CallsTable'),
        _$uTable = $('#UsersTable'),
        _userService = abp.services.app.user,
        _$userInput = _$editModal.find('[name="assignedUserName"]'),
        _$callModal = $('#CallCallModal'),
        _$callForm = $('#callForm'),
        _$callCountDown = $('#callCountdown'),
        callStart,
        callEnd,
        callCallId;
    //#endregion    

    var _$callsTable = _$table.DataTable({
        processing: true,
        serverSide: true,
        ordering: true,
        order: [[5, 'desc']],
        ajax: function (data, callback, settings) {
            var filter = $('#CallsSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _callService.getAll(filter).done(function (result) {
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
                action: () => _$callsTable.draw(false)
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

                    if (abp.auth.isGranted('Update.Calls')) {
                        return [
                            `   <button type="button" class="btn btn-sm bg-secondary edit-call" data-call-id="${row.id}" data-toggle="modal" data-target="#CallEditModal">`,
                            `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                            '   </button>',
                            `   <button type="button" class="btn btn-sm bg-danger delete-call" data-call-id="${row.id}" data-call-name="${row.name}">`,
                            `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                            '   </button>',
                        ].join('');
                    }else{
                        return [
                            `   <button type="button" class="btn btn-sm bg-primary call-call" data-call-id="${row.id}" data-call-phone="${row.phoneNumber}" data-call-name="${row.name}">`,
                            `       <i class="fas fa-phone"></i> ${l('Call')}`,
                            '   </button>'
                        ].join('');
                    }
                    
                }
            }
        ]
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

    _$editModal.find('.save-button').on('click', (e) => {

        e.preventDefault();

        _$formEdit.validate({ debug: true });

        if (!_$formEdit.valid()) {
            return;
        }

        debugger;
        
        abp.ui.setBusy(_$editModal);

        var call = _$formEdit.serializeFormToObject();

        console.log(call);

        _callService
            .update(call)
            .done(function () {
                _$editModal.modal('hide');
                _$formEdit[0].reset();
                abp.event.trigger('call.edited',l('SavedSuccessfully'));
                _$callsTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$editModal);
            });
    });

    $(document).on('click', '.delete-call', function () {
        var callId = $(this).attr("data-call-id");
        var callName = $(this).attr('data-call-name');
        deleteCall(callId, callName);
    });

    $(document).on('click', '.call-call', function () {

        callCallId = $(this).attr("data-call-id");
        let callName = $(this).attr('data-call-name');
        let callPhone = $(this).attr('data-call-phone');

        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToCallTo'),
                callName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    $('#CallCallModal').find('.modal-title').html(`${l('Call')} ${callName} - ${callPhone}`);                    
                    $('#CallCallModal').modal('show');
                    callStart = moment().format();
                    //console.log("Inicio da ligação:", callStart);
                }
            }
        );
    });    

    //Bootstraps the DatePicker for Date fields, with custom options
    // $(function () {
    //     $(".datetime-picker").datetimepicker({
    //         minDate:moment(),
    //         locale:abp.localization.currentLanguage.name,
    //         icons: {
    //             time: "fa fa-clock",
    //             date: "fa fa-calendar",
    //             up: "fa fa-arrow-up",
    //             down: "fa fa-arrow-down"
    //         }
    //     });
    // });

    $(document).on('click', '#showCallDetails', function (e) {

        var callId = $(this).attr("data-call-id");

        e.preventDefault();

        _callService.getCallForEdit({id: callId})
            .done(result => {
                _$formEdit.jsonToForm(result.call);
            } )
            .catch(e => {
                console.log(e);
                throw new Error('Ocorreu um erro no servidor!');
            });
    });

    abp.event.on('call.edited', (message) => {
        abp.notify.info(message);
        _$callsTable.ajax.reload();
    });
    
    //#region model default focus input events
    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });
    
    _$editModal.on('shown.bs.modal', () => {
        _$editModal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {});

    //#endregion
    
    $('.btn-search').on('click', (e) => {
        _$callsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$callsTable.ajax.reload();
            return false;  
        }
    });
})(jQuery);
