var sitemapApp = angular.module("sitemapApp");

sitemapApp.controller("startEvaluationController", function ($scope, startEvaluationFactory, $http, $location) {

    $scope.startEvaluation = function (url) {
        startEvaluationFactory.startEvaluation(url)
            .then(function (res) {
                $scope.evalutionResult = res;
            });
    };
});