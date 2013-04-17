using System;
using System.Linq;
using System.ServiceModel;
using Telerik.Sitefinity.Web.Services;
using System.ServiceModel.Activation;

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.Services;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Forms.Model;

namespace SitefinityWebApp.SalesForce.FormsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FormsServiceCustom" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FormsServiceCustom.svc or FormsServiceCustom.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class FormsServiceCustom : IFormsServiceCustom
    {
        public FormDescriptionViewModelContextCustom GetFormFields(string formId, string providerName)
        {
            return this.GetFormFieldsInternal(formId, providerName);
        }

        private FormDescriptionViewModelContextCustom GetFormFieldsInternal(string formId, string providerName)
        {
            ServiceUtility.RequestBackendUserAuthentication();
            FormDescriptionViewModelCustom formDescriptionViewModel = new FormDescriptionViewModelCustom(formId, providerName);

            var result = new FormDescriptionViewModelContextCustom()
            {
                Item = formDescriptionViewModel
            };

            ServiceUtility.DisableCache();

            return result;
        }
    }
}
