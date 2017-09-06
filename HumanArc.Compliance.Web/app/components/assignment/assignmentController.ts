module app {
    'use strict';

    class AssignmentController {
        trainingId: number;

        static $inject = ['$stateParams', 'assignmentService', 'toastrService'];
        constructor(
            private $stateParams: ng.ui.IStateParamsService,
            private authorizationService: IAssignmentService,
            private toastrService: IToastrService) {

            this.trainingId = $stateParams["trainingId"];
        }
    }

    angular.module('app').controller('assignmentController', AssignmentController);
}