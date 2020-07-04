angular.module('startupController')
    .service('forumTopicService', ['$http', function ($http) {

        var urlBase = 'https://localhost:44367/api/forumtopics/';

        this.getForumTopicList = function () {
            return $http.get(urlBase + "gettopics");
        };

        this.getForumTopicListGetByForumID = function (id) {
            return $http.get(urlBase + "getbyforumid?id=" + id);
        };


        this.insertUpdateForumTopic = function (forumTopic) {
            return $http.post(urlBase, forumTopic);
        };

        this.deleteForumTopic = function (id) {
            return $http.delete(urlBase + '/' + id);
        };
    }]);