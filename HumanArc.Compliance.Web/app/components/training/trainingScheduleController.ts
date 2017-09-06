module app {
    'use strict';

    class TrainingScheduleController {
        trainingScheduleForm: ng.IFormController;
        schedule: any;
        selectedGroup: any;
        groupOptions: any;
        assignedGroup: any;
        selectedList: any[];
        available: any;
        adGroup: any;
        trainingId: any;

        static $inject =
        [
            'toastrService',
            'trainingService',
            'emailFrequencyList',
            'trainingFrequencyList',
            '$state',
            '$stateParams',
        ];
        constructor
            (
            private toastrService: IToastrService,
            private trainingService: ITrainingService,
            private emailFrequencyList,
            private trainingFrequencyList,
            public $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService) {
            this.selectedList = [];
            this.schedule = {
                TrainingGroup: []
            }
            this.trainingId = this.$stateParams["ID"];
            //if (this.$stateParams["ID"] !== null && this.$stateParams["ID"]) {
            //    trainingService.getScheduleById(this.$stateParams["ID"]).then((data) => {
            //        console.log(data);
            //        this.schedule.emailFrequency = data.EmailFrequency;
            //        this.schedule.trainingFrequency = data.TrainingFrequency;
            //        this.schedule.StartDay = data.StartDay;
            //        this.schedule.EndDay = data.EndDay;
            //    });
            //}
        }

        nextStep() {
            //this.schedule.TrainingID = this.$stateParams["ID"];
            //this.trainingService.attachSchedule(this.schedule).then((data) => {
            //    this.toastrService.success("Training Scheduled Successfully");
            //    this.$state.go("training.assign", { ID: this.schedule.TrainingID });
            //}, (data) => {
            //    this.toastrService.error("Training Scheduling Failed");
            //});
        }
    }
    angular.module('app').controller('trainingScheduleController', TrainingScheduleController);
}


