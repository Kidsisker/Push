
///<reference path="~/Scripts/jquery-1.8.2.min.js"/>
///<reference path="~/Scripts/angular/angular.min.js"/>
///<reference path="~/Scripts/angular/ui/ui-bootstrap.js"/>

angular.module('teamActivityUiApp', ['ngPushUiApp']).controller('TeamActivityCtrl', ['$scope', '$http', function (scope, http) {
	scope.init = function (model) {
		scope.showLoading(true);
		scope.requestModel = model;
		http.post(scope.requestModel.apiPath, scope.requestModel).success(function (data) {
			scope.viewModel = data;
			scope.setProject();
		});
	};
	scope.showLoading = function (show) {
		scope.loading = show;
	};
	scope.setProject = function () {
		if (!scope.viewModel.project)
			return;
		angular.forEach(scope.viewModel.projects, function (project) {
			if (project.name == scope.viewModel.project.name)
				scope.viewModel.project = project;
		});
	};
}]);
