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

#### Application Tested: Windows Notepad 22H2 (OS Build 19045.5487)

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

#### Cases Tested:
* Is the path of the Source file and Destination file specified in config file is correct?
* Is the Choose File window opened after the Open menu item is clicked?
* Is the Choose File window opened after the CTRL+S is clicked?
* Is the Destination File Created after saving?
* Is the Content in destination file is same as the content in Source file

---------

# FlaUI In A Nutshell

* Topics Covered:
    * What are Windows Applications?
    * Why UI Automation Testing?
    * Testing Windows Applications using FlaUI
       * What is FlaUInspect?
       * FlaUI Libraries
       * Condition factory and Property Library
    * Optimization Tips(for searching)
    * Blockers
 
## What are Windows Applications?
 * Applications that run natively on Microsoft Windows OS are called Windows application.
 * Running natively means those apps can dierectly use the Windows OS's API and Runtime enviroinment no translator or emulator or middleware required.
 * These are coded with windows specific framework (like Win32, .NET, WPF) by following Windows UX guidelines.
 * Some examples: Notepad.exe, Visual Studio, Calculator and File Explorer.

## Why UI Automation Testing?
* To test the app's User Interface we'll be performing UI Automation testing.
* The testing conducted here is Functional UI automation testing because a specific feature's behaviour is tested through the UI and It's automated.
* By Automating testing we can save time, cost and the tests for the application can be used for a long time.
* Tools Available for Windows automation
  
1)UFT 2) TestComplete 3)Coded UI 4) Ranorex 5) White 6) FLaUI

## Testing Windows applications using FlaUI
* FlaUI is based on native UI Automation libraries from Microsoft and therefore kind of a wrapper around them.
* The above statement means the UI Automation Libraries already has methods to access any UI elements of a application but those are low-level and confusing. The FlaUI libraries wraps around those libraries.
* The methods in FlaUI libraries provides more clarity and reduces the number of lines to code for accessing an element.
### FlaUI Inspect:
* It is an GUI Inspection tool.
* #### GUI Inspecting Tools:
  * These tools fetches the data of the UI elements used in an application.
  * Available Inspection Tools : 1) UISPY 2) INSPECT 3) Visual UIA Verify 4) FLAUINSPECT
* FlaUI Modes and Working Screen Shots :
    * ![image](https://github.com/user-attachments/assets/6693d8c9-6d46-4a1b-aa75-fbcac1011a4c)
    * ![image](https://github.com/user-attachments/assets/47242bca-2ea8-4fec-9622-1e059ef55a16)
    * ![image](https://github.com/user-attachments/assets/01480d43-99fb-4c32-b703-6f7667db2dd1)
    * ![image](https://github.com/user-attachments/assets/41279072-7872-42c6-a677-09bde186f565)
  
### FlaUI Libraries: 
   * FlaUI Core.
   * FlaUI UIA2.
   * FlaUI UIA3.
   * ![image](https://github.com/user-attachments/assets/178059a5-b1b1-4a8b-b001-0d54853d4f09)


### Methods Provided:
* Launch(), Attach(), Close().
* For finding elements: FindFirstDescendant(), FindAllDescendants(), FinfFirstChild, FindAllChildren(), FindFirst(), FindFirstXpath(), FindAllXPath().
* For performing Operations: Click(), Press(), Type(); 

### Optimization Tips for Searching:
* Caching the panel to acces all it's elements.
* Prefer searching using AutomationID.
* Multiple Conditonal factory Conditions chained.
* Speed (varies based on circumstance): FindFirstChild() > FindFirstDescendant() > FindAllChildren() > FindAllDescendants().

### Blockers:
* Suitable DotNet version that FlaUI packages support.
* In windows 11 testing Notepad is not possible. because Notepad is a WindowsAppX/UWP App. 
