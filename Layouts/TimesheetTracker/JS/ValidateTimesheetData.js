function ValidateSubmit() {
    var hoursValue = document.getElementById("<%=txtHours.ClientID%>").value;
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

    var category = document.getElementById("<%=ddlCategory.ClientID%>").value;
    if (category == 0) {
        alert("Select Category.");
        return false;
    }

    return true;
}