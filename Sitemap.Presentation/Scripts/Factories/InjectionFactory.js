var sitemapApp = angular.module("sitemapApp");

sitemapApp.factory('injectionFactory', ['$http', function ($http) {

    var injectionFactory = {};

    injectionFactory.getInjection = function (url) {
        return $http({
            url: '/api/InjectionApi/GetInjection',
            dataType: 'json',
            method: 'POST',
            data: JSON.stringify(url)
        });
    };

    injectionFactory.loginInjection = function (usrLogin, loginUrl) {
        var userData = {
            login: usrLogin,
            url: loginUrl
        };
        return $http({
            url: '/api/InjectionApi/LoginInjection',
            dataType: 'json',
            method: 'POST',
            data: JSON.stringify(userData)
        });
    }
    return injectionFactory;
}]);