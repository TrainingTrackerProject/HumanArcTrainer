var app;
(function (app) {
    'use strict';
    var AuthorizationStateConfiguration = (function () {
        function AuthorizationStateConfiguration($stateProvider) {
            $stateProvider.state('authorization', {
                url: '/Authorization',
                parent: 'home',
                resolve: {},
                views: {
                    'content@': {
                        templateUrl: '/app/components/authorization/templates/authorization.html',
                        controller: 'authorizationController',
                        controllerAs: 'authorizationCtrl',
                        resolve: {
                            rolesList: ['nameValueService', function (nameValueService) { return nameValueService.getRoles(); }]
                        }
                    }
                }
            });
        }
        return AuthorizationStateConfiguration;
    }());
    AuthorizationStateConfiguration.$inject = ['$stateProvider'];
    app.AuthorizationStateConfiguration = AuthorizationStateConfiguration;
    angular
        .module('app')
        .config(AuthorizationStateConfiguration);
})(app || (app = {}));
//# sourceMappingURL=authorizationRouting.js.map