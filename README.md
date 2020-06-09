# Getting started running the application
1. run npm install in Test.Web/stepper-tester/ to install node modules
2. run Update-Database command in Package managger console from Visual Studio.
 connection string will be used as defined in CoreContextFactory by the cli
 web application reads the connection string from appsettings.Development in Test.Web project, make sure to set up both correctly
3. run the Test.Web project

# Using the templates
1. Import the .zip files to "Documents\Visual Studio 2019\Templates\ProjectTemplates"
2. Add a new project and look for the newly added templates.
3. In case of the web template, replace all occurences of "EfTemplate1" with the namespace given to the database project.
4. Add the database project as reference to web project