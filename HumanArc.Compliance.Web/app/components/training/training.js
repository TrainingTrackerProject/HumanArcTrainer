var app;
(function (app) {
    'use strict';
    var TrainingService = (function () {
        function TrainingService($http, httpHelper) {
            this.$http = $http;
            this.httpHelper = httpHelper;
        }
        TrainingService.prototype.save = function (model) {
            var request = this.$http.post('/Training/Save', model);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        TrainingService.prototype.attachFile = function (id, fileData) {
            var request = this.$http.post('/Training/AttachFile/' + id, fileData, { transformRequest: angular.identity, headers: { 'Content-Type': undefined } });
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        TrainingService.prototype.getAll = function () {
            var request = this.$http.get('/Training/GetAll');
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        TrainingService.prototype.getById = function (id) {
            var request = this.$http.get('/Training/GetById/' + id);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        TrainingService.prototype.delete = function (id) {
            var request = this.$http.get('/Training/Delete/' + id);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        TrainingService.prototype.getQuestions = function (trainingId) {
            var request = this.$http.get('/Training/GetQuestions/' + trainingId);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        TrainingService.prototype.getQuestionById = function (questionId) {
            var request = this.$http.get('/Training/GetQuestionById/' + questionId);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        TrainingService.prototype.saveQuestion = function (question) {
            var request = this.$http.post('/Training/SaveQuestion', question);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        TrainingService.prototype.deleteQuestion = function (trainingId, questionId) {
            var request = this.$http.get('/Training/DeleteQuestion/' + trainingId + '/' + questionId);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        return TrainingService;
    }());
    TrainingService.$inject = ['$http', 'httpHelper'];
    angular.module('app').service('trainingService', TrainingService);
})(app || (app = {}));
//# sourceMappingURL=training.js.map