var sitemapApp = angular.module("sitemapApp");

sitemapApp.factory('startEvaluationFactory', ['$http', function ($http) {

    var startEvaluationFactory = {};

    startEvaluationFactory.startEvaluation = function (url) {
        return $http({
            url: '/api/HomeApi',
            dataType: 'json',
            method: 'POST',
            data: JSON.stringify(url)
        });
    }
    return startEvaluationFactory;
}]);
//sitemapApp.service('startEvaluationFactory', startEvaluationFactory);