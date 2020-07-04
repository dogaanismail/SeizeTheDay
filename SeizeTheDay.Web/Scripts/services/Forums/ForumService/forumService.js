angular.module('startupController')
    .service('forumService', ['$http', function ($http) {

        var urlBase = '/api/Forums';
        var apiBase = 'https://localhost:44367/api/forums/';

        this.getForumList = function () {
            return $http.get(apiBase + "getforumsbydapper");
        };

        this.insertUpdateForum = function (forum) {
            return $http.post(urlBase, forum);
        };

        this.deleteForum = function (id) {
            return $http.delete(urlBase + '/' + id);
        };
    }]);