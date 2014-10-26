// jQuery extensions

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

// PushUI

if (typeof PushUI == "undefined") {
    var PushUI = {};
}
PushUI.namespace = function () {
    var a = arguments, o = null, i, j, d;
    for (i = 0; i < a.length; i = i + 1) {
        d = a[i].split(".");
        o = PushUI;

        // PushUI is implied, so it is ignored if it is included
        for (j = (d[0] == "PushUI") ? 1 : 0; j < d.length; j = j + 1) {
            o[d[j]] = o[d[j]] || {};
            o = o[d[j]];
        }
    }
    return o;
};

PushUI.namespace("merge");
PushUI.namespace("util");

/*global angular */
'use strict';
/**
 * The main Angular app module
 *
 * @type {angular.Module}
 */
var ngPushUiApp = typeof angular == "undefined" ? null : angular.module('ngPushUiApp', ['ui.bootstrap']);
ngPushUiApp = angular.module('ngPushUiApp'); // double check it was registered.
if (ngPushUiApp != null) {
    ngPushUiApp.factory('globalHttpInterceptor', function ($q) {
        return function (promise) {
            return promise.then(function (response) {
                // success
                var cb = new PushUI.util.ajaxCallback(response.data).handleSuccess();
                return response;
            }, function (response) {
                // error
                return $q.reject(response);
            });
        };
    });
    ngPushUiApp.config(function ($httpProvider) {
        $httpProvider.responseInterceptors.unshift('globalHttpInterceptor');
    });
    //ngPushUiApp.config(function ($compileProvider) {
    //    //SO this little gem allows angular to render its stuff properly in IE 7 for relative hrefs
    //    //for the record ie7 needs to die.
    //    $compileProvider.urlSanitizationWhitelist(/.*/);
    //});
    ngPushUiApp.directive('innerHtml', function () {
        return {
            restrict: 'A',
            scope: { innerHtml: '=' },
            replace: true,

            link: function (scope, element, attrs) {
                scope.$watch('innerHtml', function (value) {
                    element.html(value);
                });
            }
        };
    });
    ngPushUiApp.directive('datepicker', function ($filter) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                $(function () {
                    var config = {};
                    config.dateFormat = 'mm/dd/yy';
                    config.onSelect = function (date) {
                        ngModelCtrl.$setViewValue(date);
                        scope.$apply();
                    };
                    if (attrs.minDate)
                        config.minDate = parseInt(attrs.minDate);
                    element.datepicker(config);
                });
            }
        };
    });
    ngPushUiApp.directive('jqbutton', function () {
        return {
            restrict: 'E',
            replace: true,
            template: '<button></button>',
            link: function (scope, element, attrs) {
                $(function () {
                    // If you require additional attributes, please add conditions below!
                    // Supported: label, icon-primary
                    var config = {};
                    config.text = false;
                    if (attrs.label && attrs.label.length > 0) {
                        config.text = true;
                        config.label = attrs.label;
                    }
                    if (attrs.iconPrimary)
                        config.icons = { primary: attrs.iconPrimary };
                    var type = attrs.buttonType || "button";
                    element.prop("type", type).button(config);
                });
            }
        };
    });
}