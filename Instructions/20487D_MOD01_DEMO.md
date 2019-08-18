# Module 1: Overview of Service and Cloud Technologies

1. Wherever you see a path to file starting at *[Repository Root]*, replace it with the absolute path to the folder in which the 20487 repository resides. 
 For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: **[Repository Root]\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever *{YourInitials}* appears, replace it with your actual initials. (For example, the initials for John Doe will be jd.)
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and prepare them beforehand to save classroom time.


# Lesson 4: Cloud Computing

### Demonstration: Exploring the Microsoft Azure Portal

#### Preparation Steps

  >**Note**: This demonstration requires an Azure account. Make sure you have a valid account before starting the demonstration.

#### Demonstration Steps

1. Open Microsoft Edge.
2. Navigate to **https://portal.azure.com**.
3. If a page appears prompting for your email address, enter your email address, click **Next**, enter your password, and then click **Sign In**.
4. If the **Stay signed in?** dialog box appears, click **Yes**.

   >**Note**: During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.

5. Review the items in the pane on the left side of the screen and understand the different services that you can manage with the Azure portal.
6. On the left side of the screen, click **+ Create a resource**, and then click **Web App**. The **App Name** and **Resource Group** boxes and **App Service Plan/Location** appear on the right side of the screen.
7. In the **App Name** box, enter the web app name **WebAppDemo**_YourInitials_ (Replace _YourInitials_ with your initials).  

   >**Note**: The app name you entered is going to be part of the URL that you will use when connecting to the web application.
   
8. In the **Resource Group** box, select **Create new**.
9. Click **App Service Plan/Location**,  and then click **Create new**. 
10. In **App Service Plan**, enter **BlueYonder**.
11. In **Location**, select the location that is closest to you.
12. Click **Ok.**
13. Click **Create**, and then wait until the web app is deployed.
14. In the **All Resources** pane, click the web app that you created in the previous step (the one that is named **WebAppDemo** _YourInitials_).  

   >**Note**: Currently the web app has no content.
  
15. Go over the different sections and understand their purpose:  
  a. **General** (no section): Provides an overview about the activity of the web app, access control and metadata.  
  b. **Deployments**: Controls how and when the application is deployed.  
  c. **Settings**: Allows you to control settings such as monitoring capabilities, remote access, security tokens, scaling and more.
16. Close all open windows.


©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
