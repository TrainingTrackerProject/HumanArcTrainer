var app;
(function (app) {
    'use strict';
    var ToastrService = (function () {
        function ToastrService() {
        }
        ToastrService.prototype.toastrDefaults = function () {
            var toastrOptions = {
                timeOut: 2000,
                extendedTimeOut: 1000,
                positionClass: "toast-top-right-content"
            };
            return toastrOptions;
        };
        ToastrService.prototype.success = function (text, title, optionsOverride) {
            this.toastrDefaults();
            toastr.success(text, title, optionsOverride);
        };
        ToastrService.prototype.info = function (text, title, optionsOverride) {
            this.toastrDefaults();
            toastr.info(text, title, optionsOverride);
        };
        ToastrService.prototype.warning = function (text, title, optionsOverride) {
            this.toastrDefaults();
            toastr.warning(text, title, optionsOverride);
        };
        ToastrService.prototype.error = function (text, title, optionsOverride) {
            this.toastrDefaults();
            toastr.error(text, title, optionsOverride);
        };
        return ToastrService;
    }());
    app.ToastrService = ToastrService;
    angular.module('app').service('toastrService', ToastrService);
})(app || (app = {}));
//# sourceMappingURL=toastr.js.map