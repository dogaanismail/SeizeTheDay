angular.module('startupController')
    .service('userTypeService', ['$http', function ($http) {

        var urlBase = '/api/UserType';

        this.getTypes = function () {
            return $http.get(urlBase);
        };
    }]);