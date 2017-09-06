module app {
    'use strict';
    class TrainingFileController {
        trainingFileForm: ng.IFormController;
        fileName: string = "No file selected.";
        trainingId: any;
        trainingName: any;
        showForm: any;
        linkToBlob: any;
        training: any;
        file: any;
        editing: any = false;
        hasFile: any = false;

        static $inject =
        [
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
        constructor(private trainingService: ITrainingService,
            private toastrService: IToastrService,
            public $location: ng.ILocationService,
            public $window: ng.IWindowService,
            public $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService,
            public $sce: ng.ISCEService,
            private $uibModal: ng.ui.bootstrap.IModalService,
            public $scope: ng.IScope) {
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

        fileInputChanged(element) {
            if (element !== undefined && element !== null) {
                this.fileName = element.name;
                console.log(element);
                this.hasFile = false;
            }
        }

        downloadTraining() {
            this.$window.open("/Training/DownloadFileByPath/" + this.$stateParams["ID"], "_blank");
        }

        nextStep() {
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
        }

    }

    angular.module('app').controller('trainingFileController', TrainingFileController);
}