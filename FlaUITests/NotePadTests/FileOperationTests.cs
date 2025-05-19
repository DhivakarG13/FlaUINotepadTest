using System.IO;
using System.Threading;
using System.Linq;
using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Input;
using FlaUI.Core.Conditions;
using FlaUI.Core.WindowsAPI;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Patterns;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotePadTests.Models;
using NotePadTests.Utilities;
using NotePadTests.Wrappers;
using System;


// Uncomment the Thread.Sleep(1000); lines if you want to add delays between actions for better visibility or debugging purposes.

namespace NotePadTests
{
    [TestClass]
    public class FileOperationTests
    {
        string configFilePath = "C:\\Users\\Sruthi.Subaraja\\Desktop\\Dhivakar\\TestData\\ConfigFileFolder\\ConfigFile.json";
        FolderInfo FolderInfo;
        WrapperClass WrapperObject;

        [TestMethod]
        public void FetchFolderInfo_FromConfigFile()
        {
            //Assign
            // Fetching Data from config file.
            FileHandler.LogTestData("Errors Occured:");
            if (!File.Exists(configFilePath))
            {
                FileHandler.LogTestData("configFile does not exist");
                Assert.Fail("configFile does not exist");
            }

            FolderInfo = FileHandler.FetchConfigFileData(configFilePath);
            if (FolderInfo == null)
            {
                FileHandler.LogTestData("Config File data is not in proper JSON format");
                Assert.Fail("Config File data is not in proper JSON format");
            }

            if (!File.Exists(FolderInfo.SourceFilePath))
            {
                FileHandler.LogTestData("Check whether the source file exists or the folder path in config is correct");
                Assert.Fail("Check whether the source file exists or the folder path in config is correct");
            }
        }


        [TestMethod]
        public void CopyContent_FromExistingFile_Paste_AndSaveNewFile()
        {

            if (File.Exists(FolderInfo.DestinationFilePath))
            {
                File.Delete(FolderInfo.DestinationFilePath);
            }

            //Act
            // Launching the Notepad application.
            using (Application application = Application.Launch(@"notepad.exe"))
            {
                WrapperObject = new WrapperClass(@"notepad.exe", new UIA3Automation());

                // Opening the file using the File menu.
                MenuItem fileMenu = WrapperObject.GetElementByName<MenuItem>("File");
                fileMenu.Click();
                MenuItem openMenuItem = WrapperObject.GetElementByName<MenuItem>("Open...");
                openMenuItem.Click();

                //open file dialog is opened and we need to enter the file name in the text box.
                Window openFileWindow = WrapperObject.GetElementByName<Window>("Open");

                TextBox fileNameTextBox = WrapperObject.GetElementByNameFromWindow<TextBox>(openFileWindow, "File name:");
                fileNameTextBox.Enter(FolderInfo.SourceFilePath);
                Keyboard.Type(VirtualKeyShort.ENTER);

                // Wait for the file to open and focus on the document area.
                WrapperObject.GetElementByName<MenuItem>("Open...");
                TextBox document = WrapperObject.GetElementByName<TextBox>("Text editor"); ;
                document.Focus();

                // Copying all contents
                WrapperObject.CopyContentToClipBoard();

                // Getting the text from the document range to compare later.
                string ExpectedFiledata = WrapperObject.GetValueFromTextBox(document);

                // Creating a new file and pasting the copied content.
                fileMenu.Click();
                MenuItem newMenuItem = WrapperObject.GetElementByName<MenuItem>("New");
                newMenuItem.Click();

                document.Focus();
                WrapperObject.PasteContentFromClipBoard();

                // Getting the text from the new document range to compare.
                string ActualNewFileData = WrapperObject.GetValueFromTextBox(document);
                Assert.AreEqual(ExpectedFiledata, ActualNewFileData);

                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_S);

                // Accessing the Save As window to enter the destination file name.
                Window saveAsWindow = WrapperObject.GetElementByName<Window>("Save As");
                fileNameTextBox = WrapperObject.GetElementByNameFromWindow<TextBox>(openFileWindow, "File name:");
                fileNameTextBox.Enter(FolderInfo.DestinationFilePath);
                Keyboard.Type(VirtualKeyShort.ENTER);
            }

            //Assert
            if (!File.Exists(FolderInfo.DestinationFilePath))
            {
                FileHandler.LogTestData("Destination file not created.");
                Assert.Fail("Destination file not created.");
            }

            if (!File.ReadAllText(FolderInfo.SourceFilePath).Equals(File.ReadAllText(FolderInfo.DestinationFilePath)))
            {
                FileHandler.LogTestData("Destination file data is not same as the Source file Data.");
                Assert.Fail("Destination file data is not same as the Source file Data.");
            }
        }
    }
}


















// Assertion to check whether the sourcee file exists. - Done
// Check whether the destination file already exists and Handle accordingly. - Deleted if Exists
// Check whether the Config file data is in proper JSON format. - Done
// Check whether the sorce and Destination file exists after the operation. - Done
// check whether the content in the destination file == expected content. - Done
// if other note pad instance is open is it affecting our current instance. 
// check this for all the approaches. - Only one approach will be approved :)
// Asynchrony is needed here ?? - Not Needed
// Have a Test Log document. - Done