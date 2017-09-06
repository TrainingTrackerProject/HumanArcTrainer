module app {
    'use strict';

    export interface ITrainingService {
        getAll(): any;
        save(model: any): any;
        getById(id: number): any;
        delete(id: number): any;
        attachFile(id: number, fileData): any;
        getQuestions(trainingId: number): any;
        getQuestionById(questionId: number): any;
        saveQuestion(question: any): any;
        deleteQuestion(trainingId: number, questionId: number): any;
    }

    class TrainingService implements ITrainingService {
        static $inject = ['$http', 'httpHelper'];

        constructor(private $http: ng.IHttpService, private httpHelper: IHttpHelperService) {}

        save(model) {
            var request = this.$http.post('/Training/Save', model);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        attachFile(id: number, fileData) {
            var request = this.$http.post('/Training/AttachFile/' + id, fileData, { transformRequest: angular.identity, headers: { 'Content-Type': undefined } });
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        getAll() {
            var request = this.$http.get('/Training/GetAll');
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        getById(id: number) {
            var request = this.$http.get('/Training/GetById/' + id);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        delete(id: number) {
            var request = this.$http.get('/Training/Delete/' + id);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        getQuestions(trainingId: number) {
            var request = this.$http.get('/Training/GetQuestions/' + trainingId);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        getQuestionById(questionId: number) {
            var request = this.$http.get('/Training/GetQuestionById/' + questionId);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        saveQuestion(question: any) {
            var request = this.$http.post('/Training/SaveQuestion', question);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }

        deleteQuestion(trainingId: number, questionId: number) {
            var request = this.$http.get('/Training/DeleteQuestion/' + trainingId + '/' + questionId);
            return request.then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        }
    }

    angular.module('app').service('trainingService', TrainingService);
}