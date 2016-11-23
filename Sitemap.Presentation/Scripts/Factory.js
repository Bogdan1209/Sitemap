var ResultAngl = angular.module("ResultAngl");
ResultAngl.factory("ResultService", ['$http', function ($http) {

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