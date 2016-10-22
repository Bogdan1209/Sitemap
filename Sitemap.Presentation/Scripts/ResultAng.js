var uri = '';

var ResultAngl = angular.module("ResultAngl", []);
ResultAngl.controller("ResultController", function ($scope, ResultService, $http) {
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
    function getUrl() {
        ResultService.getUrl()
            .success(function(urls){
                $scope.urls = urls;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
            });
    }

    $scope.startEvaluation = function (url) {
        ResultService.startEvaluation(url)
        .success(function (res) {
            $scope.evalutionResult = res;
        })
    }

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

ResultAngl.factory("ResultService", ['$http', function($http){
    
    var ResultService = {};

    ResultService.startEvaluation = function (url) {
        return $http({
            url: '/api/HomeApi',
            dataType: 'json',
            method: 'POST',
            data: JSON.stringify(url),
            //headers: {
            //    "Content-Type": "application/json"
            //}
        })
    };

    ResultService.confirmDeleteHistory = function (id) {
        return $http({
            url: '/api/ResultApi/DeleteHistoryConfirmedAsync',
            dataType: 'json',
            method: 'POST',
            data:id,
            headers: {
                "Content-Type": "application/json"
            }
        })
    }

    ResultService.getHistories = function () {
        return $http.get('/api/ResultApi/GetHistoryAsync');
    };

    ResultService.getUrl = function () {
        return $http.get('/api/ResultApi/UrlsFromHistoryAsync');
    };
    ResultService.deleteHist = function (data) {
        return $http.get('/api/ResultApi/DeleteHistoryAsync/' + data)
    };
    return ResultService;


}]);
