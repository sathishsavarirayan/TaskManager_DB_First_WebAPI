
//$(document).ready(function () {
//    var apiBaseUrl = "http://localhost:52003/";
//    $('#btnGetData').click(function () {

//        $.ajax({
//            url: apiBaseUrl + 'api/Task',
//            type: 'GET',    
//            dataType: 'json',
//            success: function (data) {
//                var $table = $('<table/>').addClass('dataTable table table-bordered table-striped');
//                var $header = $('<thead/>').html('<tr><th>Task Name</th><th>Parent Task</th><th>Priority</th><th>Start_date</th><th>End_date</th></tr>');
//                $table.append($header);
//                $.each(data, function (i, val) {
//                    var $row = $('<tr/>');
//                    $row.append($('<td/>').html(val.TaskName));
//                    $row.append($('<td/>').html(val.TaskName));
//                    $row.append($('<td/>').html(val.Priority));
//                    $row.append($('<td/>').html(val.Start_Date));
//                    $row.append($('<td/>').html(val.End_Date));
//                    $table.append($row);
//                });
//                $('#updatePanel').html($table);
//            },
//            error: function () {
//                alert('Error!');
//            }
//        });
//    });
//});


var app = angular.module('ngmodule', []);

app.service('ngservice', function ($http) {
   
    this.getTaskdetails = function () {
        var result = $http.get("/GetTasks");
        return result;
    };

    this.getFilterdTaskDetails = function (filter, value) {
        var result;
        if (value.length == 0) {
            result = $http.get("/GetTasks");
            return result;
        }
        else {
            result = $http.get("/GetTasks/" + filter + "/" + value);
            return result;
        }
    };
});

app.controller('ngcontroller', function ($scope, ngservice) {

    $scope.Selectors = ["TaskName", "ParentTaskName", "Priority"];
    $scope.selectedcriteria = "";
    $scope.filteredvalue = "";

    loadTaskDetails();

    function loadTaskDetails() {
        var promise = ngservice.getTaskdetails();
        promise.then(function (resp) {
            $scope.TaskDetail = resp.data;
            $scope.message = "call is completed suffessfully";
        },
        function (err) {
            $scope.message = "call failed with" + err.status;
        });
    };

    $scope.getFilterdTaskDetails = function () {
        var promise = ngservice.getFilterdTaskDetails($scope.selectedcriteria, $scope.filteredvalue);
        promise.then(function (resp) {
            $scope.TaskDetails = resp.data;
            $scope.message = "Call completed"
        },
        function (err) {
            $scope.message = "call failed" + err.status;
        });
    };
});









