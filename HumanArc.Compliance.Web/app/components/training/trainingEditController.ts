module app {
    'use strict';

    class TrainingFileController {
        trainingForm: ng.IFormController;
        training: any;
        trainingId: number;
        fileToUpload: any;
        uploadInProgress: boolean;

        static $inject =
        [
            'trainingService',
            'toastrService',
            '$location',
            '$window',
            '$state',
            '$stateParams',
            '$uibModal'
        ];
        constructor(private trainingService: ITrainingService,
            private toastrService: IToastrService,
            public $location: ng.ILocationService,
            public $window: ng.IWindowService,
            public $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService,
            private $uibModal: ng.ui.bootstrap.IModalService) {

            if (this.$stateParams["trainingId"]) {
                this.trainingId = this.$stateParams["trainingId"];
                trainingService.getById(this.trainingId).then((data) => {
                    this.training = data;
                });
            }
        }

        fileInputChanged(model) {
            if (model !== undefined && model !== null) {
                this.uploadInProgress = true;
                this.fileToUpload = model;
                var formData = new FormData();
                formData.append("file", this.fileToUpload);
                this.trainingService.attachFile(this.trainingId, formData).then((data) => {
                    this.uploadInProgress = false;
                    this.toastrService.success('File Attached sucessfully, please proceed to the next step');
                    this.training = data;
                }, (data) => {
                    this.uploadInProgress = false;
                    this.toastrService.error('File Upload Failed');
                });
            }
        }

        save() {
            if (this.trainingForm.$valid) {
                this.trainingService.save(this.training).then((data) => {
                    this.toastrService.success('Sucessfully Saved');
                    this.training = data;
                }, (data) => {
                    this.toastrService.error(data);
                });
            } else {
                console.log("Have form errors for you to fix");
            }
        }

        editQuestion(questionId) {
            var trainingId = this.$stateParams["trainingId"];
            var modalInstance = this.$uibModal.open({
                templateUrl: '/app/components/training/templates/question.html',
                controller: 'trainingQuestionsController as questionEditorCtrl',
                size: 'lg',
                resolve: {
                    questionId() { return questionId; },
                    trainingId() { return trainingId; }
                }
            });

            modalInstance.result.then((data) => {
                if (data !== 'closed') {
                    this.trainingService.getQuestions(trainingId).then((data) => {
                        this.training.Questions = data;
                    });
                }
            });
        }

        deleteQuestion(questionId) {
            var trainingId = this.$stateParams["trainingId"];
            this.trainingService.deleteQuestion(trainingId, questionId).then((data) => {
                this.toastrService.success('Question deleted');
                this.training.Questions = data;
            }, (data) => {
                this.toastrService.error(data);
            });
        }
    }

    angular.module('app').controller('trainingEditController', TrainingFileController);
}