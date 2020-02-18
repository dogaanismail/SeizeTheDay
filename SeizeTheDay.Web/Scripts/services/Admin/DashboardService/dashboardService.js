angular.module('startupController')
    .service('dashboardService', ['$http', function ($http) {

        var urlBase = '/api/DashboardData';

        this.dashboardData = function () {
            return $http.get(urlBase);
        };

    }]);