
if (!isDateInputSupported()) {
    $("#CSUItem_DateForCallback").datepicker({
        dateFormat: 'dd-mm-yy'           
    });
    // Force Format Change.
    $('#CSUItem_DateForCallback').datepicker("setDate", new Date($("#CSUItem_DateForCallback").val()));
}

(function ($) {
    $.validator.addMethod('DateNoGreater', function (value, element) {
        if (isDateInputSupported()) {
            if (new Date(value) > new Date()) {
                return true; // Return true or false depending on if it passes or fails validation, respectively.
            }
        } else {
            // date format is in dd-mm-yyyy at this point due to date picker 
            var x = value.split('-', 3);
            if (new Date(x[2] + '-' + x[1]+ '-' + x[0]) > new Date()) {
                return true; // Return true or false depending on if it passes or fails validation, respectively.
            }
        }
        return false;
    }, 'Date must be greater than today.');

}(jQuery));

$.validator.unobtrusive.adapters.addBool('DateNoGreater');

function createBegin() {
	$("#submitItem").attr("disabled", "disabled");
	$("#clearItem").attr("disabled", "disabled");
}

function onSuccess() {
	$("select").each(function () { this.selectedIndex = 0 });
	$("#submitItem").removeAttr("disabled");
    $("#clearItem").removeAttr("disabled"); 

    var today = new Date();
  
    toastMeNoFade("success", "A Callback for the CSU team has been created at " +  today, "Created");

	// renable validation
	$.validator.unobtrusive.parse('form');

}

function onError() {
	toastMe("error", "The callback for the CSU team could not be created", "Error");
	$("#submitItem").removeAttr("disabled");
	$("#clearItem").removeAttr("disabled");
}

$('.popover-dismiss').popover({
	trigger: 'focus'
});

function PostiveNumericInput() {
    var valuex = document.getElementById("CSUItem_ContactNumber").value;
    if (!(/^\d*\d*$/.test(valuex))) {
        document.getElementById("CSUItem_ContactNumber").value = valuex.slice(0, -1);
    }
}

function toastMe(toastType, toastMessage, toastTitle) {
	toastr.options = {
		"closeButton": true,
		"debug": false,
		"newestOnTop": false,
		"progressBar": true,
		"positionClass": "toast-top-right",
		"preventDuplicates": false,
		"onclick": null,
		"showDuration": "300",
		"hideDuration": "1000",
		"timeOut": "5000",
		"extendedTimeOut": "1000",
		"showEasing": "swing",
		"hideEasing": "linear",
		"showMethod": "fadeIn",
        "hideMethod": "fadeOut",
	}
	toastr[toastType](toastMessage, toastTitle);
}

function toastMeNoFade(toastType, toastMessage, toastTitle) {

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "50000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
    }
    toastr[toastType](toastMessage, toastTitle);
    
}


function CallerNameAutoFill(divobject, username) {
	//This method is called when the focus is out for the customer name, the value of the customer name will be the same as the caller

    switch (divobject)
    {
        case "Customer":
            if (username != null && username != '' && username != 'undefined') {
                $("#NameOfCaller").val(username);
                $('#NameOfCaller').prop('readonly', true);
                $("#NameOfCaller").css('background-color', 'white');
                $("#NameOfCaller").valid();
            }
			break;
        default:
            if ($("#NameOfCaller").val() == username) {
                RemoveDisableNameOfCaller();
            }
			break;
	}
}

function RemoveDisableNameOfCaller() {
    $("#NameOfCaller").val('');
    $('#NameOfCaller').prop('readonly', false);
    $("#NameOfCaller").css('background-color', 'white');
}

function healthissueCheck(checked) {
	if (checked != true) {
		$('#HealthIssuseInput').hide();
		$('#CSUItem_HealthIssue').val('');
	}
	else {
		$('#HealthIssuseInput').show();
		$('#CSUItem_HealthIssue').val('');
		scrollWin();
	}
}

function scrollWin() {
	window.scrollBy(0, 100);
}

function PlusMinusContInpt() {
	if ($("#ContactNumDropDown").attr("hidden")) {
		$("#ContactNumDropDown").attr("hidden", false);
		$("#ContactNumDropDown").attr("data-val", true);

		$("#CSUItem_ContactNumber").attr("hidden", true);
		$("#CSUItem_ContactNumber").attr("data-val", false);

		$("#PlusMinusContactNum").removeClass("btn-danger");
		$("#PlusMinusContactNum").addClass("btn-primary");

		$("#GlipMinusPlus").removeClass("fa-minus-circle");
		$("#GlipMinusPlus").addClass("fa-plus-circle");
	} else {
		$("#ContactNumDropDown").attr("hidden", true);
		$("#ContactNumDropDown").attr("data-val", false);

		$("#CSUItem_ContactNumber").attr("hidden", false);
		$("#CSUItem_ContactNumber").attr("data-val", true);

		$("#PlusMinusContactNum").removeClass("btn-primary");
		$("#PlusMinusContactNum").addClass("btn-danger");

		$("#GlipMinusPlus").removeClass("fa-plus-circle");
		$("#GlipMinusPlus").addClass("fa-minus-circle");

	}
}