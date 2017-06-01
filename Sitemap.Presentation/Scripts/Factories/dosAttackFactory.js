var sitemapApp = angular.module("sitemapApp");

sitemapApp.factory('dosAttackFactory', ['$http', function ($http) {

    var dosAttackFactory = {};

    dosAttackFactory.startDosAttack = function (url, countOfThreads) {
        var dosData = {
            ip: url,
            countOfUsers: countOfThreads
        };
        return $http({
            url: '/api/DosAttackApi/StartDosAttack',
            dataType: 'json',
            method: 'POST',
            data: JSON.stringify(dosData)
        });
    };

    dosAttackFactory.changeDosState = function () {
        return $http({
            url: '/api/DosAttackApi/ChangeDosAttackState',
            dataType: 'json',
            method: 'POST',
            data: ''
        });
    }
    return dosAttackFactory;
}]);