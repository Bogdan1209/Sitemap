var sitemapApp = angular.module("sitemapApp");

sitemapApp.controller("historiesController", function ($scope, historiesService) {

    $scope.getHistories = function () {
        historiesService.getHistories()
            .then(function (hists) {
                $scope.histories = hists.data.histories;
                pageInfo = hists.data.pageInfo;
                $scope.maxSize = 5;
                $scope.itemsPerPage = pageInfo.pageSize;
                $scope.totalItems = pageInfo.totalItems;
                $scope.currentPage = pageInfo.pageNumber;
            });
        //FIX CODE BELOW
        /*.error(function (error) {
            $scope.status = 'Unable to load customer data: ' + error.message;
        });*/

    };
    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };
    $scope.pageChanged = function () {
        historiesService.getHistories($scope.currentPage)
            .then(function (hists) {
                $scope.histories = hists.data.histories;
            });
    };

    $scope.deleteHistory = function (id) {
        historiesService.deleteHist(id)
            .then(function (response) {
                $scope.hist = response.data;
            });
    };
    $scope.confirmDeleteHistory = function (id) {
        historiesService.deleteConfirm(id)
            .then(function (response) {
                $scope.status = response;
                $scope.getHistories();
                document.getElementById("closeModalBtn").click();
            });
    };

    });