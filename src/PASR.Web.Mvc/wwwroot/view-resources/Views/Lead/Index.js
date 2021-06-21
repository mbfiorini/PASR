(function ($) {
    var _leadService = abp.services.app.lead,
        l = abp.localization.getSource('PASR'),
        _$modal = $('#LeadCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#LeadsTable');

    var priorityList = ["Max","Normal","Min"];

    var _$leadsTable = _$table.DataTable({
        processing: true,
        serverSide: true,
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
                sortable: true

            },
            {
                targets: 4,
                data: 'emailAddress',
                sortable: true

            },
            {
                targets: 5,
                data:'priority',
                sortable: true

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

    _$form.find('.save-button').on('click', (e) => {
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

    $(document).on('click', '.delete-lead', function () {
        var leadId = $(this).attr("data-lead-id");
        var leadName = $(this).attr('data-lead-name');

        deleteLead(leadId, leadName);
    });

    $(document).on('click', '.edit-lead', function (e) {
        
        var leadId = $(this).attr("data-lead-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Lead/EditModal?LeadId=' + leadId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#LeadEditModal div.modal-content').html(content);
            },
            error: function (e) { 
                abp.message.error("Erro ao obter HTML através do Serviço!");
            }
        })
    });

    abp.event.on('lead.edited', (data) => {
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
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$leadsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$leadsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
