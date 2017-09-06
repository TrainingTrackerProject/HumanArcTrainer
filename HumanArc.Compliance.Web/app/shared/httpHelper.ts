module app {
    'use strict';
    export interface IHttpHelperService {
        handleSuccess(response);
        handleError(response);
    }

    export class HttpHelperService {

        static $inject = ['$q'];
        constructor(private $q: ng.IQService) { }

        handleSuccess(response) {
            return response.data;
        }

        handleError = (response): ng.IPromise<any> => {
            return this.$q.reject(response.data);
        }
    }

    angular.module('app').service('httpHelper', HttpHelperService);
}