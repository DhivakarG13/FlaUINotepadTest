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

namespace NotePadTests
{
    [TestClass]
    public class FileOperationTests
    {
        [TestMethod]
        public void CopyContent_FromExistingFile_Paste_AndSaveNewFile()
        {
            //Assign
            string configFilePath = "C:\\Users\\Sruthi.Subaraja\\Desktop\\Dhivakar\\TestData\\ConfigFileFolder\\ConfigFile.json";
            FileHandler.LogTestData("Errors Occured:");
            if (!File.Exists(configFilePath))
            {
                FileHandler.LogTestData("configFile does not exist");
                Assert.Fail("configFile does not exist");
            }

            FolderInfo folderInfo = FileHandler.FetchConfigFileData(configFilePath);
            if (folderInfo == null)
            {
                FileHandler.LogTestData("Config File data is not in proper JSON format");
                Assert.Fail("Config File data is not in proper JSON format");
            }

            if (!File.Exists(folderInfo.SourceFilePath))
            {
                FileHandler.LogTestData("Check whether the source file exists or the folder path in config is correct");
                Assert.Fail("Check whether the source file exists or the folder path in config is correct");
            }

            if (File.Exists(folderInfo.DestinationFilePath))
            {
                File.Delete(folderInfo.DestinationFilePath);
            }

            //Act
            Application application = Application.Launch(@"notepad.exe");
            Thread.Sleep(1000);
            UIA3Automation automation = new UIA3Automation();
            Window mainWindow = application.GetMainWindow(automation);
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            MenuItem fileMenu = mainWindow.FindFirstDescendant(cf.ByName("File")).AsMenuItem();
            fileMenu.Click();
            Thread.Sleep(1000);
            MenuItem openMenuItem = mainWindow.FindFirstDescendant(cf.ByName("Open...")).AsMenuItem();
            openMenuItem.Click();
            Thread.Sleep(1000);

            Window openFileWindow = mainWindow.ModalWindows.FirstOrDefault(w => w.Title.Contains("Open"));
            if (openFileWindow != null)
            {
                TextBox fileNameTextBox = openFileWindow.FindFirstDescendant(cf.ByControlType(ControlType.Edit).And(cf.ByName("File name:"))).AsTextBox();
                fileNameTextBox.Enter(folderInfo.SourceFilePath);
                Thread.Sleep(1000);
                Keyboard.Type(VirtualKeyShort.ENTER);
                Thread.Sleep(1000);
            }
            else
            {
                FileHandler.LogTestData("Open window not found.");
                Assert.Fail("Open window not found.");
            }

            mainWindow.FindFirstDescendant(cf.ByControlType(ControlType.Document)).Focus();
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
            Thread.Sleep(1000);
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_C);
            Thread.Sleep(1000);

            ITextPattern textPattern = mainWindow.FindFirstDescendant(cf.ByControlType(ControlType.Document)).Patterns.Text.PatternOrDefault;
            string ExpectedFiledata = textPattern.DocumentRange.GetText(-1);

            fileMenu = mainWindow.FindFirstDescendant(cf.ByName("File")).AsMenuItem();
            fileMenu.Click();
            Thread.Sleep(1000);
            MenuItem newMenuItem = mainWindow.FindFirstDescendant(cf.ByName("New")).AsMenuItem();
            newMenuItem.Click();
            Thread.Sleep(1000);

            mainWindow.FindFirstDescendant(cf.ByControlType(ControlType.Document)).Focus();
            Thread.Sleep(1000);
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
            Thread.Sleep(1000);


            textPattern = mainWindow.FindFirstDescendant(cf.ByControlType(ControlType.Document)).Patterns.Text.PatternOrDefault;
            string ActualNewFiledata = textPattern.DocumentRange.GetText(-1);
            Assert.AreEqual(ExpectedFiledata, ActualNewFiledata);

            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_S);
            Thread.Sleep(1000);

            Window saveAsWindow = mainWindow.ModalWindows.FirstOrDefault(w => w.Title.Contains("Save As"));
            if (saveAsWindow != null)
            {
                TextBox fileNameTextBox = saveAsWindow.FindFirstDescendant(cf.ByControlType(ControlType.Edit).And(cf.ByName("File name:"))).AsTextBox();
                fileNameTextBox.Enter(folderInfo.DestinationFilePath);
                Thread.Sleep(1000);
                Button saveButton = saveAsWindow.FindFirstDescendant(cf.ByControlType(ControlType.Button).And(cf.ByName("Save"))).AsButton();
                saveButton.Click();
                Thread.Sleep(1000);
            }
            else
            {
                FileHandler.LogTestData("Save As window not found.");
                Assert.Fail("Save As window not found.");
            }

            mainWindow.Close();
            Thread.Sleep(2000);

            //Assert
            if (!File.Exists(folderInfo.DestinationFilePath))
            {
                FileHandler.LogTestData("Destination file not created.");
                Assert.Fail("Destination file not created.");
            }

            if(!File.ReadAllText(folderInfo.SourceFilePath).Equals(File.ReadAllText(folderInfo.DestinationFilePath)))
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