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

        /// <summary>
        /// Opens an existing file by interacting with the application's file menu and open dialog.
        /// </summary>
        /// <param name="filePath">The full path of the file to open. This must be a valid, accessible file path.</param>
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

        /// <summary>
        /// Opens a new page by navigating through the "File" menu and selecting the "New" option.
        /// </summary>
        public void OpenANewPage()
        {
            MenuItem fileMenu = GetDescendant("File").AsMenuItem();
            fileMenu.Click();
            MenuItem newMenuItem = GetDescendant("New").AsMenuItem();
            newMenuItem.Click();
        }

        /// <summary>
        /// Saves the current content to a file at the specified file path.
        /// </summary>
        /// <param name="filePath">The full path, including the file name, where the content should be saved.  This must be a valid file path
        /// and cannot be null or empty.</param>
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
