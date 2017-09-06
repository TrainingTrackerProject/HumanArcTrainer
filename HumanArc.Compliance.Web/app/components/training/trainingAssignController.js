var app;
(function (app) {
    'use strict';
    var TrainingAssignController = (function () {
        function TrainingAssignController(groupList, toastrService, trainingService, $state, $stateParams) {
            this.groupList = groupList;
            this.toastrService = toastrService;
            this.trainingService = trainingService;
            this.$state = $state;
            this.$stateParams = $stateParams;
            this.trainingId = this.$stateParams["ID"];
            this.selectedList = [];
        }
        TrainingAssignController.prototype.moveItem = function (item, from, to, isAdd) {
            var idx = from.indexOf(item);
            console.log(item.Value);
            if (idx != -1) {
                if (isAdd === true) {
                    this.addGroup(item.Value);
                }
                else {
                    this.deleteGroup(item.Value);
                }
                from.splice(idx, 1);
                to.push(item);
                this.selectedGroup = "";
            }
        };
        TrainingAssignController.prototype.addGroup = function (groupName) {
            this.adGroup = {};
            this.adGroup.GroupName = groupName;
            this.adGroup.trainingID = this.$stateParams["ID"];
            //this.trainingService.addGroup(this.adGroup).then((data) => {
            //    console.log(data);
            //    //this.toastrService.success(data);
            //}, ((data) => {
            //    //this.toastrService.error(data);
            //})
            //);
        };
        TrainingAssignController.prototype.deleteGroup = function (groupName) {
            this.adGroup = {};
            this.adGroup.GroupName = groupName;
            this.adGroup.trainingID = this.$stateParams["ID"];
            //this.trainingService.deleteGroup(this.adGroup).then((data) => {
            //    //this.toastrService.success(data);
            //}, ((data) => {
            //    //this.toastrService.error(data);
            //})
            //);
        };
        TrainingAssignController.prototype.finish = function () {
            this.$state.go("training");
        };
        return TrainingAssignController;
    }());
    TrainingAssignController.$inject = [
        'groupList',
        'toastrService',
        'trainingService',
        '$state',
        '$stateParams',
    ];
    angular.module('app').controller('trainingAssignController', TrainingAssignController);
})(app || (app = {}));
//# sourceMappingURL=trainingAssignController.js.map