This is the readme file for LCI.Bootstrap.MVC4.

This project will pull in the Twitter.Boostrap NuGet package, and will add in the _Layout file that you need to 
make this work.  Complete the three steps below and you should be on your way.

------------------------------------------------------------------------------------------------------------------------
1. Rename the _Layout File
------------------------------------------------------------------------------------------------------------------------
   In the shared folder, a new layout file was added.  Rename your existing _Layout.cshtml to something else, 
   then rename the _Layout_Bootstrap.cshtml to _Layout.cshtml

------------------------------------------------------------------------------------------------------------------------
2. Update the BundleConfig.cs file
------------------------------------------------------------------------------------------------------------------------
   Add the following lines in the App_Start\BundleConfig.cs file:
   
      bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
        "~/Scripts/bootstrap.js"));

      bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
        "~/Content/bootstrap.css",
        "~/Content/SiteBootstrap.css"));

------------------------------------------------------------------------------------------------------------------------
3. Add a method for the sample Tabs page 
------------------------------------------------------------------------------------------------------------------------
   In your Controllers\HomeController.cs, add the following function to enable the sample Tabs page:
    public ActionResult Tabs()
    {
      return View();
    }

------------------------------------------------------------------------------------------------------------------------
4. Compile and Run
------------------------------------------------------------------------------------------------------------------------
