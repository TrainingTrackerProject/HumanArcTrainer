var app;
(function (app) {
    'use strict';
    var HttpHelperService = (function () {
        function HttpHelperService($q) {
            var _this = this;
            this.$q = $q;
            this.handleError = function (response) {
                return _this.$q.reject(response.data);
            };
        }
        HttpHelperService.prototype.handleSuccess = function (response) {
            return response.data;
        };
        return HttpHelperService;
    }());
    HttpHelperService.$inject = ['$q'];
    app.HttpHelperService = HttpHelperService;
    angular.module('app').service('httpHelper', HttpHelperService);
})(app || (app = {}));
//# sourceMappingURL=httpHelper.js.map