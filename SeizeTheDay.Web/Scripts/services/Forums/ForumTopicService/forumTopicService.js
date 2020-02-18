angular.module('startupController')
    .service('forumTopicService', ['$http', function ($http) {

        var urlBase = '/api/ForumTopic';

        this.getForumTopicList = function () {
            return $http.get(urlBase);
        };

        this.getForumTopicListGetByForumID = function (id) {
            return $http.get(urlBase,id);
        };


        this.insertUpdateForumTopic = function (forumTopic) {
            return $http.post(urlBase, forumTopic);
        };

        this.deleteForumTopic = function (id) {
            return $http.delete(urlBase + '/' + id);
        };
    }]);