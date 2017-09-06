module app {
    export interface INameValueService {
        getNameValueList(url: string);
        getRoles();
        getGroupsList();
    }

    export class NameValueService implements INameValueService {
        static $inject = ['$http', 'httpHelper'];
        constructor(private $http: ng.IHttpService,
            private httpHelper: HttpHelperService) { }

        getNameValueList(url: string) {
            return this.$http.get(url, { cache: true })
                .then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        getRoles() {
            return this.getNameValueList('/NameValue/GetRoles/');
        }
        getGroupsList() {
            return this.getNameValueList('/NameValue/GetGroupList/');
        }
    }

    angular.module('app').service('nameValueService', NameValueService);
}
