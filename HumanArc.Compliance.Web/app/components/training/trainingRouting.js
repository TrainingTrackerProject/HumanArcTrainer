var app;
(function (app) {
    'use strict';
    var TrainingStateConfiguration = (function () {
        function TrainingStateConfiguration($stateProvider) {
            $stateProvider.state('training', {
                url: '/Training',
                parent: 'home',
                resolve: {},
                views: {
                    'content@': {
                        templateUrl: '/app/components/training/templates/training.html',
                        controller: 'trainingController',
                        controllerAs: 'trainingCtrl',
                        resolve: {}
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
        return TrainingStateConfiguration;
    }());
    TrainingStateConfiguration.$inject = ['$stateProvider'];
    app.TrainingStateConfiguration = TrainingStateConfiguration;
    angular
        .module('app')
        .config(TrainingStateConfiguration);
})(app || (app = {}));
//# sourceMappingURL=trainingRouting.js.map