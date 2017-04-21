/*var app = angular.module("ApplicationModule", ["ngRoute"]);

app.factory("ShareData", function () {
    return { value: 0 };
});

//Showing Routing  
app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    debugger;
    $routeProvider.when('/Result/ShowUrl/:requestHistoryId',
                        {
                            templateUrl: 'View/Result/UrlFromHistory',
                            controller: 'UrlController'
                        });
    $routeProvider.when('/Result/ShowListOfHistories',
                        {
                            templateUrl: 'View/Result/ShowListOfHistories',
                            controller: 'AddStudentController'
                        });
    $routeProvider.otherwise(
                        {
                            redirectTo: '/'
                        });

    $locationProvider.html5Mode(true).hashPrefix('!');
}]);
*/