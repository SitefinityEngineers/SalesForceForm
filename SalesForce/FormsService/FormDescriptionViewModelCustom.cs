using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;

using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using System.Web;
using Telerik.Sitefinity;

namespace SitefinityWebApp.SalesForce.FormsService
{
    [KnownType(typeof(FormDescriptionViewModelCustom))]
    [DataContract]
    public class FormDescriptionViewModelCustom
    {
        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>The fields.</value>
        [DataMember]
        public List<string> Fields
        {
            get
            {
                if (this.fields == null)
                    this.fields = new List<string>();
                return this.fields;
            }
            set
            {
                this.fields = value;
            }
        }

        #region Constructors
        // <summary>
        /// Initializes a new instance of the <see cref="FormDescriptionViewModel"/> class.
        /// </summary>
        public FormDescriptionViewModelCustom()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDescriptionViewModel"/> class.
        /// </summary>
        /// <param name="form">The form description.</param>
        public FormDescriptionViewModelCustom(string formId)
            : this(formId, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDescriptionViewModel"/> class.
        /// </summary>
        /// <param name="form">The form description.</param>
        /// <param name="provider">The provider.</param>
        public FormDescriptionViewModelCustom(string formId, string provider)
        {
             var controls = FormsManager.GetManager().GetForm(new Guid(formId)).Controls;
             string title = "";
             foreach (FormControl c in controls)
             {
                 if (!c.IsLayoutControl && !c.Caption.Value.Equals("Section header"))
                 {
                     var props = c.Properties.Where(p => p.Name.Equals("Title") && p.Language == null);
                     if (props.Any())
                     {
                         title = c.Properties.Where(p => p.Name.Equals("Title") && p.Language == null).First().Value.ToString();
                         if(title.Length>30)
                            title = title.Substring(0, 30)+"...";
                         this.Fields.Add(title + "," 
                             + c.ObjectType.Substring(c.ObjectType.LastIndexOf(".") + 1) + "_" + c.GetProperties(true).Where(p => p.Name.Equals("ID")).First().Value);
                     }
                 }
             }
        }
        #endregion

        private List<string> fields;
    }
}