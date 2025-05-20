using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA2;
using FlaUI.UIA3;
using NotePadTests.Models;

namespace NotePadTests.Wrappers
{
    public class NotepadManager : ApplicationManager
    {
        public NotepadManager(string executable, UIA3Automation automation) : base(executable, automation)
        {
        }
        public NotepadManager(string executable, UIA2Automation automation) : base(executable, automation)
        {
        }
        public void OpenAnExistingFile(string filePath)
        {
            MenuItem fileMenu = GetDescendant("File").AsMenuItem();
            fileMenu.Click();
            MenuItem openMenuItem = GetDescendant("Open...").AsMenuItem();
            openMenuItem.Click();
            Thread.Sleep(1000);
            Window openFileWindow = GetModalWindow("Open");
            TextBox fileNameTextBox = GetModalWindowDescendant(openFileWindow, "File name:", ControlType.Edit).AsTextBox();
            fileNameTextBox.Enter(filePath);
            Keyboard.Type(VirtualKeyShort.ENTER);
            Thread.Sleep(1000);
        }
        public void OpenANewPage()
        {
            MenuItem fileMenu = GetDescendant("File").AsMenuItem();
            fileMenu.Click();
            MenuItem newMenuItem = GetDescendant("New").AsMenuItem();
            newMenuItem.Click();
        }
        public void SaveContentToFile(string filePath)
        {
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_S);
            Thread.Sleep(1000);
            Window saveAsWindow = GetModalWindow("Save As");
            var fileNameTextBox = GetModalWindowDescendant(saveAsWindow, "File name:", ControlType.Edit).AsTextBox();
            fileNameTextBox.Enter(filePath);
            Keyboard.Type(VirtualKeyShort.ENTER);
            Thread.Sleep(2000);
        }

    }
}
