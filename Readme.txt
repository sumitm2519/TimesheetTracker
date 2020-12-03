Following steps are necessary to perform as a part of configuration of this webpart :

1) Timesheet list needs to be created with defined columns to run the solution :

Listname : Timesheet

Columns :

-> Hours (Required) (Type : Number)
-> Description (Optional) (Type : Multiline Rich Text Box)
-> Category (Required) (Type : Choice - Billable, Non-Billable, Upskilling, Meeting)
-> TimesheetDate (Required) (Type : DateTime)

2) On site collection level, feature named "Employee Timesheet Tracker" needs to be activated.

3) On your page, add below webpart:

-> "ManageTimesheet"

4) In web.config file, define below keys in app settings section for generating custom exception logs in file and to get list name (here listname = Timesheet).

<add key="LogFilePath" value="[Directory path where custom log file will be generated]" />
<add key="TimesheetList" value="Timesheet" />