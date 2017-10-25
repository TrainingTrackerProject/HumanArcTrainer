/*
Test code for the Quiz application, called on Quiz.cshtml.
*/

var Myapp = angular.module('Quizapp', ['ngRoute']);
Myapp.controller('QuizCtrl', function ($scope, $http, $getQuestion) {
    scope.QuestionAnswer = []
    scope.count = 0;
    scope.correctAns = 0;
    scope.checkedCount = 0;
    scope.notvisible = true;
    scope.questionsvisible = true;
    //get the Question and answers from the controller and pass it to array  
    scope.rbshow = false;
    getQuestion.result().success(function (response) {
        for (var i = 0; i < response.length; i++) {
            scope.QuestionAnswer.push(response[i]);
        }
    });
    scope.nextQuestion = function () {
        scope.rbshow = true;
        var nextQuestion = scope.count; //To get next question  
        var answer = "";
        var choicesArr = document.getElementsByName('choices');
        for (var i = 0; i < choicesArr.length; i++) {
            if (choicesArr[i].checked) {
                scope.checkedCount = scope.checkedCount + 1;
                answer = scope.QuestionAnswer[nextQuestion - 1].Ans;
                if (choicesArr[i].value === answer) {
                    scope.correctAns = scope.correctAns + 1;
                }
                choicesArr[i].checked = false;
            }
        }
        if (scope.checkedCount === scope.QuestionAnswer.length) {
            scope.notvisible = false;
            scope.questionsvisible = false;
        }
        else if (scope.QuestionAnswer.length >= scope.count) {
            scope.QuestionAnswer.Question = scope.QuestionAnswer[nextQuestion].Question;
            scope.QuestionAnswer.OptionA = scope.QuestionAnswer[nextQuestion].OpA;
            scope.QuestionAnswer.OptionB = scope.QuestionAnswer[nextQuestion].OpB;
            scope.QuestionAnswer.OptionC = scope.QuestionAnswer[nextQuestion].OpC;
            scope.QuestionAnswer.OptionD = scope.QuestionAnswer[nextQuestion].OpD;
            scope.count = scope.count + 1;
        }
    }
});
//http get to retrive the questions from Home Controller QuizQuestionAns Action //Methood  
Myapp.service('getQuestion', function ($http) {
    this.result = function () {
        var ans = $http.get('~/Training/QuizQuestionAns');
        return ans;
    }
});
//Directive with radio button to select the option  
//Submit button to go to next question  
//This directive displays the question along with the options to select  
Myapp.directive("questionContainer", function () {
    return {
        template: '<div>{{QuestionAnswer.Question}}<br/>'
        + '<input name="choices" name="choices" type="radio" value="A" ng-show="rbshow" />{{QuestionAnswer.OptionA}}<br/>'
        + '<input name="choices" name="choices" type="radio" value="B" ng-show="rbshow" />{{QuestionAnswer.OptionB}}<br/>'
        + '<input name="choices" name="choices" type="radio" value="C" ng-show="rbshow" />{{QuestionAnswer.OptionC}}</br>'
        + '<input name="choices" name="choices" type="radio" value="D" ng-show="rbshow" />{{QuestionAnswer.OptionD}}</br>'
        + ' <input type="button" ng-click="nextQuestion()" value="Next"/>' + '</div>',
        restrict: "E"
    }
});
//Directive is shown at last once the quiz is completed with the total number of correct answers  
Myapp.directive("correctContainer", function () {
    return {
        template: '<div>Total Correct Answer {{correctAns}}<br/>',
        restrict: "E"
    }
});
