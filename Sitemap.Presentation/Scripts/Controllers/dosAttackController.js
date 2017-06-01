var sitemapApp = angular.module("sitemapApp");

sitemapApp.controller("dosAttackController", function ($scope, dosAttackFactory) {

    $scope.startDos = function (url, countOfUsers) {
        dosAttackFactory.startDosAttack(url, countOfUsers)
            .then(function (result) {
                $scope.attacking = result.data;
            });
    };

    $scope.changeDosState = function () {
        dosAttackFactory.changeDosState()
            .then(function (result) {
                $scope.attacking = result.data;
            });
    };
});