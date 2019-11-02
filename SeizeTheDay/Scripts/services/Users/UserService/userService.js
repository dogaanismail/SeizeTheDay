angular.module('startupController')
    .service('userService', ['$http', function ($http) {

        var urlBase = '/api/IdendityUsers';
        var changePasswordUrl = '/api/ChangePassword';


        this.getUsers = function () {
            return $http.get(urlBase);
        };

        this.getUserRole = function (id) {
            return $http.get(urlBase + '/' + id);
        };

        this.insertUpdateUser = function (user) {
            return $http.post(urlBase, user);
        };

        this.deleteUser = function (id) {
            return $http.delete(urlBase + '/' + id);
        };

        this.changePassword = function (change) {
            return $http.post(changePasswordUrl, change);
        };

    }]);