/*
 * AngularJS code for the Quiz application, called on Quiz.cshtml.
 */

app = angular.module("QuizApp", []);
var json = {
    title: 'my title',
    description: 'myDesc',
    allQuestions: [{
        text: 'This is question 1',
        type: 'multipleChoice',
        answers: [{
            text: 'a1',
            isCorrect: true
        },
        {
            text: 'a2',
            isCorrect: false
        },
        {
            text: 'a3',
            isCorrect: false
        },
        {
            text: 'a4',
            isCorrect: false
        }]
    },
    {
        text: 'This is question 2',
        type: 'trueFalse',
        answers: [{
            text: 'True',
            isCorrect: true
        },
        {
            text: 'False',
            isCorrect: false
        }]
    },
    {
        text: 'This is question 3',
        type: 'shortAnswer'
    }
    ]
}
app.controller("QuizCtrl", function ($scope) {
    $scope.isArray = angular.isArray;
    $scope.json = json;
})