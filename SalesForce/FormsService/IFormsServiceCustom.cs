using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Data.WcfHelpers;

using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using SitefinityWebApp.SalesForce.FormsService;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SitefinityWebApp.SalesForce.FormsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFormsServiceCustom" in both code and config file together.
    [ServiceContract]
    [AllowDynamicFields]
    public interface IFormsServiceCustom
    {
        [OperationContract]
        [WebGet(UriTemplate = "/{formId}/?providerName={providerName}", ResponseFormat = WebMessageFormat.Json)]
        FormDescriptionViewModelContextCustom GetFormFields(string formId, string providerName);
    }
}
