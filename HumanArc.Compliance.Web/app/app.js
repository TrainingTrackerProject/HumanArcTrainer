var app;
(function (app) {
    'use strict';
    var Config = (function () {
        function Config($stateProvider, $locationProvider, $httpProvider) {
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
        return Config;
    }());
    Config.$inject = ['$stateProvider', '$locationProvider', '$httpProvider'];
    app.Config = Config;
    var Run = (function () {
        function Run() {
        }
        return Run;
    }());
    app.Run = Run;
    angular.module('app', ['ui.router', 'ngFileUpload', 'ui.bootstrap', 'ui.date']).config(Config).run(Run);
})(app || (app = {}));
//# sourceMappingURL=app.js.map