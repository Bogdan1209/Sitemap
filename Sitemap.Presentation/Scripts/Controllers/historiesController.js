var sitemapApp = angular.module("sitemapApp");

sitemapApp.controller("historiesController", function ($scope, historiesService) {

    $scope.getHistories =  function() {
        historiesService.getHistories()
            .then(function (hists) {
                $scope.histories = hists;
            });
                                                    //FIX CODE BELOW
            /*.error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
            });*/
    }; 
})