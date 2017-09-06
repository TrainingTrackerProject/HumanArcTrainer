var app;
(function (app) {
    'use strict';
    var AssignmentController = (function () {
        function AssignmentController($stateParams, authorizationService, toastrService) {
            this.$stateParams = $stateParams;
            this.authorizationService = authorizationService;
            this.toastrService = toastrService;
            this.trainingId = $stateParams["trainingId"];
        }
        return AssignmentController;
    }());
    AssignmentController.$inject = ['$stateParams', 'assignmentService', 'toastrService'];
    angular.module('app').controller('assignmentController', AssignmentController);
})(app || (app = {}));
//# sourceMappingURL=assignmentController.js.map