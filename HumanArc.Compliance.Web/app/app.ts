module app {
    'use strict';

    export class Config {

        static $inject = ['$stateProvider', '$locationProvider', '$httpProvider'];

        constructor($stateProvider: ng.ui.IStateProvider,
            $locationProvider: ng.ILocationProvider,
            $httpProvider: ng.IHttpProvider) {

            $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
            $httpProvider.defaults.headers['Cache-Control'] = 'no-cache';
            $httpProvider.defaults.headers['Pragma'] = 'no-cache';
            $httpProvider.defaults.headers['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';

            $locationProvider.html5Mode(true);

            $stateProvider
                .state('root', {
                    abstract: true,
                    resolve: {}
                });

            $stateProvider
                .state('home', {
                    abstract: true,
                    parent: 'root',
                    resolve: {},
                    views: {
                        "topnavAndCurrentUser@": {
                            templateUrl: 'app/components/layout/templates/topnav.html'
                        },
                        "leftnav@": {
                            templateUrl: 'app/components/layout/templates/leftnav.html'
                        }
                    }
                });

            $stateProvider
                .state('admin', {
                    abstract: true,
                    parent: 'root',
                    resolve: {},
                    views: {
                        "topnavAndCurrentUser@": {
                            templateUrl: 'app/components/layout/templates/topnav.html',
                            controller: 'topnavController',
                            controllerAs: 'model'
                        },
                        "leftnav@": {
                            templateUrl: 'app/components/layout/templates/leftnavAdmin.html',
                            controller: 'leftnavController',
                            controllerAs: 'model'
                        }
                    }
                });

            $stateProvider
                .state('home.index', {
                    url: '/',
                    views: {
                        "content@": {
                            templateUrl: 'app/components/layout/templates/home.html'
                        }
                    }
                });
        }
    }

    export class Run {

        //static $inject = ['$rootScope', '$log'];
        //constructor(private $rootScope: ng.IRootScopeService, private $log: ng.ILogService) {
        //    $rootScope.$on('$stateChangeError', (event, toState, toParams, fromState, fromParams, error) => {
        //        this.$log.error("State change error:");
        //        this.$log.error(toState);
        //        this.$log.error(toParams);
        //        this.$log.error(fromState);
        //        this.$log.error(fromParams);
        //        this.$log.error(error);
        //    });
        //}
    }

    angular.module('app', ['ui.router', 'ngFileUpload', 'ui.bootstrap', 'ui.date' ]).config(Config).run(Run);
}