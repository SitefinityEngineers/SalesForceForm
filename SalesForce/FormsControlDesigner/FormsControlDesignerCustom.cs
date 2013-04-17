using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.SalesForce.FormsControlDesigner
{
    public class FormsControlDesignerCustom : Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner
    {
        public override string LayoutTemplatePath
        {
            get
            {
                return _layoutTemplatePath;
            }
            set
            {
                _layoutTemplatePath = value;
            }
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            if (scripts == null) return base.GetScriptReferences();
            scripts.Add(new ScriptReference(_scriptReference));
            return scripts;
        }

        private string _layoutTemplatePath = "~/SalesForce/FormsControlDesigner/FormsControlDesignerCustom.ascx";
        private string _scriptReference = "~/SalesForce/FormsControlDesigner/FormsControlDesignerCustom.js";
       }
}