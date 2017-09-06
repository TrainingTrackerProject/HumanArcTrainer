var app;
(function (app) {
    'use strict';
    var AssignmentStateConfiguration = (function () {
        function AssignmentStateConfiguration($stateProvider) {
            $stateProvider.state('assignment', {
                url: '/Assignment/:trainingId',
                parent: 'home',
                resolve: {},
                views: {
                    'content@': {
                        templateUrl: '/app/components/assignment/templates/assignment.html',
                        controller: 'assignmentController',
                        controllerAs: 'assignmentCtrl',
                        resolve: {}
                    }
                }
            });
        }
        return AssignmentStateConfiguration;
    }());
    AssignmentStateConfiguration.$inject = ['$stateProvider'];
    app.AssignmentStateConfiguration = AssignmentStateConfiguration;
    angular
        .module('app')
        .config(AssignmentStateConfiguration);
})(app || (app = {}));
//# sourceMappingURL=assignmentRouting.js.map