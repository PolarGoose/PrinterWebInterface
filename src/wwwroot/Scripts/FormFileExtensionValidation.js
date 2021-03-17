$.validator.addMethod('formfileextension', function (value, element, params) {
    var allowedExtensions = params.split(", ");
    var extension = "." + value.split('.').pop();

    var index = allowedExtensions.findIndex(e => e == extension)
    return index > -1;
});

$.validator.unobtrusive.adapters.add('formfileextension', ['allowedextensions'], function (options) {

    options.rules['formfileextension'] = options.params['allowedextensions'];
    options.messages['formfileextension'] = options.message;
});
