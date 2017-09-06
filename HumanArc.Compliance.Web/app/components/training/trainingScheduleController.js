var app;
(function (app) {
    'use strict';
    var TrainingScheduleController = (function () {
        function TrainingScheduleController(toastrService, trainingService, emailFrequencyList, trainingFrequencyList, $state, $stateParams) {
            this.toastrService = toastrService;
            this.trainingService = trainingService;
            this.emailFrequencyList = emailFrequencyList;
            this.trainingFrequencyList = trainingFrequencyList;
            this.$state = $state;
            this.$stateParams = $stateParams;
            this.selectedList = [];
            this.schedule = {
                TrainingGroup: []
            };
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
        TrainingScheduleController.prototype.nextStep = function () {
            //this.schedule.TrainingID = this.$stateParams["ID"];
            //this.trainingService.attachSchedule(this.schedule).then((data) => {
            //    this.toastrService.success("Training Scheduled Successfully");
            //    this.$state.go("training.assign", { ID: this.schedule.TrainingID });
            //}, (data) => {
            //    this.toastrService.error("Training Scheduling Failed");
            //});
        };
        return TrainingScheduleController;
    }());
    TrainingScheduleController.$inject = [
        'toastrService',
        'trainingService',
        'emailFrequencyList',
        'trainingFrequencyList',
        '$state',
        '$stateParams',
    ];
    angular.module('app').controller('trainingScheduleController', TrainingScheduleController);
})(app || (app = {}));
//# sourceMappingURL=trainingScheduleController.js.map