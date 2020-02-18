angular.module('startupController')
    .service('forumPostLikeService', ['$http', function ($http) {

        var urlBase = '/api/ForumPostLike';

        this.getForumPostLike = function () {
            return $http.get(urlBase);
        };

        this.insertUpdateForumPostLike = function (forumPostLike) {
            return $http.post(urlBase, forumPostLike);
        };

        this.deleteForumPostLike = function (id) {
            return $http.delete(urlBase + '/' + id);
        };
    }]);