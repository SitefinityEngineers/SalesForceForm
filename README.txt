These are the steps you need to take in order to use the new widget:

1.	First you should allow access to SalesForce by creating a Connected App. Each app has a consumer key and a consumer secret. You also need a user, who has permissions for lead creation. For the configuration within Sitefinity you will need that user’s username, password and security token.
2.	Download the source code for the new Forms control. Place the SalesForce folder under the root of your application. 
3.	You have to register the new configuration section so merge this code in the Global.asax file:

protected void Application_Start(object sender, EventArgs e) {
            Telerik.Sitefinity.Abstractions.Bootstrapper.Initialized += Bootstrapper_Initialized;
}

protected void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs args) {
            Config.RegisterSection<SalesForceConfig>();
}

4.	Finally build the application and register FormsControlCustom using Sitefinity Thunder.
