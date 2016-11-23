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