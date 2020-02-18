angular.module('startupController')
    .service('forumPostService', ['$http', function ($http) {

        var urlBase = '/api/ForumPost';
        var postDetail = '/api/TopicDetail';

        this.getForumPostList = function () {
            return $http.get(urlBase);
        };

        this.insertUpdateForumPost = function (forumPost) {
            return $http.post(urlBase, forumPost);
        };

        this.deleteForumPost = function (id) {
            return $http.delete(urlBase + '/' + id);
        };

        ////////////////   POSTDETAIL ////////////////////////////////////
        this.getTopicDetail = function (id) {
            return $http.get(postDetail, id);
        };

        this.updatePostDetail = function (forumPost) {
            return $http.post(postDetail, forumPost);
        };

        this.deleteTopicDetail = function (id) {
            return $http.delete(postDetail + '/' +  id);
        };
    }]);