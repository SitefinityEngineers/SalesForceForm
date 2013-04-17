using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using SitefinityWebApp.SalesForce.FormsControlDesigner;
using SitefinityWebApp.SalesForce.Configuration;

namespace SitefinityWebApp.SalesForce.FormsControl
{
    
    [ControlDesigner(typeof(FormsControlDesignerCustom))]
    public class FormsControlCustom : Telerik.Sitefinity.Modules.Forms.Web.UI.FormsControl
    {
        private static string sfVersion = "";
        private string fieldLastName = string.Empty;
        private string fieldCompany = string.Empty;

        public string FieldLastName
        {
            get
            {
                return this.fieldLastName;
            }
            set
            {
                this.fieldLastName = value;
            }
        }


        public string FieldCompany
        {
            get
            {
                return this.fieldCompany;
            }
            set
            {
                this.fieldCompany = value;
            }
        }

        protected override void ConfigureSubmitButton(System.Web.UI.Control control, string validationGroup)
        {
            var submit = control as FormSubmitButton;
            submit.Click += new EventHandler(submit_Click);
            base.ConfigureSubmitButton(control, validationGroup);
        }

        void submit_Click(object sender, EventArgs e)
        {
            if (!this.fieldLastName.Equals("No Mapping") && !this.fieldCompany.Equals("No Mapping")
                && this.FieldControls.Where(c => c.MetaField.FieldName.Equals(this.fieldLastName)).Any()
                && this.FieldControls.Where(c => c.MetaField.FieldName.Equals(this.fieldCompany)).Any())
            {
                //Retrieve configuration and build request for token
                SalesForceConfig config = Config.Get<SalesForceConfig>();
                StringBuilder request = new StringBuilder();
                request.Append("https://login.salesforce.com/services/oauth2/token?response_type=code&grant_type=password");
                request.Append("&client_id="); request.Append(config.ClientId); //Consumer Key for the Connected App
                request.Append("&client_secret="); request.Append(config.ClientSecret); //Consumer Secret for the Connected App
                request.Append("&username="); request.Append(config.Username);
                request.Append("&password="); request.Append(config.Password);request.Append(config.SecurityToken); //the security token is sent in an email to each user

                //Request token
                HttpWebRequest requestToken = (HttpWebRequest)WebRequest.Create(request.ToString());
                requestToken.Method = "POST";
                HttpWebResponse token = (HttpWebResponse)requestToken.GetResponse();
                string accessToken = new StreamReader(token.GetResponseStream()).ReadToEnd();
                string tokenStart = accessToken.Substring(accessToken.IndexOf("access_token") + 15);
                accessToken = tokenStart.Substring(0, tokenStart.IndexOf("\""));

                //Get form fields' values
                string lastName = ((FieldControl)this.FieldControls.Where(c => c.MetaField.FieldName.Equals(this.fieldLastName)).First()).Value.ToString();
                string company = ((FieldControl)this.FieldControls.Where(c => c.MetaField.FieldName.Equals(this.fieldCompany)).First()).Value.ToString();

                
                if (sfVersion.Equals(""))
                {
                    setSalesForceVersion();

                }
                //Create lead in SalesForce
                HttpWebRequest createLead = (HttpWebRequest)WebRequest.Create(config.Server + "/services/data/v" + sfVersion + "/sobjects/Lead/");
                createLead.Headers.Add("Authorization: Bearer " + accessToken);
                createLead.ContentType = "application/json";
                ASCIIEncoding encoding = new ASCIIEncoding();
                string stringData = "{\"LastName\":\"" + lastName + "\",\"Company\":\"" + company + "\"}";
                byte[] data = encoding.GetBytes(stringData);
                createLead.Method = "POST";
                createLead.ContentLength = data.Length;
                Stream newStream = createLead.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Flush();
                newStream.Close();
                createLead.GetResponse();
            }
        }

        private void setSalesForceVersion() {
            HttpWebRequest getVersion = (HttpWebRequest)WebRequest.Create(Config.Get<SalesForceConfig>().Server + "/services/data/");
            getVersion.Method = "GET";
            HttpWebResponse getVersionResult = (HttpWebResponse)getVersion.GetResponse();
            string str = null;
            using (Stream stream = getVersionResult.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    str = streamReader.ReadToEnd();
                }
            }
            sfVersion = str.Substring(str.LastIndexOf("version") + 10, 4);
        }
    }
}