var sitemapApp = angular.module("sitemapApp");

sitemapApp.factory('historiesService', ['$http', function ($http) {

    var historiesService = {};

    historiesService.deleteConfirm = function (id, $http) {
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

    historiesService.getHistories = function () {
        return $http.get('/api/ResultApi/GetHistoryAsync');
    };

    historiesService.getUrl = function (id) {
        return $http.get('/api/ResultApi/UrlsFromHistoryAsync/' + id);
    };
    historiesService.deleteHist = function (data) {
        return $http.get('/api/ResultApi/DeleteHistoryAsync/' + data);
    };
    return historiesService;
}]);