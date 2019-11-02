angular.module('startupController')
    .service('moduleService', ['$http', function ($http) {

        var urlBase = '/api/Modules';

        this.getModuleList = function () {
            return $http.get(urlBase);
        };

        this.insertUpdateModule = function (module) {
            return $http.post(urlBase, module);
        };

        this.deleteModule = function (id) {
            return $http.delete(urlBase + '/' + id);
        };

    }]);