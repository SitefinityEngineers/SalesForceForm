SalesForceForm
==============
The content of the package is the following:

1.  SalesForceConfig

This is a new configuration section, which will appear in the advanced settings in the backend

2.	FormsControlDesignerCustom

The control designer is based on the default FormsControlDesigner. The layout has input fields for the data that we want to send to SalesForce. The script has several more important functions:

•	_pageLoadHandler: Initializes the controls, sets the initial “No mapping” value for the DDLs, registers the _selectItemHandler that will be triggered when you select an existing form from the list

•	_selectItemHandler: when a form is selected a request (getFields) is made that will get a list of all the fields for that form and set that set as the data source for the DDLs

•	getFields: calls a custom service that will return all the fields of a particular form in the <field_title>,<field_id> form

•	applyChanges: when the designer is closed the selected mappings (lead field->form field id) are persisted 

•	refreshUI: when the designer is reopened the previously persisted values are set as initial values for the DDLs

3.	FormsControlCustom

The control is based on the default Forms control. A new event handler is added to the submit button. That handler uses OAuth to authenticate the user in SalesForce, reads the data from the relevant fields from the form and sends a request to SalesForce to create a new lead. In the sample only the two required fields (Last Name, Company) are added to the control and its designer but you can add any additional relevant field.

4.	FormService

This is a custom service that retrieves all the fields of specific form using the forms id.
