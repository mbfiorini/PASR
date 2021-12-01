(function ($,_sh) {
    //#region variables
    var _userService = abp.services.app.user,
        _teamService = abp.services.app.team,
        l = abp.localization.getSource('PASR'),
        _$uTable = $('#UsersTable');
    //#endregion   
     
    $(function () {
        $('.team-shuffle').popover({
            container: 'body',
            selector: '.btn-details',
            boundary: 'viewport',
            trigger: 'focus',
            content: `<ul class="list-group list-group-flush" data-team-id="1">                    
                        <li class="list-group-item d-flex text-info menu-action btn-edit">
                            ${l('Edit')}
                            <span class="ml-3"><i class="fas fa-edit"></i></span>
                        </li>
                        <li class="list-group-item d-flex text-danger menu-action btn-delete">
                            ${l('Delete')}
                            <span class="ml-3"><i class="fas fa-trash"></i></span>
                        </li>
                    </ul>`,
            html: true,
            template:`<div class="popover action-list" role="tooltip">
                           <div class="arrow"></div>
                           <h3 class="popover-header"></h3>
                           <div class="popover-body"></div>
                       </div>`
            
        });
    });
    
    var shuffleElem = $('.team-shuffle');

    var myShuffle = new _sh(shuffleElem, {
        itemSelector: '.team-card',
        sizer: '.sizer-element'
    });        
    
    $('.btn.bg-blue').on('click', function () {
        //call Create modal Trigger created call xhr and trigger shuffle reload
        var data = {
            id: '1',
            name: 'Matheus Team',
            description: 'lorem ipsum sit dolor amet...',
            sdmUser: 'mbfiorini',
            sdmFullName: 'Matheus Fiorini',
            goalPoints: '10',
            goalTotal: '20',
            goalDueDate: new Date(),
            goalPercentage: '50'
        };
    
        //format will be the ajax, should create an arrau of divs and add to the shuffle
        let shuffleElem = $(teamCard(data));
    
        $('.team-shuffle').append(shuffleElem);
    
        myShuffle.add(shuffleElem.toArray());
    });
    
    
    //#region search field behavior
    $('.btn-search').on('click', function () {
        let filter = $('.txt-search').val().toLowerCase().trim();
        console.log(filter);
    
        //Ajax and reupdate de shuffle
        myShuffle.filter(function (element) {
            return (
                !$(element).attr('data-name') ||
                $(element).attr('data-name').toLowerCase().trim().includes(filter)
            );
        });
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            $('.btn-search').trigger('click');
            return false;
        }
    });
    //#endregion
    
    var _$usersTable = _$uTable.DataTable({
        paging: true,
        serverSide: true,
        ordering: true,
        sortable: true,
        ajax: function (data, callback, settings) {
            var filter = $('#UsersSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;
            abp.ui.setBusy(_$uTable);
            _userService.getAll(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function () {
                abp.ui.clearBusy(_$uTable);
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

    $(document).on('click', '.btn-edit', function (e) {

        var teamId = $(e.target).attr('data-user-id');

        e.preventDefault();

        _teamService.getTeamForEdit({ id: teamId })
            .done(result => {
            })
            .catch(e => {
                throw new Error('Ocorreu um erro no servidor!');
            });
    });

    $(document).on('click', '.btn-delete', function (e) {
        
        let list = $(e.target).closest('ul');

        var teamId = $(list).attr('data-team-id');
        var teamName = $(list).attr('data-team-name');

        e.preventDefault();
        
        deleteTeam(teamId, teamName);
        
    });

    $(document).on('click', '.assign-sdm', function (e) {
                
        var userId = $(e.target).attr('data-user-id');

        e.preventDefault();

        _teamService.assignToSdm({ id: userId })
            .done(result => {
                abp.notify.info(l('SuccessfullyAssigned'));
            })
            .catch(e => {
                throw new Error('Ocorreu um erro no servidor!');
            });
    });


    //#region Functions
    function deleteTeam(teamId, teamName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                teamName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _teamService.delete({
                        id: teamId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$usersTable.ajax.reload();
                    });
                }
            }
        );
    };

    function teamCard(team) {
        return `<div class="card col-xl-4 team-card">
                    <div class="card-header row">
                        <h5 class="card-title">${team.name}</h5>
                        <button type="button" class="btn btn-details ml-auto" data-toggle="popover" data-trigger="focus">
                            <i class="fas fa-ellipsis-h"></i>
                        </button>
                    </div>
                    <div class="card-body">
                        <p class="card-text">${team.description}</p>
                        <hr>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="info-box bg-blue">
                                    <span class="info-box-icon"><i class="fa fa-calendar"></i></span>
                                    <div class="info-box-content">
                                        <span class="info-box-text">Goal</span>
                                        <span class="info-box-number">${team.goalPoints}/${team.goalTotal} points</span>
                                        <div class="progress">
                                            <div class="progress-bar" role="progressbar" aria-valuenow="${team.goalPoints}" aria-valuemin="0" aria-valuemax="${team.goalTotal}" style="width: ${team.goalPercentage}%"></div>
                                        </div>
                                        <span class="">19/12/2021</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="info-box bg-green">
                                    <span class="info-box-icon"><i class="fa fa-user"></i></span>
                                    <div class="info-box-content">
                                        <span class="info-box-text">${team.sdmUser}</span>
                                        <span class="info-box-number"></span>
                                        <hr class="bg-white">
                                        <span class="">${team.sdmFullName}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`;
    };
    //#endregion    
})(jQuery,window.Shuffle);
