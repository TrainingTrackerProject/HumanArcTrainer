var app;
(function (app) {
    'use strict';
    var TrainingQuestionsController = (function () {
        function TrainingQuestionsController(questionId, trainingId, trainingService, toastrService, $uibModalInstance) {
            var _this = this;
            this.questionId = questionId;
            this.trainingId = trainingId;
            this.trainingService = trainingService;
            this.toastrService = toastrService;
            this.$uibModalInstance = $uibModalInstance;
            console.log("Loading Question if I can");
            if (questionId !== undefined && questionId !== null) {
                trainingService.getQuestionById(questionId).then(function (data) {
                    _this.question = data;
                });
            }
        }
        TrainingQuestionsController.prototype.save = function () {
            var _this = this;
            console.log("going to save now!");
            if (this.questionEditorForm.$valid) {
                this.question.TrainingId = this.trainingId;
                this.trainingService.saveQuestion(this.question).then(function (data) {
                    _this.question = data;
                    _this.toastrService.success('Question Save Successfully');
                    _this.$uibModalInstance.close();
                }, function (data) {
                    _this.toastrService.error(data);
                });
            }
        };
        return TrainingQuestionsController;
    }());
    TrainingQuestionsController.$inject = [
        'questionId',
        'trainingId',
        'trainingService',
        'toastrService',
        '$uibModalInstance'
    ];
    angular.module('app').controller('trainingQuestionsController', TrainingQuestionsController);
})(app || (app = {}));
//# sourceMappingURL=trainingQuestionsController.js.map