var app;
(function (app) {
    var NameValueService = (function () {
        function NameValueService($http, httpHelper) {
            this.$http = $http;
            this.httpHelper = httpHelper;
        }
        NameValueService.prototype.getNameValueList = function (url) {
            return this.$http.get(url, { cache: true })
                .then(this.httpHelper.handleSuccess, this.httpHelper.handleError);
        };
        NameValueService.prototype.getRoles = function () {
            return this.getNameValueList('/NameValue/GetRoles/');
        };
        NameValueService.prototype.getGroupsList = function () {
            return this.getNameValueList('/NameValue/GetGroupList/');
        };
        return NameValueService;
    }());
    NameValueService.$inject = ['$http', 'httpHelper'];
    app.NameValueService = NameValueService;
    angular.module('app').service('nameValueService', NameValueService);
})(app || (app = {}));
//# sourceMappingURL=nameValue.js.map