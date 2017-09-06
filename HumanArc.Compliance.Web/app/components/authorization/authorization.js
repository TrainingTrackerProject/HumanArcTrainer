var app;
(function (app) {
    'use strict';
    var AuthorizationService = (function () {
        function AuthorizationService($http, httpHelper) {
            this.$http = $http;
            this.httpHelper = httpHelper;
        }
        AuthorizationService.prototype.getRolesAndUsers = function () {
            var request = this.$http.get('/Authorization/GetRolesAndUsers');
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        AuthorizationService.prototype.assign = function (auth) {
            var request = this.$http.post('/Authorization/Assign', auth);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        AuthorizationService.prototype.remove = function (role, userName) {
            var request = this.$http.post('/Authorization/Remove', { UserName: userName, Role: role });
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        return AuthorizationService;
    }());
    AuthorizationService.$inject = ['$http', 'httpHelper'];
    angular.module('app').service('authorizationService', AuthorizationService);
})(app || (app = {}));
//# sourceMappingURL=authorization.js.map