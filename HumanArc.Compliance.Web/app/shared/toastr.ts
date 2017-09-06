module app {
    'use strict';

    export interface IToastrService {
        toastrDefaults(): ToastrOptions;
        success: (text: string, title?: string, optionsOverride?) => void;
        info: (text: string, title?: string, optionsOverride?) => void;
        warning: (text: string, title?: string, optionsOverride?) => void;
        error: (text: string, title?: string, optionsOverride?) => void;
    }

    export class ToastrService implements IToastrService {

        toastrDefaults(): ToastrOptions {
            var toastrOptions: ToastrOptions = {
                timeOut: 2000,
                extendedTimeOut: 1000,
                positionClass: "toast-top-right-content"
            };

            return toastrOptions;
        }

        success(text, title, optionsOverride) {
            this.toastrDefaults();
            toastr.success(text, title, optionsOverride);
        }

        info(text, title, optionsOverride) {
            this.toastrDefaults();
            toastr.info(text, title, optionsOverride);
        }

        warning(text, title, optionsOverride) {
            this.toastrDefaults();
            toastr.warning(text, title, optionsOverride);
        }

        error(text, title, optionsOverride) {
            this.toastrDefaults();
            toastr.error(text, title, optionsOverride);
        }
    }

    angular.module('app').service('toastrService', ToastrService);
}
