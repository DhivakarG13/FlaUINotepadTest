using System;
using System.IO;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotePadTests.Models;
using NotePadTests.Utilities;
using NotePadTests.Wrappers;


namespace NotePadTests
{
    [TestClass]
    public class FileOperationTests
    {
        //Test Documents related to Logging, copying and saving are kept in a local path away from source documents.
        private string configFilePath = "D:\\TestData\\ConfigFileFolder\\ConfigFile.json";
        private string ErrorLogFilePath = "D:\\TestData\\TestLog\\" + $"ErrorLog[{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}].txt";
        private FolderInfo FolderInfo;
        private NotepadManager Notepad;

        [TestInitialize]
        public void Setup()
        {
            // Fetching Data from config file.
            try
            {
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
            }
            catch (Exception ex)
            {
                FileHandler.LogData("Error Occurred:", ErrorLogFilePath);
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
                using (Notepad = new NotepadManager(new UIA3Automation()))
                {
                    //Act
                    Notepad.OpenExistingFile(FolderInfo.SourceFilePath);
                    Notepad.CopyFocusedContentToClipBoard();
                    Notepad.OpenNewPage();
                    Notepad.PasteContentFromClipBoard();
                    Notepad.SaveContentToFile(FolderInfo.DestinationFilePath);
                }

                //Assert
                if (!File.Exists(FolderInfo.DestinationFilePath))
                {
                    Assert.Fail($"Destination file not created in path {FolderInfo.DestinationFilePath}");
                }

                if (!File.ReadAllText(FolderInfo.SourceFilePath).Equals(File.ReadAllText(FolderInfo.DestinationFilePath)))
                {
                    Assert.Fail("Destination file data is not same as the Source file Data.");
                }
            }
            catch (Exception ex)
            {
                FileHandler.LogData("Error Occurred:", ErrorLogFilePath);
                FileHandler.LogData(ex.Message, ErrorLogFilePath);
            }
            finally
            {
                if (File.Exists(FolderInfo.DestinationFilePath))
                {
                    File.Delete(FolderInfo.DestinationFilePath);
                }
            }
        }
    }
}
