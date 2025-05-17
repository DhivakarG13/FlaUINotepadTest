# FlaUINotepadTest

### Tools and Version Information

* Here are the versions of the tools used in this project:
    * DotNet Version 4.8
    * FlaUI.Core 5.0.0 (Package by Roamer)
    * FlaUI.UIA2 5.0.0 (Package by Roamer)
    * FlaUI.UIA3 5.0.0 (Package by Roamer)
    * Test project type: Unit Test Project (.NET Framework) (Contains MS Unit tests) Targeting .NET Framework 4.8
    * FlaUInspect 
 -----------
## Test Information

#### Application Tested: Windows Notepad 11.2503.16.0 // Check This

#### Feature tested:  Copy Content from a file and Paste it in new document and Save it.

#### Approach 1:
1) Open Notepad Application.
2) Click on File Menu.
3) Click on Open Menu, a file explorer window named "Open" opens.
4) Enter the Source file path in the File Name: Text box.
5) Click on Open Button.
6) Press Ctrl+A to Select All the content in the file and press Ctrl+C to copy to click board.
7) Click on File Menu.
8) Click on New Menu.
9) New Document will be opened now press Ctrl+V to paste the content in the new document.
10) Press Ctrl+S, a file explorer window named "Save AS" opens.
11) Enter the Destination file path in the File Name: Text box.
12) Press Enter key to save the file.
13) Close the Notepad application.

#### Test Demo video:
https://drive.google.com/file/d/17SUV2sosDCJLfHnDF0oc-C_CmcGFZUvh/view?usp=sharing
#### Code walk through video:
https://drive.google.com/file/d/1ivXrNGAayPDJ0nHIKdPcdUKSDtXISfd5/view?usp=sharing
#### Video Transcript:
* To test the Notepad application, I created a Unit Test Project (.NET Framework) (Contains MS Unit tests) by setting my Targeting .NET Framework version to 4.8.
* FlaUI.Core, FlaUI.UIA2, and FlaUI.UIA3 NuGet packages are installed into my project from Nuget package manager.
* In my test method, Initial SetUp was Fetching data from my configFile.json. It contains the source and destination file paths.
* With the help of a Helper method the object with source file and destination file is fetched. If any error occurs like improper JSON format or invalid File paths, it will throw an exception.
* Then I created a new instance of the Application class and Opened Notepad application.
* Notepad is our process opened here and its wrapped inside the Application class and stored in our variable "application". 
* A new instance of UIA3Automation class is created and stored in the variable "automation".
* To get the Main window of the Notepad application. I used the GetWindow method which takes the automation object as the parameter.
* FlaUInspect tool to inspect the Notepad application and find the details of the elements I need to interact with in this approach.
* I used the FindFirstDescendant method to find the File menu and click on it. To reduce the search time, I used the AutomationId property to find the File menu.
* From the Menu, I clicked on the Open menu item. I used the FindFirstDescendant method to find the Open menu item and click on it.

#### Cases Tested:
* Is the path of the Source file and Destination file specified in config file is correct?
* Is the Choose File window opened after the Open menu item is clicked?
* Is the Choose File window opened after the CTRL+S is clicked?
* Is the Destination File Created after saving?
* Is the Content in destination file is same as the content in Source file?
// Add cases here

---------

# FlaUI In A Nutshell

* Topics Covered:
    * What are Windows Applications?
    * Why UI Automation Testing?
    * Testing Windows Applications using FlaUI
       * What is FlaUInspect?
       * Methods provided by FlaUI Library
       * Condition factory and Property Library
    * Optimization Tips(for searching)
    * Blockers
