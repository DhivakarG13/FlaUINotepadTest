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
        private NotepadManager Notepad;

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

        [TestMethod]
        public void CopyContent_FromExistingFile_Paste_AndSaveNewFile()
        {
            //Assign
            // Launching the Notepad application.
            try
            {
                using (Notepad = new NotepadManager(@"notepad.exe", new UIA3Automation()))
                {

                    //Act
                    Notepad.OpenAnExistingFile(FolderInfo.SourceFilePath);
                    Notepad.CopyFocusedContentToClipBoard();
                    Notepad.OpenANewPage();
                    //AutomationElement document = Notepad.GetDescendant("Text Editor", ControlType.Document);
                    //document.Focus();
                    Notepad.PasteContentFromClipBoard();
                    Thread.Sleep(1000);
                    Notepad.SaveContentToFile(FolderInfo.DestinationFilePath);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_S);
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
