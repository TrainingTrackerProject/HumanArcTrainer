module app {
    'use strict';

    export interface IAssignmentService {

    }

    class AssignmentService implements IAssignmentService {
        static $inject = ['$http', 'httpHelper'];
        constructor(private $http: ng.IHttpService, private httpHelper: IHttpHelperService) { }

    }

    angular.module('app').service('assignmentService', AssignmentService);
}