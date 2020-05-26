angular.module('startupController')
    .service('forumPostService', ['$http', function ($http) {

        var urlBase = 'https://localhost:44367/api/forumposts/';
        var postDetail = 'https://localhost:44367/api/postdetail/';

        this.getForumPostList = function () {
            return $http.get(urlBase + "getposts");
        };

        this.insertUpdateForumPost = function (forumPost) {
            return $http.post(urlBase, forumPost);
        };

        this.deleteForumPost = function (id) {
            return $http.delete(urlBase + '/' + id);
        };

        ////////////////   POSTDETAIL ////////////////////////////////////
        this.getTopicDetail = function (id) {
            return $http.get(postDetail + "getdetailsbydapper?id=" + id);
        };

        this.updatePostDetail = function (forumPost) {
            return $http.post(postDetail + "editpostdetail" + '/', forumPost);
        };

        this.deletePost = function (id) {
            return $http.delete(postDetail + '/' + id);
        };
    }]);