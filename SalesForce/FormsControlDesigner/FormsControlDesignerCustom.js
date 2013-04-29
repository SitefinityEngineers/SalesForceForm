Type.registerNamespace("SitefinityWebApp.SalesForce.FormsControlDesigner");

SitefinityWebApp.SalesForce.FormsControlDesigner.FormsControlDesignerCustom = function (element) {
    SitefinityWebApp.SalesForce.FormsControlDesigner.FormsControlDesignerCustom.initializeBase(this, [element]);
    this._propertyEditor = null;
    this._controlData = null;
    this._contentSelector = null;
    this._selectedFormId = null;
    this._company = null;
    this._lastName = null;
}

SitefinityWebApp.SalesForce.FormsControlDesigner.FormsControlDesignerCustom.prototype = {
    initialize: function () {
        SitefinityWebApp.SalesForce.FormsControlDesigner.FormsControlDesignerCustom.callBaseMethod(this, 'initialize');
        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoadHandler);
        Sys.Application.add_load(this._pageLoadDelegate);
    },
    dispose: function () {
        if (this._itemSelectedDelegate) {
            delete this._itemSelectedDelegate;
        }

        SitefinityWebApp.SalesForce.FormsControlDesigner.FormsControlDesignerCustom.callBaseMethod(this, 'dispose');
    },
    refreshUI: function () {
        SitefinityWebApp.SalesForce.FormsControlDesigner.FormsControlDesignerCustom.callBaseMethod(this, 'refreshUI');
        var controlData = this.get_controlData();
        $("input.inputField").data("kendoDropDownList");

        if (typeof controlData.FieldLastName != 'undefined' && controlData.FieldLastName != "" && $("#lastname").length)
            $("#lastname").data("kendoDropDownList").select(function (dataItem) {
                return dataItem.Value === controlData.FieldLastName;
            });
        if (typeof controlData.FieldCompany != 'undefined' && controlData.FieldCompany != "" && $("#company").length)
            $("#company").data("kendoDropDownList").select(function (dataItem) {
                return dataItem.Value === controlData.FieldCompany;
            });
    },
    applyChanges: function () {
        SitefinityWebApp.SalesForce.FormsControlDesigner.FormsControlDesignerCustom.callBaseMethod(this, 'applyChanges');
        var controlData = this._propertyEditor.get_control();
        controlData.FieldLastName = $("#lastName").data("kendoDropDownList").value();
        controlData.FieldCompany = $("#company").data("kendoDropDownList").value();
    },

    _pageLoadHandler: function () {
        this._itemSelectedDelegate = Function.createDelegate(this, this._selectItemHandler);
        this._contentSelector._itemSelector.add_itemSelected(this._itemSelectedDelegate);
        var controlData = this._propertyEditor.get_control();
        dataSource = new kendo.data.DataSource({
            data: { text: "No Mapping", value: "No Mapping" }
        });

        $("#lastName").kendoDropDownList({
            dataTextField: "Text",
            dataValueField: "Value",
            dataSource: dataSource,
            enable: false,
            index: 0
        });
        var lastName = $("#lastName").data("kendoDropDownList");
        if (typeof controlData.FieldLastName != 'undefined' && controlData.FieldLastName != "" && controlData.FormId != "00000000-0000-0000-0000-000000000000") {
            lastName.setDataSource(this.getFields(controlData.FormId));
            lastName.select(function (dataItem) {
                return dataItem.Value === controlData.FieldLastName;
            })
        } else {
            lastName.select(0);
        }
        $("#company").kendoDropDownList({
            dataTextField: "Text",
            dataValueField: "Value",
            dataSource: dataSource,
            enable: false,
            index: 0
        });


    },

    _selectItemHandler: function () {
        var selectedItems = this.get_contentSelector().getSelectedItems();
        if (selectedItems != null) {
            if (selectedItems.length > 0) {
                var controlData = this._propertyEditor.get_control();
                var dict = [];
                controlData.Mappings = dict;
                var choiceItems = this.getFields(selectedItems[0].Id);

                var lastName = $("#lastName").data("kendoDropDownList");
                var dataSource = new kendo.data.DataSource({
                    data: choiceItems
                });
                lastName.setDataSource(dataSource);
                lastName.enable(true);
                if (typeof controlData.FieldLastName != 'undefined' && controlData.FieldLastName != "" && controlData.FormId != "00000000-0000-0000-0000-000000000000") {
                    lastName.select(function (dataItem) {
                        return dataItem.Value === controlData.FieldLastName;
                    })
                } else {
                    lastName.select(0);
                }

                var company = $("#company").data("kendoDropDownList");
                dataSource = new kendo.data.DataSource({
                    data: choiceItems
                });
                company.setDataSource(dataSource);
                if (typeof controlData.FieldCompany != 'undefined' && controlData.FieldCompany != "" && controlData.FormId != "00000000-0000-0000-0000-000000000000") {
                    company.select(function (dataItem) {
                        return dataItem.Value === controlData.FieldCompany;
                    });
                } else {
                    company.select(0);
                }
                company.enable(true);
            }
        }


    },

    /* --------------------------------- private methods --------------------------------- */

    getFields: function (id) {
        var fields;
        var formsServiceUrl = document.URL.split("/Sitefinity")[0] + "/SalesForce/FormsService/FormsServiceCustom.svc/" + id;
        $.ajax({
            type: "GET",
            url: formsServiceUrl,
            data: "{}",
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                fields = msg.Item.Fields;

            }
        });
        var choiceItems = new Array();
        var field = {
            Text: "No Mapping",
            Value: "No Mapping"
        };
        choiceItems[0] = field;
        for (var i = 0; i < fields.length; i++) {
            field = {
                Text: fields[i].substring(0, fields[i].lastIndexOf(",")),
                Value: fields[i].substring(fields[i].lastIndexOf(",") + 1)
            };
            choiceItems[i + 1] = field;
        }
        return choiceItems;
    },
    /* --------------------------------- properties --------------------------------- */

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    },

    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },

    // gets the reference to the content selector used to choose content item
    get_contentSelector: function () {
        return this._contentSelector;
    },

    // gets the reference to the content selector used to choose one or more content item
    set_contentSelector: function (value) {
        this._contentSelector = value;
    }
}

SitefinityWebApp.SalesForce.FormsControlDesigner.FormsControlDesignerCustom.registerClass('SitefinityWebApp.SalesForce.FormsControlDesigner.FormsControlDesignerCustom',
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
