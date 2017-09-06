var app;
(function (app) {
    'use strict';
    var TrainingFileController = (function () {
        function TrainingFileController(trainingService, toastrService, $location, $window, $state, $stateParams, $uibModal) {
            var _this = this;
            this.trainingService = trainingService;
            this.toastrService = toastrService;
            this.$location = $location;
            this.$window = $window;
            this.$state = $state;
            this.$stateParams = $stateParams;
            this.$uibModal = $uibModal;
            if (this.$stateParams["trainingId"]) {
                this.trainingId = this.$stateParams["trainingId"];
                trainingService.getById(this.trainingId).then(function (data) {
                    _this.training = data;
                });
            }
        }
        TrainingFileController.prototype.fileInputChanged = function (model) {
            var _this = this;
            if (model !== undefined && model !== null) {
                this.uploadInProgress = true;
                this.fileToUpload = model;
                var formData = new FormData();
                formData.append("file", this.fileToUpload);
                this.trainingService.attachFile(this.trainingId, formData).then(function (data) {
                    _this.uploadInProgress = false;
                    _this.toastrService.success('File Attached sucessfully, please proceed to the next step');
                    _this.training = data;
                }, function (data) {
                    _this.uploadInProgress = false;
                    _this.toastrService.error('File Upload Failed');
                });
            }
        };
        TrainingFileController.prototype.save = function () {
            var _this = this;
            if (this.trainingForm.$valid) {
                this.trainingService.save(this.training).then(function (data) {
                    _this.toastrService.success('Sucessfully Saved');
                    _this.training = data;
                }, function (data) {
                    _this.toastrService.error(data);
                });
            }
            else {
                console.log("Have form errors for you to fix");
            }
        };
        TrainingFileController.prototype.editQuestion = function (questionId) {
            var _this = this;
            var trainingId = this.$stateParams["trainingId"];
            var modalInstance = this.$uibModal.open({
                templateUrl: '/app/components/training/templates/question.html',
                controller: 'trainingQuestionsController as questionEditorCtrl',
                size: 'lg',
                resolve: {
                    questionId: function () { return questionId; },
                    trainingId: function () { return trainingId; }
                }
            });
            modalInstance.result.then(function (data) {
                if (data !== 'closed') {
                    _this.trainingService.getQuestions(trainingId).then(function (data) {
                        _this.training.Questions = data;
                    });
                }
            });
        };
        TrainingFileController.prototype.deleteQuestion = function (questionId) {
            var _this = this;
            var trainingId = this.$stateParams["trainingId"];
            this.trainingService.deleteQuestion(trainingId, questionId).then(function (data) {
                _this.toastrService.success('Question deleted');
                _this.training.Questions = data;
            }, function (data) {
                _this.toastrService.error(data);
            });
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
        '$uibModal'
    ];
    angular.module('app').controller('trainingEditController', TrainingFileController);
})(app || (app = {}));
//# sourceMappingURL=trainingEditController.js.map