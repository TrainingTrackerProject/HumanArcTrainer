module app {
    'use strict';

    class TrainingQuestionsController {
        questionEditorForm: ng.IFormController;
        question: any;

        static $inject =
        [
            'questionId',
            'trainingId',
            'trainingService',
            'toastrService',
            '$uibModalInstance'
        ];

        constructor
            (
            private questionId,
            private trainingId,
            private trainingService: ITrainingService,
            private toastrService: IToastrService,
            private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) {
            console.log("Loading Question if I can");
            if (questionId !== undefined && questionId !== null) {
                trainingService.getQuestionById(questionId).then((data) => {
                     this.question = data;
                });
            }
        }

        save() {
            console.log("going to save now!");
            if (this.questionEditorForm.$valid) {
                this.question.TrainingId = this.trainingId;
                this.trainingService.saveQuestion(this.question).then((data) => {
                    this.question = data;
                    this.toastrService.success('Question Save Successfully');
                    this.$uibModalInstance.close();

                }, (data) => {
                    this.toastrService.error(data);
                });
            }

        }

    }
    angular.module('app').controller('trainingQuestionsController', TrainingQuestionsController);
}


