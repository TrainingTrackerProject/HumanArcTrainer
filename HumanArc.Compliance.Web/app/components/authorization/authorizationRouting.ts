module app {
    'use strict';

    export class AuthorizationStateConfiguration {
        static $inject = ['$stateProvider'];

        constructor($stateProvider: ng.ui.IStateProvider) {

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
                            rolesList: ['nameValueService', (nameValueService: INameValueService) => { return nameValueService.getRoles(); }]
                        }
                    }
                }
            });
        }
    }

    angular
        .module('app')
        .config(AuthorizationStateConfiguration);
}