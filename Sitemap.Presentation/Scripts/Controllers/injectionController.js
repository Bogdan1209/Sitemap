var sitemapApp = angular.module("sitemapApp");

sitemapApp.controller("injectionController", function ($scope, injectionFactory) {

    $scope.postInjection = function (login, url) {
        injectionFactory.loginInjection(login,url)
            .then(function (result) {
                $scope.loginInjectResult = result.data;
            });

    };

    $scope.getInjection = function (url) {
        injectionFactory.getInjection(url)
            .then(function (result) {
                $scope.getInjectionResult = result.data;
            });
    };
    $scope.injections = [{ code: 1, name: 'Check login injection' }, { code: 2, name: 'Check get injection' }];
    $scope.loginShow = function () {
        document.getElementById('loginInject').style.visibility = 'visible';
        document.getElementById('getInject').style.visibility = 'hidden';
    };

    $scope.getShow = function () {
        if (document.getElementById('loginInject').style.display == 'none') {
            document.getElementById('loginInject').style.display = 'block';
            document.getElementById('getInject').style.display = 'none';
        }
        else {
            document.getElementById('loginInject').style.display = 'none';
            document.getElementById('getInject').style.display = 'block';
        }
    };

});