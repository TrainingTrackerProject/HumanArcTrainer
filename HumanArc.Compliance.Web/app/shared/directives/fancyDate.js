'use strict';

angular.module('app').directive('fancyDate', function ($timeout) {
    return {
        require: '^form',
        restrict: 'E',
        templateUrl: "/app/shared/directives/templates/fancyDate.html",
        scope: {
            dateField: "="
        },
        link: function (scope, el, attrs, formCtrl) {

            scope.clearDate = function () {
                scope.dateField = null;
                if (scope.fieldName && formCtrl[scope.fieldName]) {
                    formCtrl[scope.fieldName].$setDirty();

                    // This bit of trickery is needed to trigger the blur event after $apply has finished.
                    // Triggering the blur event is needed to let the show-errors directive know that something has changed.
                    $timeout(function () {
                        var inputElement = angular.element(el[0].querySelector("[name]"));
                        inputElement.triggerHandler("blur");
                    }, 0);

                }
            };
        }
    };
});
