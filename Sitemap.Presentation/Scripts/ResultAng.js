var uri = '';

var ResultAngl = angular.module("ResultAngl", ['ngRoute']);

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


