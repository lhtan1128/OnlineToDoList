var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    debugger;
    $scope.GetToDoList = () => {
        var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

        $http({
            method: "get",
            url: "GetToDoList"
        }).then(function (response) {
            if (response && response.data) {
                response.data.forEach(
                    data => {
                        if (data && data.CreatedDateTime) {
                            let createdDate = new Date(data.CreatedDateTime.match(/\d+/)[0] * 1);
                            data.CreatedDateTime = createdDate.getDate() + " " + months[createdDate.getMonth()] + " " + createdDate.getFullYear()
                        }
                    }
                )

                response.data = response.data.sort((a, b) => (a.Status > b.Status) ? 1 : -1)
            }
            $scope.todoList = response.data;
        }, function () {
            alert("Error Occur");
        })
    };

    $scope.InsertTask = () => {
        $scope.task = {};
        $scope.task.Content = $scope.taskContent;
        $http({
            method: "post",
            url: "InsertTask",
            datatype: "json",
            data: JSON.stringify($scope.task)
        }).then(function (response) {
            $scope.GetToDoList();
            $scope.taskContent = "";
        }, function () {
            alert("Error Occur");
        })
    }

    $scope.DeleteTask = (task) => {
        $http({
            method: "post",
            url: "DeleteTask",
            datatype: "json",
            data: JSON.stringify(task)
        }).then(function (response) {
            $scope.GetToDoList();
        })
    };

    $scope.CompleteTask = (task) => {
        $http({
            method: "post",
            url: "CompleteTask",
            datatype: "json",
            data: JSON.stringify(task)
        }).then(function (response) {
            $scope.GetToDoList();
        })
    };
})