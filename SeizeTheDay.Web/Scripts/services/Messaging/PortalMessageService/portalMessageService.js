﻿angular.module('startupController')
    .service('portalMessageService', ['$http', function ($http) {

        var urlBase = 'https://localhost:44367/api/messages/getmessages';
        
        this.getMessages = function () {
            return $http.get(urlBase);
        };
    
        this.insertMessage = function (message) {
            return $http.post(urlBase, message);
        };

        this.deleteMessage = function (id) {
            return $http.delete(urlBase + '/' + id);
        };

    }]);