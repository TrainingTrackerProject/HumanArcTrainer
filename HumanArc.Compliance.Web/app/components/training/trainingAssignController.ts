module app {
    'use strict';

    class TrainingAssignController {
        trainingAssignForm: ng.IFormController;
        adGroup: any;
        selectedGroup: any;
        trainingId: any;
        groupOptions: any;
        assignedGroup: any;
        selectedList: any[];
        available: any;
        
        static $inject =
        [
            'groupList',
            'toastrService',
            'trainingService',
            '$state',
            '$stateParams',
        ];
        constructor
            (
            private groupList,
            private toastrService: IToastrService,
            private trainingService: ITrainingService,
            public $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService
        ) {
            this.trainingId = this.$stateParams["ID"];
            this.selectedList = [];
        }

        moveItem(item, from, to, isAdd) {
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
        }

        addGroup(groupName) {
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
        }

        deleteGroup(groupName) {
            this.adGroup = {};
            this.adGroup.GroupName = groupName;
            this.adGroup.trainingID = this.$stateParams["ID"];
            //this.trainingService.deleteGroup(this.adGroup).then((data) => {
            //    //this.toastrService.success(data);
            //}, ((data) => {
            //    //this.toastrService.error(data);
            //})
            //);
        }

        finish() {
            this.$state.go("training");
        }
    }
    angular.module('app').controller('trainingAssignController', TrainingAssignController);
}


