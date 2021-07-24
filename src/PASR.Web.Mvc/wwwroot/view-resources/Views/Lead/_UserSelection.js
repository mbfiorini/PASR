(function ($) {
    var _userService = abp.services.app.user,
        l = abp.localization.getSource('PASR'),
        _$table = $('#UsersTable'),
        _leadService = abp.services.app.lead,
        _$userInput = $('#assignedUser');

    var _$usersTable = _$table.DataTable({
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
                defaultContent:'',
            }
        ]
    });

    function assignLead(userId, userName) {

        //Tá escondido no form da página 1
        leadId = $('#Lead_Id').val();

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
                    }).error((e) => { console.log(e); });
                }
            }
        );
    }

    $(document).on('click', '.assign-user', function (e) {
        var userId = $(this).attr("data-user-id");
        var userFullName = $(this).attr("data-user-fname");
        e.preventDefault();

        assignLead(userId, userFullName);
    });

    $(document).on('click', '.user-label', function (e) {
        var userId = $(this).attr("label-user-id");
        e.preventDefault();

        var userRow = _$usersTable.row(`#${userId}`).select();


    });

    abp.event.on('user.assigned', (data) => {
        _$usersTable.ajax.reload();
        _$userInput.val(data.fullName);     
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
})(jQuery);
//# sourceURL=_UserSelection.js

