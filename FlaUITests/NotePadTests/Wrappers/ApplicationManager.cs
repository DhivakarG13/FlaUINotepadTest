using System;
using System.IO;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Patterns;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA2;
using FlaUI.UIA3;

namespace NotePadTests.Wrappers
{
    public class ApplicationManager : IDisposable
    {
        public Application Application { get; set; }

        public Window MainWindow { get; set; }

        public ConditionFactory AutomationConditionFactory { get; set; }

        //Constructors
        public ApplicationManager(string executable, UIA3Automation automation)
        {
            try
            {
                Application = Application.Launch(@"notepad.exe");
                AutomationConditionFactory = automation.ConditionFactory;
                MainWindow = Application.GetMainWindow(automation);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Internal Error, The attributes provided to Application while launching may be null.");

            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("Internal Error, The .exe file is not found");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Problem in attaching the process to Application");
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected Error,\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        public ApplicationManager(string executable, UIA2Automation automation)
        {
            try
            {
                Application = Application.Launch(@"notepad.exe");
                AutomationConditionFactory = automation.ConditionFactory;
                MainWindow = Application.GetMainWindow(automation);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Internal Error, The attributes provided to Application while launching may be null.");

            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("Internal Error, The .exe file is not found");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Problem in attaching the process to Application");
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected Error,\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        // Search Operations
        public AutomationElement GetDescendant(string Name, ControlType controlType, string AutomationID, string className)
        {
            if(MainWindow == null)
            {
                throw new Exception("The mainwindow is null,the process is ended or not started");
            }

            if (AutomationID == null || Name == null || className == null)
            {
                throw new Exception("Null values are passed for filtering, which is invalid.");
            }

            try
            {
                return MainWindow.FindFirstDescendant(AutomationConditionFactory.ByAutomationId(AutomationID)
                    .And(AutomationConditionFactory.ByClassName(className)
                    .And(AutomationConditionFactory.ByControlType(controlType)
                    .And(AutomationConditionFactory.ByName(Name)))));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when filtering window's descendents.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }

        }

        public AutomationElement GetDescendant(string Name, ControlType controlType, string AutomationID)
        {
            if (MainWindow == null)
            {
                throw new Exception("The mainwindow is null,the process is ended or not started");
            }

            if (AutomationID == null || Name == null)
            {
                throw new Exception("Null values are passed for filtering, which is invalid.");
            }

            try
            {
                return MainWindow.FindFirstDescendant(AutomationConditionFactory.ByAutomationId(AutomationID)
                .And(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name))));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when filtering window's descendents.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        public AutomationElement GetDescendant(string Name, ControlType controlType)
        {
            if (MainWindow == null)
            {
                throw new Exception("The mainwindow is null,the process is ended or not started");
            }

            if (Name == null)
            {
                throw new Exception("Null values are passed for filtering, which is invalid.");
            }

            try
            {
                return MainWindow.FindFirstDescendant(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name)));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when filtering window's descendents.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        public AutomationElement GetDescendant(string Name)
        {
            if (MainWindow == null)
            {
                throw new Exception("The mainwindow is null,the process is ended or not started");
            }

            if (Name == null)
            {
                throw new Exception("Null values are passed for filtering, which is invalid.");
            }

            try
            {
                return MainWindow.FindFirstDescendant(AutomationConditionFactory.ByName(Name));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when filtering window's descendents.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        public Window GetModalWindow(string windowTitle)
        {
            if (MainWindow == null)
            {
                throw new Exception("The mainwindow is null,the process is ended or not started");
            }

            if (windowTitle == null)
            {
                throw new Exception("Null values are passed for filtering ModalWindow, which is invalid.");
            }

            try
            {
                return MainWindow.ModalWindows.FirstOrDefault(modalWindow => modalWindow.Title.Contains(windowTitle));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when fetching modal window.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        public AutomationElement GetModalWindowDescendant(Window modalWindow, string Name, ControlType controlType, string AutomationID, string className)
        {
            if (modalWindow == null)
            {
                throw new Exception("The modalWindow is null,the modal Window is not opened or not not active");
            }

            if (AutomationID == null || Name == null || className == null)
            {
                throw new Exception("Null values are passed for filtering, which is invalid.");
            }

            try
            {
                return modalWindow.FindFirstDescendant(AutomationConditionFactory.ByAutomationId(AutomationID)
                .And(AutomationConditionFactory.ByClassName(className)
                .And(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name)))));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when filtering modal window's descendents.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        public AutomationElement GetModalWindowDescendant(Window modalWindow, string Name, ControlType controlType, string AutomationID)
        {
            if (modalWindow == null)
            {
                throw new Exception("The modalWindow is null,the modal Window is not opened or not not active");
            }

            if (AutomationID == null || Name == null)
            {
                throw new Exception("Null values are passed for filtering, which is invalid.");
            }

            try
            {
                return modalWindow.FindFirstDescendant(AutomationConditionFactory.ByAutomationId(AutomationID)
                .And(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name))));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when filtering modal window's descendents.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        public AutomationElement GetModalWindowDescendant(Window modalWindow, string Name, ControlType controlType)
        {
            if (modalWindow == null)
            {
                throw new Exception("The modalWindow is null,the modal Window is not opened or not not active");
            }

            if (Name == null)
            {
                throw new Exception("Null values are passed for filtering, which is invalid.");
            }

            try
            {
                return modalWindow.FindFirstDescendant(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name)));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when filtering modal window's descendents.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        public AutomationElement GetModalWindowDescendant(Window modalWindow, string Name)
        {
            if (modalWindow == null)
            {
                throw new Exception("The modalWindow is null,the modal Window is not opened or not not active");
            }

            if (Name == null)
            {
                throw new Exception("Null values are passed for filtering, which is invalid.");
            }

            try
            {
                return modalWindow.FindFirstDescendant(AutomationConditionFactory.ByName(Name));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when filtering modal window's descendents.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        // Fetch Data from Elements
        public string GetValueFromTextBox(TextBox textBox)
        {
            try
            {
                ITextPattern textPattern = textBox.Patterns.Text.PatternOrDefault;
                return textPattern.DocumentRange.GetText(-1);
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occured when fetching data from {textBox}.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        //KeyBoard Operations
        public void CopyFocusedContentToClipBoard()
        {
            try
            {
                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_C);
            }
            catch(Exception ex)
            {
                throw new Exception($"Internal Error while CopyContentToClipBoard().\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        public void PasteContentFromClipBoard()
        {
            try
            {
                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Error while PasteContentFromClipBoard().\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }
    
        // Dispose
        public void Dispose()
        {
            try
            {
                Application.Close();
                Application.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while trying to close the application.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }
    }
}
