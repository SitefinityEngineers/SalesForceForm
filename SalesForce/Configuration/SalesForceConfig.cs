using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;


namespace SitefinityWebApp.SalesForce.Configuration
{
    public class SalesForceConfig : ConfigSection
    {
        [ConfigurationProperty("Server")]
        public string Server
        {
            get
            {
                return (string)this["Server"];
            }
            set
            {
                this["Server"] = value;
            }
        }

        //"ConsumerKey"
        [ConfigurationProperty("ClientId")]
        public string ClientId
        {
            get { 
                return (string)this["ClientId"]; 
            }
            set
            {
                this["ClientId"] = value;
            }
        }

        //"ConsumerSecret"
        [ConfigurationProperty("ClientSecret")]
        public string ClientSecret
        {
            get
            {
                return (string)this["ClientSecret"];
            }
            set
            {
                this["ClientSecret"] = value;
            }
        }

        [ConfigurationProperty("Username")]
        public string Username
        {
            get
            {
                return (string)this["Username"];
            }
            set
            {
                this["Username"] = value;
            }
        }

        [ConfigurationProperty("Password")]
        public string Password
        {
            get
            {
                return (string)this["Password"];
            }
            set
            {
                this["Password"] = value;
            }
        }

        [ConfigurationProperty("SecurityToken")]
        public string SecurityToken
        {
            get
            {
                return (string)this["SecurityToken"];
            }
            set
            {
                this["SecurityToken"] = value;
            }
        }
    }
}