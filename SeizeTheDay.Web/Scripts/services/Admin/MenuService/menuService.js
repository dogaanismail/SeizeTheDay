angular.module('startupController')
    .service('menuService', ['$http', function ($http) {

        var urlBase = '/api/Menu';

        this.getMenuList = function () {
            return $http.get(urlBase);
        };
    }]);