﻿bookingApp.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {

                event.preventDefault();
            }
        });
    };
})