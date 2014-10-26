
///<reference path="~/Scripts/jquery-1.8.2.min.js"/>
///<reference path="~/Scripts/angular/angular.min.js"/>
///<reference path="~/Scripts/angular/ui/ui-grid/ui-grid.js"/>
///<reference path="~/Scripts/angular/ui/ui-bootstrap.js"/>

angular.module('buildQueryUiApp', ['ngPushUiApp', 'ui.grid', 'ui.grid.resizeColumns'])
    .controller('BuildQueryCtrl', ['$scope', '$http', function(scope, http) {
        scope.$scope = scope;
        scope.gridResults = {
            data: 'viewModel.results',
            sortInfo: { fields: ['finishTime'], directions: ['desc'] },
            columnDefs:
            [
                { field: 'number', name: 'Build', resizable: true, width: '25%' },
                { field: 'definition', name: 'Definition', resizable: true, width: '15%' },
                { field: 'quality', name: 'Quality', resizable: true, width: '15%' },
                { field: 'startTime', name: 'Start', resizable: true, width: '15%', cellFilter: "date:'MM/dd/yyyy h:mm a'" },
                { field: 'finishTime', name: 'Finish', resizable: true, width: '15%', cellFilter: "date:'MM/dd/yyyy h:mm a'" },
                { field: 'requestedBy', name: 'Requested By', resizable: true, width: '10%' }
            ]
        };
        scope.init = function(model) {
            scope.showLoading(true);
            scope.requestModel = model;
            http.post(scope.requestModel.apiPath, scope.requestModel).success(function(data) {
                scope.viewModel = data;
                scope.showLoading(false);
            });
        };
        scope.showLoading = function(show) {
            scope.loading = show;
        };
        scope.getQueue = function() {
            scope.showLoading(true);
            http.post(scope.viewModel.getQueuePath, scope.viewModel.requestModel).success(function(data) {
                scope.queueItems = data || [];
                scope.showLoading(false);
            });
        };
    }]);
