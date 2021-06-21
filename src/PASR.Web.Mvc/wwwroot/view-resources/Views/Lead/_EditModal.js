(function ($) {
    var _leadService = abp.services.app.lead,
        l = abp.localization.getSource('PASR'),
        _$modal = $('#leadEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var lead = _$form.serializeFormToObject();
        //lead.grantedPermissions = [];
        //var _$permissionCheckboxes = _$form[0].querySelectorAll("input[name='permission']:checked");
        //if (_$permissionCheckboxes) {
        //    for (var permissionIndex = 0; permissionIndex < _$permissionCheckboxes.length; permissionIndex++) {
        //        var _$permissionCheckbox = $(_$permissionCheckboxes[permissionIndex]);
        //        lead.grantedPermissions.push(_$permissionCheckbox.val());
        //    }
        //}

        abp.ui.setBusy(_$form);
        _leadService.update(lead).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('lead.edited', lead);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);
