﻿var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('Radin');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);