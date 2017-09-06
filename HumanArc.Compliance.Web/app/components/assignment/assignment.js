var app;
(function (app) {
    'use strict';
    var AssignmentService = (function () {
        function AssignmentService($http, httpHelper) {
            this.$http = $http;
            this.httpHelper = httpHelper;
        }
        return AssignmentService;
    }());
    AssignmentService.$inject = ['$http', 'httpHelper'];
    angular.module('app').service('assignmentService', AssignmentService);
})(app || (app = {}));
//# sourceMappingURL=assignment.js.map