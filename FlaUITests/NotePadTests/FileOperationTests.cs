using System;
using System.IO;
using System.Threading;
using FlaUI.UIA3;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotePadTests.Models;
using NotePadTests.Utilities;
using NotePadTests.Wrappers;
using System.Runtime.ExceptionServices;


namespace NotePadTests
{
    [TestClass]
    public class FileOperationTests
    {
        private string configFilePath = "C:\\Users\\Sruthi.Subaraja\\Desktop\\Dhivakar\\FlaUINotepadTest\\TestData\\ConfigFileFolder\\ConfigFile.json";
        private string ErrorLogFilePath = "C:\\Users\\Sruthi.Subaraja\\Desktop\\Dhivakar\\FlaUINotepadTest\\TestData\\TestLog\\" + $"ErrorLog[{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}].txt";
        private FolderInfo FolderInfo;
        private ApplicationManager Application;

        [TestInitialize]
        public void Setup()
        {
            // Fetching Data from config file.
            try
            {
                FileHandler.LogData("Errors Occured:", ErrorLogFilePath);
                if (!File.Exists(configFilePath))
                {
                    Assert.Fail("configFile does not exist");
                }

                FolderInfo = FileHandler.FetchFileData<FolderInfo>(configFilePath);
                if (FolderInfo == null)
                {
                    Assert.Fail("Config File data is not in proper JSON format");
                }

                if (!File.Exists(FolderInfo.SourceFilePath))
                {
                    Assert.Fail("Check whether the source file exists or the folder path in config is correct");
                }

                if (File.Exists(FolderInfo.DestinationFilePath))
                {
                    File.Delete(FolderInfo.DestinationFilePath);
                }
            }
            catch (Exception ex)
            {
                FileHandler.LogData(ex.Message, ErrorLogFilePath);
            }
        }

        //FileHandler.LogData("configFile does not exist", ErrorLogFilePath);
        //FileHandler.LogData("Config File data is not in proper JSON format", ErrorLogFilePath);
        //FileHandler.LogData("Check whether the source file exists or the folder path in config is correct", ErrorLogFilePath);

        [TestMethod]
        public void CopyContent_FromExistingFile_Paste_AndSaveNewFile()
        {
            //Assign
            //Act
            // Launching the Notepad application.
            try
            {
                using (Application = new ApplicationManager(@"notepad.exe", new UIA3Automation()))
                {
                    MenuItem fileMenu = Application.GetDescendant("File").AsMenuItem();
                    fileMenu.Click();
                    MenuItem openMenuItem = Application.GetDescendant("Open...").AsMenuItem();
                    openMenuItem.Click();
                    //Thread.Sleep(1000);
                    Window openFileWindow = Application.GetModalWindow("Open");
                    TextBox fileNameTextBox = Application.GetModalWindowDescendant(openFileWindow, "File name:", ControlType.Edit).AsTextBox();
                    fileNameTextBox.Enter(FolderInfo.SourceFilePath);
                    Keyboard.Type(VirtualKeyShort.ENTER);
                    //Thread.Sleep(1000);
                    Application.CopyContentToClipBoard();
                    AutomationElement document = Application.GetDescendant("Text Editor", ControlType.Document);
                    string ExpectedFiledata = Application.GetValueFromTextBox(document.AsTextBox());
                    fileMenu.Click();
                    MenuItem newMenuItem = Application.GetDescendant("New").AsMenuItem();
                    newMenuItem.Click();
                    document = Application.GetDescendant("Text Editor", ControlType.Document);
                    document.Focus();
                    Application.PasteContentFromClipBoard();
                    //Thread.Sleep(1000);
                    string ActualNewFileData = Application.GetValueFromTextBox(document.AsTextBox());
                    Assert.AreEqual(ExpectedFiledata, ActualNewFileData);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_S);
                    //Thread.Sleep(1000);
                    Window saveAsWindow = Application.GetModalWindow("Save As");
                    fileNameTextBox = Application.GetModalWindowDescendant(saveAsWindow, "File name:", ControlType.Edit).AsTextBox();
                    fileNameTextBox.Enter(FolderInfo.DestinationFilePath);
                    Keyboard.Type(VirtualKeyShort.ENTER);
                    //Thread.Sleep(2000);
                }

                //Assert
                if (!File.Exists(FolderInfo.DestinationFilePath))
                {
                    Assert.Fail("Destination file not created.");
                }

                if (!File.ReadAllText(FolderInfo.SourceFilePath).Equals(File.ReadAllText(FolderInfo.DestinationFilePath)))
                {
                    Assert.Fail("Destination file data is not same as the Source file Data.");
                }
            }
            catch (Exception ex)
            {
                FileHandler.LogData(ex.Message, ErrorLogFilePath);
            }
        }
    }
}
