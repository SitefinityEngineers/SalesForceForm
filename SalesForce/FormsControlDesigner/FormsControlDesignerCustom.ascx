<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="designers" Namespace="Telerik.Sitefinity.Web.UI.ControlDesign" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinityFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>.
<sitefinity:ResourceLinks id="resourcesLinks1" runat="server">
    <sitefinity:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="True" />
    <%--<sitefinity:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_default_min.css" Static="True" />--%>
    <sitefinity:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js"  Static="true" />
</sitefinity:ResourceLinks>
<link href="http://cdn.kendostatic.com/2012.3.1315/styles/kendo.bootstrap.min.css" rel="stylesheet" />
<div class="sfContentViews sfViewsTopSep">
<div class="sfColWrapper sfEqualCols sfModeSelector sfClearfix sfHierarchicalContentDim">
<div class="sfLeftCol">
<h2 class="sfStep1">Choose a form</h2>  
<div id="selectorTag" style="display: none;" class="sfFlatDialogSelector">
    <designers:ContentSelector 
       ID="selector" 
       runat="server" 
       ItemType="Telerik.Sitefinity.Forms.Model.FormDescription"
       ItemsFilter="Visible == true AND Status == Live" 
       TitleText="Choose News" 
       BindOnLoad="false" 
       AllowMultipleSelection="false" 
       WorkMode="List" 
       SearchBoxInnerText="" 
       SearchBoxTitleText="<%$Resources:Labels, NarrowByTyping %>" 
       ServiceUrl="~/Sitefinity/Services/Forms/FormsService.svc"
       ListModeClientTemplate="<strong class='sfItemTitle'>{{Title}}</strong>">
   </designers:ContentSelector>
</div>
</div>
<div id="ForumSettings" class="sfRightCol" style="width: 340px;" runat="server">
    <h2 class="sfStep2">SalesForce Lead Mappings</h2>
    <div style="width: 100%; padding-top: 10px">
        <label for="lastName" style="display: block;float: left;width: 100px;text-align:right;padding-right:10px">Last Name</label>
        <input id="lastName" value="1" style="width: 220px" /><br/>
    </div>
    <div style="width: 100%; padding-top: 10px">
        <label for="company" style="display: block;float: left;width: 100px;text-align:right;padding-right:10px">Company</label>
        <input id="company" value="1" style="width: 220px" />
     </div>
</div>
</div>
</div>