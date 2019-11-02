angular.module('startupController')
    .service('chatboxService', ['$http', function ($http) {

        var urlBase = '/api/ChatBox';
        var postUrl = '/api/ChatBox?username=';
        var userUrl = '/api/NameList';

        
        this.getBoxListByID = function (id) {
            return $http.get(urlBase,id);
        };
    
        this.insertBox = function (userName) {
            return $http.post(postUrl + userName);
        };

        this.deleteBox = function (boxID) {
            return $http.delete(urlBase , boxID);
        };

        this.getUserNameList = function () {
            return $http.get(userUrl);
        };

    }]);