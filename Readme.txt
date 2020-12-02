1) Below list needs to be created to run the solution :

Listname : Timesheet

Columns :

-> Hours (Required) (Type : Number)
-> Description (Optional) (Type : Multiline Rich Text Box)
-> Category (Required) (Type : Choice - Billable, Non-Billable, Upskilling, Meeting)
-> TimesheetDate (Required) (Type : DateTime)

2) Site collection level, feature named "TimesheetTracker Feature1" needs to be activated.

3) Below page needs to be created in site pages and add respected webpart and script:

-> Timesheet.aspx - Add "ManageTimesheet" webpart and below script in this page

======================================== Script Start =======================================================

<script language="javascript">

_spBodyOnLoadFunctionNames.push("HideBrandingsuite");

function HideBrandingsuite()

{

document.getElementById('pageTitle').style.visibility = 'hidden';

} 

</script>

======================================== Script End =======================================================


4) Home page of site should be "Home.aspx" and "ListTimesheet" webpart needs to be added.

5) On site's navigation, create one navigation "Add Timesheet" and set its navigation to page "Timesheet.aspx"

6) Edit "Home.aspx" file, add html content editor webpart and add below script to get welcome user title.


======================================== Script Start =======================================================

<div align="right"><h5><b>Welcome, <label id="lblUsername"></label></b></h5></div>

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
<script language="javascript">

_spBodyOnLoadFunctionNames.push("HideBrandingsuite");

function HideBrandingsuite()

{

document.getElementById('pageTitle').style.visibility = 'hidden';

} 

</script>

======================================== Script End =======================================================

7) In web.config file, define below key in app settings section for generating custom exception logs in file.

<add key="LogFilePath" value="[Directory path where custom log file will be generated]" />