using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA2;
using FlaUI.UIA3;

namespace NotePadTests.Wrappers
{
    public class NotepadManager : ApplicationManager
    {
        private const string NotepadExecutableFileName = @"notepad.exe";
        private MenuItem fileMenu;
        private MenuItem newMenu;
        private MenuItem openMenu;
        private MenuItem saveAsMenu;
        private Window modalWindow_Open;
        private Window modalWindow_SaveAs;

        MenuItem FileMenu
        {
            get 
            { 
                if (fileMenu == null)
                {
                    fileMenu = GetDescendant("File").AsMenuItem();
                }
                return fileMenu;
            }
        }
        MenuItem NewMenu
        {
            get 
            { 
                if (newMenu == null)
                {
                    newMenu = GetDescendant("New").AsMenuItem();
                }
                return newMenu;
            }
        }
        MenuItem OpenMenu
        {
            get 
            { 
                if (openMenu == null)
                {
                    openMenu = GetDescendant("Open...").AsMenuItem();
                }
                return openMenu;
            }
        }
        MenuItem SaveAsMenu
        {
            get 
            { 
                if (saveAsMenu == null)
                {
                    saveAsMenu = GetDescendant("Save As").AsMenuItem();
                }
                return saveAsMenu;
            }
        }

        Window ModalWindow_Open
        { 
            get
            {
                if (modalWindow_Open == null)
                {
                    modalWindow_Open = GetModalWindow("Open"); ;
                }
                return modalWindow_Open;
            }
        }
        Window ModalWindow_SaveAs
        {
            get
            {
                if (modalWindow_SaveAs == null)
                {
                    modalWindow_SaveAs = GetModalWindow("Save As");
                }
                return modalWindow_SaveAs;
            }
        }


        public NotepadManager(UIA3Automation automation) : base(NotepadExecutableFileName, automation)
        {
        }
        public NotepadManager(UIA2Automation automation) : base(NotepadExecutableFileName, automation)
        {
        }

        /// <summary>
        /// Opens an existing file by interacting with the application's file menu and open dialog.
        /// </summary>
        /// <param name="filePath">The full path of the file to open. This must be a valid, accessible file path.</param>
        public void OpenExistingFile(string filePath)
        {
            SelectMenuItem(FileMenu);
            SelectMenuItem(OpenMenu);
            Thread.Sleep(1000);
            TextBox fileNameTextBox = GetModalWindowDescendant(ModalWindow_Open, "File name:", ControlType.Edit).AsTextBox();
            fileNameTextBox.Enter(filePath);
            Keyboard.Type(VirtualKeyShort.ENTER);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Opens a new page by navigating through the "File" menu and selecting the "New" option.
        /// </summary>
        public void OpenNewPage()
        {
            SelectMenuItem(FileMenu);
            SelectMenuItem(NewMenu);
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
            TextBox fileNameTextBox = GetModalWindowDescendant(ModalWindow_SaveAs, "File name:", ControlType.Edit).AsTextBox();
            fileNameTextBox.Enter(filePath);
            Keyboard.Type(VirtualKeyShort.ENTER);
            Thread.Sleep(2000);
        }
    }
}
