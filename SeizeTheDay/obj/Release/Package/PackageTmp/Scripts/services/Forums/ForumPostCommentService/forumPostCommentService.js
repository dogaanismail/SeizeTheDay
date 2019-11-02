angular.module('startupController')
    .service('forumPostCommentService', ['$http', function ($http) {

        var urlBase = '/api/PostComments';
        var updateUrl = '/api/EditComment';

        this.getCommentListByPostID = function (id) {
            return $http.get(urlBase,id);
        };

        this.insertUpdateComment = function (comment) {
            return $http.post(urlBase, comment);
        };

        this.deleteComment = function (id) {
            return $http.delete(urlBase + '/' + id);
        };
//////////////////////////////////////////////////////////////////////////////////////////
        this.updateComment = function (comment) {
            return $http.post(updateUrl,comment);
        };

        this.getEditComment = function (comment) {
            return $http.get(updateUrl, comment);
        };


    }]);