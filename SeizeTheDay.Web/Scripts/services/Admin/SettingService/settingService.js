angular.module('startupController')
    .service('settingService', ['$http', function ($http) {

        var urlBase = 'https://localhost:44367/api/settings/';

        this.getSettings = function () {
            return $http.get(urlBase + "getsettings");
        };

        this.getSettingByID = function (id) {
            return $http.get(urlBase + "getbyid?id=" + id);
        };

        this.insertSetting = function (model) {
            return $http.post(urlBase + "createsetting" + '/', model);
        };

        this.updateSetting = function (model) {
            return $http.post(urlBase + "updatesetting" + '/', model);
        };

        this.deleteSetting = function (id) {
            return $http.post(urlBase + "deletesetting?id=" + id);
        };

    }]);