angular.module('startupController')
    .service('forumPostCommentService', ['$http', function ($http) {

        var urlBase = 'https://localhost:44367/api/comments/';

        this.getCommentListByPostID = function (id) {
            return $http.get(urlBase + "getlistbypostid?id=" + id);
        };

        this.insertUpdateComment = function (comment) {
            return $http.post(urlBase);
        };

        this.deleteComment = function (id) {
            return $http.delete(urlBase + "deletecomment?id=" + id);
        };
//////////////////////////////////////////////////////////////////////////////////////////
        this.updateComment = function (comment) {
            return $http.post(urlBase,comment);
        };

        this.getEditComment = function (id) {
            return $http.get(urlBase + "getbyid?id=" + id);
        };


    }]);