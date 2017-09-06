module app {
    'use strict';

    export class AssignmentStateConfiguration {
        static $inject = ['$stateProvider'];

        constructor($stateProvider: ng.ui.IStateProvider) {

            $stateProvider.state('assignment', {
                url: '/Assignment/:trainingId',
                parent: 'home',
                resolve: {},
                views: {
                    'content@': {
                        templateUrl: '/app/components/assignment/templates/assignment.html',
                        controller: 'assignmentController',
                        controllerAs: 'assignmentCtrl',
                        resolve: {

                        }
                    }
                }
            });
        }
    }

    angular
        .module('app')
        .config(AssignmentStateConfiguration);
}