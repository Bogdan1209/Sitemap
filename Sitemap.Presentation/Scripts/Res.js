var TestCtrl = function ($scope, $http) {

    $scope.SendData = function (Data) {
        var GetAll = new Object();
        GetAll.FirstName = Data.firstName;
        GetAll.SecondName = Data.lastName;
        GetAll.SecondGet = new Object();
        GetAll.SecondGet.Mobile = Data.mobile;
        GetAll.SecondGet.EmailId = Data.email;
        $http({
            url: "/api/resultapi/firstCall",
            dataType: 'json',
            method: 'POST',
            data: GetAll,
            headers: {
                "Content-Type": "application/json"
            }
        }).success(function (response) {
            $scope.value = response;
        })
           .error(function (error) {
               alert(error);
           });
    };
};