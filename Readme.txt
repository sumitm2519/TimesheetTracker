1) Below list needs to be created to run the solution :

Listname : Timesheet

Columns :

-> Hours (Required) (Type : Number)
-> Description (Optional) (Type : Multiline Rich Text Box)
-> Category (Required) (Type : Choice - Billable, Non-Billable, Upskilling, Meeting)
-> TimesheetDate (Required) (Type : DateTime)

2) Site collection level, feature named "TimesheetTracker Feature1" needs to be activated.

3) Below pages needs to be created in site pages and add respected webparts:

-> EditTimesheet.aspx - Add "EditTimesheet" webpart in this page.
-> ViewTimesheet.aspx - Add "ViewTimesheet" webpart in this page.
-> NewTimesheet.aspx - Add "NewTimesheet" webpart in this page.

4) Home page of site should be "Home.aspx" and  "ListTimesheet" webpart needs to be added.

5) On site's navigation, create one navigation "Add Timesheet" and set its navigation to page "NewTimesheet.aspx"

6) Edit "Home.aspx" file, add html content editor webpart and add below script to get welcome user title.


======================================== Script Start =======================================================

<div align="right"><h2><b>Welcome, <div><label id="lblUsername"></label></div></b></h2></div>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>

$(document).ready(function () {

var userid = _spPageContextInfo.userId;

function GetCurrentUser() {
var requestUri = _spPageContextInfo.webAbsoluteUrl + "/_api/web/getuserbyid(" + userid + ")";

var requestHeaders = { "accept" : "application/json;odata=verbose" };

$.ajax({
  url : requestUri,
  contentType : "application/json;odata=verbose",
  headers : requestHeaders,
  success : onSuccess,
  error : onError
});
}

function onSuccess(data, request){
  var loginName = data.d.Title;

document.getElementById("lblUsername").innerText =loginName;

}

function onError(error) {
  alert(error);
}

GetCurrentUser();

});</script>

======================================== Script End =======================================================
