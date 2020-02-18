angular.module('startupController')
    .service('roleService', ['$http', function ($http) {

        var urlBase = '/api/Roles';

        this.getRoles = function () {
            return $http.get(urlBase);
        };

        this.getRoleByID = function (id) {
            return $http.get(urlBase + '/' + id);
        };

        this.insertUpdateRole = function (role) {
            return $http.post(urlBase, role);
        };

        this.deleteRole = function (id) {
            return $http.delete(urlBase + '/' + id);
        };

    }]);