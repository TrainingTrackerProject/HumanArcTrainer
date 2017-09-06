var app;
(function (app) {
    'use strict';
    var AuthorizationController = (function () {
        function AuthorizationController(authorizationService, rolesList, toastrService) {
            var _this = this;
            this.authorizationService = authorizationService;
            this.rolesList = rolesList;
            this.toastrService = toastrService;
            authorizationService.getRolesAndUsers().then(function (data) {
                _this.rolesAndUsers = data;
            });
        }
        AuthorizationController.prototype.save = function () {
            var _this = this;
            if (this.authForm.$valid) {
                this.authorizationService.assign(this.auth).then(function (data) {
                    _this.toastrService.success('Sucessfully Saved');
                    _this.rolesAndUsers = data;
                }, function (data) {
                    _this.toastrService.error(data);
                });
            }
        };
        AuthorizationController.prototype.remove = function (role, userName) {
            var _this = this;
            this.authorizationService.remove(role, userName).then(function (data) {
                _this.toastrService.success('Sucessfully Removed');
                _this.rolesAndUsers = data;
            }, function (data) {
                _this.toastrService.error(data);
            });
        };
        return AuthorizationController;
    }());
    AuthorizationController.$inject = ['authorizationService', 'rolesList', 'toastrService'];
    angular.module('app').controller('authorizationController', AuthorizationController);
})(app || (app = {}));
//# sourceMappingURL=authorizationController.js.map