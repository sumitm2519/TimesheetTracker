$(document).ready(function () {

    var userid = _spPageContextInfo.userId;

    function GetCurrentUser() {
        var requestUri = _spPageContextInfo.webAbsoluteUrl + "/_api/web/getuserbyid(" + userid + ")";

        var requestHeaders = { "accept": "application/json;odata=verbose" };

        $.ajax({
            url: requestUri,
            contentType: "application/json;odata=verbose",
            headers: requestHeaders,
            success: onSuccess,
            error: onError
        });
    }

    function onSuccess(data, request) {
        var loginName = data.d.Title;

        document.getElementById("lblUsername").innerText = loginName;

    }

    function onError(error) {
        alert(error);
    }

    GetCurrentUser();

});

_spBodyOnLoadFunctionNames.push("HideBrandingsuite");
function HideBrandingsuite() {
    document.getElementById('pageTitle').style.visibility = 'hidden';
}
function ValidateSubmit() {
    var timesheetDate = $('input[id*="dtDate"]').val();
    if (timesheetDate == '') {
        alert("Enter timesheet date.");
        return false;
    }
    else {
        var date_regex1 = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/;
        var date_regex2 = /^([1-9]|1[0-2])\/([1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/;
        if (!(date_regex1.test(timesheetDate)) && !(date_regex2.test(timesheetDate))) {
            alert("Enter valid timesheet date.");
            return false;
        }
    }
    var hoursValue = $('input[id*="txtHours"]').val();
    if (hoursValue != "") {
        if (hoursValue.match(/^-?\d*(\.\d+)?$/)) {

            if (hoursValue <= 0) {
                alert("Enter valid hours.");
                return false;
            }

            if (hoursValue > 8) {
                alert("Timesheet hours should not be more than 8 hours.");
                return false;
            }
        }
        else {
            alert("Enter valid hours.");
            return false;
        }
    }
    else {
        alert("Enter Timesheet hours.");
        return false;
    }

    var category = $('select[id*="ddlCategory"]').val();
    if (category == 0) {
        alert("Select Category.");
        return false;
    }

    return true;
}

function ConfirmDeletion() {
    if (confirm("Are you sure you want to delete timesheet entry?"))
        return true;
    else
        return false;
}