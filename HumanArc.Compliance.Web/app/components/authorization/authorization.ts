module app {
    'use strict';

    export interface IAuthorizationService {
        getRolesAndUsers();
        assign(auth: any);
        remove(role: string, userName: string);
    }

    class AuthorizationService implements IAuthorizationService {
        static $inject = ['$http', 'httpHelper'];
        constructor(private $http: ng.IHttpService, private httpHelper: IHttpHelperService) { }

        getRolesAndUsers() {
            var request = this.$http.get('/Authorization/GetRolesAndUsers');
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        assign(auth: any) {
            var request = this.$http.post('/Authorization/Assign', auth);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        remove(role: string, userName: string) {
            var request = this.$http.post('/Authorization/Remove', { UserName: userName, Role: role });
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }
    }

    angular.module('app').service('authorizationService', AuthorizationService);
}