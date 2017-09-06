module app {
    'use strict';

    class TrainingFileController {
        trainingForm: ng.IFormController;
        training: any;
        trainings: any;
        trainingId: number;

        static $inject =
        [
            'trainingService',
            'toastrService',
            '$location',
            '$window',
            '$state'
        ];
        constructor(private trainingService: ITrainingService,
            private toastrService: IToastrService,
            public $location: ng.ILocationService,
            public $window: ng.IWindowService,
            public $state: ng.ui.IStateService) {

            // Load all trainings for Display
            trainingService.getAll().then((data) => {
                this.trainings = data;
            });
        }

        delete(trainingId) {
            this.trainingService.delete(trainingId).then((data) => {
                this.toastrService.success("Successfully Removed Training");
                this.trainingService.getAll().then((data) => {
                    this.trainings = data;
                });
            }, ((data) => {
                this.toastrService.error(data);
            }));
        }
    }

    angular.module('app').controller('trainingController', TrainingFileController);
}