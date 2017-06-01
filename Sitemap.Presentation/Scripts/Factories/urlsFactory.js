var sitemapApp = angular.module("sitemapApp");

sitemapApp.factory('urlsFactory', ['$http', function ($http) {

    urlsFactory = {};

    urlsFactory.getUrls = function (historyId) {
        return $http.get('/api/ResultApi/UrlsFromHistoryAsync/' + id);
    }
}]);