module app {
    'use strict';

    export class TrainingStateConfiguration {
        static $inject = ['$stateProvider'];

        constructor($stateProvider: ng.ui.IStateProvider) {

            $stateProvider.state('training', {
                url: '/Training',
                parent: 'home',
                resolve: {},
                views: {
                    'content@': {
                        templateUrl: '/app/components/training/templates/training.html',
                        controller: 'trainingController',
                        controllerAs: 'trainingCtrl',
                        resolve: {
                        }
                    }
                }
            }).state('training.edit', {
                url: '/Edit/:trainingId?',
                    resolve: {},
                    views: {
                        'content@': {
                            templateUrl: '/app/components/training/templates/edit.html',
                            controller: 'trainingEditController',
                            controllerAs: 'trainingEditCtrl',
                            resolve: {}
                        }
                    }
            });
        }
    }

    angular
        .module('app')
        .config(TrainingStateConfiguration);
}