angular.module('startupController')
    .service('chatService', ['$http', function ($http) {

        var urlBase = '/api/Chats';
        
        this.getChatsByBoxID = function (id) {
            return $http.get(urlBase,id);
        };
    
        this.deleteChatByID = function (mssgID) {
            return $http.delete(urlBase + '/' + mssgID);
        };

    }]);