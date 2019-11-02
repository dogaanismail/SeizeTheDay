angular.module('startupController')
    .service('notificationService', ['$http', function ($http) {

        var notificationUrl = '/api/Notifications';
        var notificationCountUrl = '/api/NotificationCount';

        var messageNotifUrl = '/api/MessageNotif';
        var messageNotifCountUrl = '/api/MessageNotifCount';


        ////////////////////// General Notifications //////////////////////////////////
        this.getGeneralNotif = function () {
            return $http.get(notificationUrl);
        };

        this.getGeneralNotifCount = function () {
            return $http.get(notificationCountUrl);
        };

        /////////////////////// Message Notifications //////////////////////////////////

        this.getMessageNotif = function () {
            return $http.get(messageNotifUrl);
        };

        this.getMessageCountNotif = function () {
            return $http.get(messageNotifCountUrl);
        };

    }]);