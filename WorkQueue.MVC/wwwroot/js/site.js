// Write your JavaScript code.
var isDateInputSupported = function () {
    var elem = document.createElement('input');
    elem.setAttribute('type', 'date');
    elem.value = 'foo';
    return (elem.type == 'date' && elem.value != 'foo');
};

