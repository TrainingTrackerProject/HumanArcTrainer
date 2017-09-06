var app;
(function (app) {
    'use strict';
    var TrainingFileController = (function () {
        function TrainingFileController(trainingService, toastrService, $location, $window, $state, $stateParams, $sce, $uibModal, $scope) {
            this.trainingService = trainingService;
            this.toastrService = toastrService;
            this.$location = $location;
            this.$window = $window;
            this.$state = $state;
            this.$stateParams = $stateParams;
            this.$sce = $sce;
            this.$uibModal = $uibModal;
            this.$scope = $scope;
            this.fileName = "No file selected.";
            this.editing = false;
            this.hasFile = false;
            this.showForm = true;
            if (this.$stateParams["ID"] !== null && this.$stateParams["ID"]) {
                //trainingService.getTrainingById(this.$stateParams["ID"]).then((data) => {
                //    this.trainingId = data.ID;
                //    this.fileName = "Current File";
                //    this.trainingName = data.Name;
                //    this.showForm = true;
                //    this.training = data;
                //    this.editing = true;
                //    this.hasFile = true;
                //});
                this.showForm = false;
            }
        }
        TrainingFileController.prototype.fileInputChanged = function (element) {
            if (element !== undefined && element !== null) {
                this.fileName = element.name;
                console.log(element);
                this.hasFile = false;
            }
        };
        TrainingFileController.prototype.downloadTraining = function () {
            this.$window.open("/Training/DownloadFileByPath/" + this.$stateParams["ID"], "_blank");
        };
        TrainingFileController.prototype.nextStep = function () {
            console.log(this.hasFile);
            var formData = new FormData();
            if (this.hasFile === false) {
                formData.append("file", this.file);
                formData.append("HasTrainingFile", false);
            }
            else {
                formData.append("HasTrainingFile", true);
            }
            formData.append("Name", this.training.Name);
            if (this.$stateParams["ID"] !== null && this.$stateParams["ID"]) {
                formData.append("ID", this.$stateParams["ID"]);
            }
            //formData.append("FilePathToMedia", this.training.FilePathToMedia);
            //this.trainingService.attachFile(formData).then((data) => {
            //    this.trainingId = data;
            //    this.$state.go("training.questions", { ID: this.trainingId });
            //    //this.toastrService.success("Training Successfully Created");
            //}, (data) => {
            //    //this.toastrService.error(data);
            //});
        };
        return TrainingFileController;
    }());
    TrainingFileController.$inject = [
        'trainingService',
        'toastrService',
        '$location',
        '$window',
        '$state',
        '$stateParams',
        '$sce',
        '$uibModal',
        '$scope'
    ];
    angular.module('app').controller('trainingFileController', TrainingFileController);
})(app || (app = {}));
//# sourceMappingURL=trainingFileController.js.map