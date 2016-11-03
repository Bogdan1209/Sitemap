var uri = '';

var ResultAngl = angular.module("ResultAngl", ['ngRoute']);

ResultAngl.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
    .when('ShowHistory/:ReqestHistoryId', {
        templateUrl: '/Views/Result/UrlFromHistory.cshtml',
        controller: 'UrlController'
    })
    .when('/Result/Result', {
        templateUrl: 'Result/ShowListOfHistory',
        controller: 'ResultController'
    });
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);

ResultAngl.controller("ResultController", function ($scope, ResultService, $http, $location) {
    getHistories();
    function getHistories(){
        ResultService.getHistories()
        .success(function(hists){
            $scope.histories = hists;
        })
        .error(function(error){
            $scope.status = 'Unable to load customer data: ' + error.message;
        });
    } 

    $scope.getUrlOfHistory = function (id) {
        $location.path("/ShowHistory/" + id);
    };

    $scope.startEvaluation = function (url) {
        ResultService.startEvaluation(url)
        .success(function (res) {
            $scope.evalutionResult = res;
        });
    };

    $scope.deleteHistory = function (id) {
        ResultService.deleteHist(id)
        .success(function (response) {
            $scope.hist = response;
        })
               .error(function (error) {
                   alert(error);
               });
    };

    $scope.confirmDeleteHistory = function (id) {
        ResultService.confirmDeleteHistory(id)
        .success(function (response) {
            $scope.hist = response;
        })
               .error(function (error) {
                   alert(error);
               });
    };
});

ResultAngl.controller("UrlController", ['$scope', 'ResultService', '$http', '$routeParams', function ($scope, ResultService, $http, $routeParams) {
    var responseHistoryId = $routeParams.ReqestHistoryId;
    getUrlOfHistory(responseHistoryId);
    function getUrlOfHistory(historyId) {
        ResultService.getUrl(historyId)
            .success(function (urls) {
                $scope.urls = urls;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
            });
    };
}]);

ResultAngl.factory("ResultService", ['$http', function($http){
    
    var ResultService = {};

    ResultService.startEvaluation = function (url) {
        return $http({
            url: '/api/HomeApi',
            dataType: 'json',
            method: 'POST',
            data: JSON.stringify(url)
        });
    };

    ResultService.confirmDeleteHistory = function (id) {
        return $http({
            url: '/api/ResultApi/DeleteHistoryConfirmedAsync',
            dataType: 'json',
            method: 'POST',
            data: id,
            headers: {
                "Content-Type": "application/json"
            }
        });
    };

    ResultService.getHistories = function () {
        return $http.get('/api/ResultApi/GetHistoryAsync');
    };

    ResultService.getUrl = function (id) {
        return $http.get('/api/ResultApi/UrlsFromHistoryAsync/' + id);
    };
    ResultService.deleteHist = function (data) {
        return $http.get('/api/ResultApi/DeleteHistoryAsync/' + data);
    };
    return ResultService;


}]);
