var sitemapApp = angular.module("sitemapApp");

sitemapApp.factory('historiesService', ['$http', function ($http) {

    var historiesService = {};

    historiesService.deleteConfirm = function (id) {
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

    historiesService.getHistories = function (pageNumber, orderBy) {
        if (pageNumber === undefined) pageNumber = 1;
        if (orderBy === undefined) orderBy = "";
        return $http.get('/api/ResultApi/GetHistoryAsync/' + pageNumber + '/' + orderBy);
    };

    historiesService.getUrls = function (id) {
        return $http.get('/api/ResultApi/UrlsFromHistoryAsync/' + id);
    };
    historiesService.deleteHist = function (data) {
        return $http.get('/api/ResultApi/DeleteHistoryAsync/' + data);
    };
    return historiesService;
}]);