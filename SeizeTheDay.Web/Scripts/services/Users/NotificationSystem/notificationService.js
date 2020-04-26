angular.module('startupController')
    .service('notificationService', ['$http', function ($http) {

        var notificationUrl = 'https://localhost:44367/api/notifications/';

        ////////////////////// General Notifications //////////////////////////////////
        this.getGeneralNotif = function () {
            return $http.get(notificationUrl + "getnotifications");
        };

        this.getGeneralNotifCount = function () {
            return $http.get(notificationUrl + "getcount");
        };

        /////////////////////// Message Notifications //////////////////////////////////

        this.getMessageNotif = function () {
            return $http.get(notificationUrl + "getmessagenot");
        };

        this.getMessageCountNotif = function () {
            return $http.get(notificationUrl + "getnotcount");
        };

    }]);