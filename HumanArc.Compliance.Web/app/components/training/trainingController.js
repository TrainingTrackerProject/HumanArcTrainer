var app;
(function (app) {
    'use strict';
    var TrainingFileController = (function () {
        function TrainingFileController(trainingService, toastrService, $location, $window, $state) {
            var _this = this;
            this.trainingService = trainingService;
            this.toastrService = toastrService;
            this.$location = $location;
            this.$window = $window;
            this.$state = $state;
            // Load all trainings for Display
            trainingService.getAll().then(function (data) {
                _this.trainings = data;
            });
        }
        TrainingFileController.prototype.delete = function (trainingId) {
            var _this = this;
            this.trainingService.delete(trainingId).then(function (data) {
                _this.toastrService.success("Successfully Removed Training");
                _this.trainingService.getAll().then(function (data) {
                    _this.trainings = data;
                });
            }, (function (data) {
                _this.toastrService.error(data);
            }));
        };
        return TrainingFileController;
    }());
    TrainingFileController.$inject = [
        'trainingService',
        'toastrService',
        '$location',
        '$window',
        '$state'
    ];
    angular.module('app').controller('trainingController', TrainingFileController);
})(app || (app = {}));
//# sourceMappingURL=trainingController.js.map