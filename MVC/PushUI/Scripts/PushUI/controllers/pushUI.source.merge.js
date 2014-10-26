
///<reference path="~/Scripts/jquery-1.8.2.min.js"/>
///<reference path="~/Scripts/angular/angular.min.js"/>
///<reference path="~/Scripts/angular/ui/ui-grid/ui-grid.js"/>
///<reference path="~/Scripts/angular/ui/ui-bootstrap.js"/>

angular.module('mergeUiApp', ['ngPushUiApp', 'ui.grid', 'ui.grid.resizeColumns']).controller('MergeCtrl', ['$scope', '$http', function (scope, http) {
	scope.$scope = scope;
	scope.gridWorkItems = {
		data: 'workItems',
		columnDefs:
		[
			{ field: 'id', name: 'ID', resizable: true, width: '10%' },
			{ field: 'type.name', name: 'Type', resizable: true, width: '15%' },
			{ field: 'title', name: 'Title', resizable: true, width: '70%' }
		]
	};
	
	scope.gridMergeTasks = {
		data: 'mergeTasks',
		columnDefs:
		[
			{ field: 'id', name: 'ID', resizable: true, width: '10%' },
			{ field: 'title', name: 'Title', resizable: true, width: '85%' }
		]
	};
	scope.gridCandidates = {
	    data: 'mergeCandidates',
	    enableRowSelection: true,
	    multiSelect: false,
	    columnDefs:
		[
			{ field: 'id', name: 'ID', resizable: true, width: '8%' },
			{ field: 'committedBy', name: 'By', resizable: true, width: '10%' },
			{ field: 'workItems[0].id', name: 'Work Item', resizable: true, width: '8%' },
			{ field: 'workItems[0].state', name: 'State', resizable: true, width: '10%' },
			{ field: 'workItems[0].title', name: 'Title', resizable: true, width: '50%' }
		]
	};
	scope.gridChangesets = {
		data: 'changesets',
		enableRowSelection: true,
		multiSelect: false,
		columnDefs:
		[
			{ displayName: ' ', name: 'Status', resizable: false, width: '3%' },
			{ name: 'Message', resizable: true, width: '30%' },
			{ field: 'id', name: 'ID', resizable: true, width: '8%' },
			{ field: 'workItems[0].id', name: 'Work Item', resizable: true, width: '8%' },
			{ field: 'workItems[0].title', name: 'Title', resizable: true, width: '50%' }
		]
	};
	scope.gridChangesets.columnDefs[0].cellTemplate = '<div class="ui-grid-cell-contents">' +
		'<span ng-show="(row.entity.status && row.entity.mergedChangeset) || (!row.entity.autoCommit && row.entity.status && row.entity.status.numFailures <= 0 && row.entity.status.numConflicts <= 0)">&#10003;</span>' +
		'<span ng-show="row.entity.status && !row.entity.mergedChangeset" ng-click="commitChangeset(row.entity)">&#10007;</span>' +
		'</div>';
	scope.gridChangesets.columnDefs[1].cellTemplate = '<div class="ui-grid-cell-contents">' +
		'<span ng-show="row.entity.autoCommit && row.entity.mergedChangeset">Committed Changeset: {{row.entity.mergedChangeset.id}}</span>' +
		'<span ng-show="row.entity.autoCommit && !row.entity.mergedChangeset && row.entity.status && row.entity.status.noActionNeeded && row.entity.status.numFiles == 0">Nothing to merge.</span>' +
		'<span ng-show="!row.entity.autoCommit && row.entity.status && row.entity.status.numFailures <= 0 && row.entity.status.numConflicts <= 0">Merged Changeset.</span>' +
		'<span ng-show="!row.entity.mergedChangeset && row.entity.status.numConflicts > 0" class="grid-action-cell">Click to <a ng-click="getExternalScopes().commitChangeset(row.entity);">commit</a> after resolving conflicts.</span>' +
		'<span ng-show="!row.entity.mergedChangeset && row.entity.status.numConflicts <= 0 && row.entity.status.numFailures > 0">{{row.entity.status.message}}</span>' +
		'</div>';
	
	scope.init = function (model) {
		scope.showLoading(true);
		scope.requestModel = model;
		http.post(scope.requestModel.apiPath, scope.requestModel).success(function (data) {
			scope.viewModel = data;
			scope.setProject();
			scope.setEnvironment();
			scope.setMergeMethod();
			scope.showLoading(false);
		});
	};
	scope.showLoading = function (show) {
		scope.loading = show;
	};
	scope.setMergeMethod = function () {
		if (!scope.viewModel.mergeMethod)
			return;
		angular.forEach(scope.viewModel.mergeMethods, function (method) {
			if (method.method == scope.viewModel.mergeMethod.method) {
				scope.viewModel.mergeMethod = method;
				angular.forEach(scope.viewModel.mergeMethod.options, function (option) {
					if (option.method == scope.viewModel.mergeMethod.method && option.methodValue == scope.viewModel.mergeMethodOption.methodValue) {
						scope.viewModel.mergeMethodOption = option;
					}
				});
			}
		});
	};
	scope.setEnvironment = function () {
		if (!scope.viewModel.environment)
			return;
		angular.forEach(scope.viewModel.project.environments, function (environment) {
			if ((environment.source.value == scope.viewModel.environment.source.value) && (environment.target.value == scope.viewModel.environment.target.value))
				scope.viewModel.environment = environment;
		});
	};
	scope.setProject = function () {
		if (!scope.viewModel.project)
			return;
		angular.forEach(scope.viewModel.projects, function (project) {
			if (project.name == scope.viewModel.project.name)
				scope.viewModel.project = project;
		});
	};
	scope.getWorkItems = function () {
		scope.showLoading(true);
		http.post(scope.viewModel.getWorkItemsPath, scope.viewModel.mergeMethodOption).success(function (data) {
			scope.workItems = data || [];
			scope.showLoading(false);
		});
	};
	scope.getMigrationScripts = function () {
		scope.showLoading(true);
		http.post(scope.viewModel.getMigrationScriptsPath, scope.viewModel.mergeMethodOption).success(function (data) {
			scope.mergeTasks = data || [];
			scope.showLoading(false);
		});
	};
	scope.getChangesets = function () {
		scope.showLoading(true);
		http.post(scope.viewModel.getChangesetsPath, scope.viewModel).success(function (data) {
			scope.changesets = data || [];
			scope.showLoading(false);
		});
	};
	scope.getMergeCandidates = function () {
	    scope.showLoading(true);
	    http.post(scope.viewModel.getMergeCandidatesPath, scope.requestModel).success(function (data) {
	        scope.mergeCandidates = data || [];
	        scope.showLoading(false);
	    });
	};
	scope.mergeChangeset = function (index, indexMax) {
		scope.showLoading(true);
		indexMax = typeof indexMax == 'undefined' ? scope.changesets.length : indexMax;
		if (index >= scope.changesets.length || index > indexMax) {
			scope.showLoading(false);
			return;
		}
		var postdata = scope.changesets[index];
		postdata.autoCommit = scope.viewModel.autoCommit;
		http.post(scope.viewModel.mergeChangesetPath, postdata).success(function (data) {
			if (!data || !data.status)
				return;
			postdata.status = data.status;
			postdata.mergedChangeset = data.mergedChangeset;
			if (postdata.status.numFailures > 0 || postdata.status.numConflicts > 0)
				return;
			scope.showLoading(false);
			scope.mergeChangeset(index + 1, indexMax);
		});
	};
	scope.commitChangeset = function (model) {
		scope.showLoading(true);
		http.post(scope.viewModel.commitChangesetPath, model).success(function (data) {
			if (!data || !data.status)
				return;
			model.status = data.status;
			model.mergedChangeset = data.mergedChangeset;
			scope.showLoading(false);
		});
	};
	scope.buildEnvironment = function() {
		scope.showLoading(true);
		http.post(scope.viewModel.buildEnvironmentPath, scope.requestModel).success(function (data) {
			scope.showLoading(false);
		});
	};
}]);
