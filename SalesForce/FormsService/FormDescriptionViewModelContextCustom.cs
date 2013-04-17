using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Collections.Generic;

namespace SitefinityWebApp.SalesForce.FormsService
{
    [DataContract]
    public class FormDescriptionViewModelContextCustom
    {
        /// <summary>
        /// Gets or sets the form description item.
        /// </summary>
        /// <value>The item.</value>
        [DataMember]
        public FormDescriptionViewModelCustom Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }
        private FormDescriptionViewModelCustom item;
    }
}