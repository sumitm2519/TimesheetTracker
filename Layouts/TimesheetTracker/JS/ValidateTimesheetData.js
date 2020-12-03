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