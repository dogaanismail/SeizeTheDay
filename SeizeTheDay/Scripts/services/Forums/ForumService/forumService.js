angular.module('startupController')
    .service('forumService', ['$http', function ($http) {

        var urlBase = '/api/Forums';

        this.getForumList = function () {
            return $http.get(urlBase);
        };

        this.insertUpdateForum = function (forum) {
            return $http.post(urlBase, forum);
        };

        this.deleteForum = function (id) {
            return $http.delete(urlBase + '/' + id);
        };
    }]);