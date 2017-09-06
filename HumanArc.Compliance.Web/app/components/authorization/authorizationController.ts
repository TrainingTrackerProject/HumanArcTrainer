module app {
    'use strict';

    class AuthorizationController {
        rolesAndUsers: any;
        auth: any;
        authForm: ng.IFormController;

        static $inject = ['authorizationService', 'rolesList', 'toastrService'];
        constructor(private authorizationService: IAuthorizationService, private rolesList, private toastrService: IToastrService) {
            authorizationService.getRolesAndUsers().then((data) => {
                this.rolesAndUsers = data;
            });
        }

        save() {
            if (this.authForm.$valid) {
                this.authorizationService.assign(this.auth).then((data) => {
                    this.toastrService.success('Sucessfully Saved');
                    this.rolesAndUsers = data;
                }, (data) => {
                    this.toastrService.error(data);
                });
            }
        }

        remove(role, userName) {
            this.authorizationService.remove(role, userName).then((data) => {
                this.toastrService.success('Sucessfully Removed');
                this.rolesAndUsers = data;
            }, (data) => {
                this.toastrService.error(data);
            });
        }
    }

    angular.module('app').controller('authorizationController', AuthorizationController);
}