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

        public double WindowFetchMaxWaitTime { get; set; } = 5000;

        //Constructors
        //public ApplicationManager(string executable, UIA3Automation automation)
        public ApplicationManager(string executable, AutomationBase automationBase)
        {
            try
            {
                UIA2Automation UIA2Automation;
                UIA3Automation UIA3Automation;
                if (automationBase is UIA2Automation)
                {
                    UIA2Automation = new UIA2Automation();
                    Application = Application.Launch(executable);
                    AutomationConditionFactory = UIA2Automation.ConditionFactory;
                    MainWindow = Application.GetMainWindow(UIA2Automation, TimeSpan.FromMilliseconds(WindowFetchMaxWaitTime));
                }
                if (automationBase is UIA3Automation)
                {
                    UIA3Automation = new UIA3Automation();
                    Application = Application.Launch(executable);
                    AutomationConditionFactory = UIA3Automation.ConditionFactory;
                    MainWindow = Application.GetMainWindow(UIA3Automation, TimeSpan.FromMilliseconds(WindowFetchMaxWaitTime));
                }
            }
            catch (ArgumentException)
            {
                throw new Exception("Internal Error, The attributes provided to Application while launching may be null.");

            }
            catch (FileNotFoundException)
            {
                throw new Exception("Internal Error, The .exe file is not found");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Problem in attaching the process to Application");
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected Error,\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        // Search Operations
        /// <summary>
        /// Retrieves a descendant automation element from the main window that matches the specified criteria.
        /// </summary>
        /// <param name="Name">The name of the automation element to search for. Cannot be <see langword="null"/>.</param>
        /// <param name="controlType">The control type of the automation element to search for.</param>
        /// <param name="AutomationID">The automation ID of the element to search for. Cannot be <see langword="null"/>.</param>
        /// <param name="className">The class name of the automation element to search for. Cannot be <see langword="null"/>.</param>
        /// <returns>The first descendant <see cref="AutomationElement"/> that matches the specified criteria.</returns>
        /// <exception cref="Exception"></exception>
        public AutomationElement GetDescendant(string Name, FlaUI.Core.Definitions.ControlType controlType, string AutomationID, string className)
        {
            if (MainWindow == null)
            {
                throw new Exception("The main window is null,the process is ended or not started");
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
                throw new Exception($"UnExpected error occurred when filtering modal window's descendants.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }

        }

        /// <summary>
        /// Retrieves a descendant automation element from the main window that matches the specified criteria.
        /// </summary>
        /// <param name="Name">The name of the automation element to search for. Cannot be <see langword="null"/>.</param>
        /// <param name="controlType">The control type of the automation element to search for.</param>
        /// <param name="AutomationID">The automation ID of the element to search for. Cannot be <see langword="null"/>.</param>
        /// <returns>The first descendant <see cref="AutomationElement"/> that matches the specified criteria.</returns>
        /// <exception cref="Exception"></exception>
        public AutomationElement GetDescendant(string Name, ControlType controlType, string AutomationID)
        {
            if (MainWindow == null)
            {
                throw new Exception("The main window is null,the process is ended or not started");
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
                throw new Exception($"UnExpected error occurred when filtering window's descendants.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a descendant automation element from the main window that matches the specified criteria.
        /// </summary>
        /// <param name="Name">The name of the automation element to search for. Cannot be <see langword="null"/>.</param>
        /// <param name="controlType">The control type of the automation element to search for.</param>
        /// <returns>The first descendant <see cref="AutomationElement"/> that matches the specified criteria.</returns>
        /// <exception cref="Exception"></exception>
        public AutomationElement GetDescendant(string Name, ControlType controlType)
        {
            if (MainWindow == null)
            {
                throw new Exception("The main window is null,the process is ended or not started");
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
                throw new Exception($"UnExpected error occurred when filtering window's descendants.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a descendant automation element from the main window that matches the specified criteria.
        /// </summary>
        /// <param name="Name">The name of the automation element to search for. Cannot be <see langword="null"/>.</param>
        /// <returns>The first descendant <see cref="AutomationElement"/> that matches the specified criteria.</returns>
        /// <exception cref="Exception"></exception>
        public AutomationElement GetDescendant(string Name)
        {
            if (MainWindow == null)
            {
                throw new Exception("The main window is null,the process is ended or not started");
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
                throw new Exception($"UnExpected error occurred when filtering window's descendants.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a modal window with a title that contains the specified substring.
        /// </summary>
        /// <param name="windowTitle">The substring to search for in the titles of modal windows. This value cannot be <see langword="null"/>.</param>
        /// <returns>The first modal window whose title contains the specified substring, or <see langword="null"/> if no such
        /// window is found.</returns>
        /// <exception cref="Exception"></exception>
        public Window GetModalWindow(string windowTitle)
        {
            if (MainWindow == null)
            {
                throw new Exception("The main window is null,the process is ended or not started");
            }

            if (windowTitle == null)
            {
                throw new Exception("Null values are passed for filtering ModalWindow, which is invalid.");
            }

            try
            {
                return MainWindow.ModalWindows.FirstOrDefault(modalWindow => modalWindow.Title.Equals(windowTitle));
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occurred when fetching modal window.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a descendant automation element from the specified modal window that matches the provided
        /// filtering criteria.
        /// </summary>
        /// <param name="modalWindow">The modal window from which to search for the descendant element. Must not be <see langword="null"/>.</param>
        /// <param name="Name">The name of the descendant element to locate. Must not be <see langword="null"/>.</param>
        /// <param name="controlType">The control type of the descendant element to locate.</param>
        /// <param name="AutomationID">The automation ID of the descendant element to locate. Must not be <see langword="null"/>.</param>
        /// <param name="className">The class name of the descendant element to locate. Must not be <see langword="null"/>.</param>
        /// <returns>The <see cref="AutomationElement"/> that matches the specified criteria, or <see langword="null"/> if no
        /// matching element is found.</returns>
        /// <exception cref="Exception"></exception>
        public AutomationElement GetWindowDescendant(Window modalWindow, string Name, ControlType controlType, string AutomationID, string className)
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
                throw new Exception($"UnExpected error occurred when filtering modal window's descendants.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a descendant automation element from the specified modal window that matches the provided
        /// filtering criteria.
        /// </summary>
        /// <param name="modalWindow">The modal window from which to search for the descendant element. Must not be <see langword="null"/>.</param>
        /// <param name="Name">The name of the descendant element to locate. Must not be <see langword="null"/>.</param>
        /// <param name="controlType">The control type of the descendant element to locate.</param>
        /// <param name="AutomationID">The automation ID of the descendant element to locate. Must not be <see langword="null"/>.</param>
        /// <returns>The <see cref="AutomationElement"/> that matches the specified criteria, or <see langword="null"/> if no
        /// matching element is found.</returns>
        /// <exception cref="Exception"></exception>
        public AutomationElement GetWindowDescendant(Window modalWindow, string Name, ControlType controlType, string AutomationID)
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
                throw new Exception($"UnExpected error occurred when filtering modal window's descendants.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a descendant automation element from the specified modal window that matches the provided
        /// filtering criteria.
        /// </summary>
        /// <param name="modalWindow">The modal window from which to search for the descendant element. Must not be <see langword="null"/>.</param>
        /// <param name="Name">The name of the descendant element to locate. Must not be <see langword="null"/>.</param>
        /// <param name="controlType">The control type of the descendant element to locate.</param>
        /// <returns>The <see cref="AutomationElement"/> that matches the specified criteria, or <see langword="null"/> if no
        /// matching element is found.</returns>
        /// <exception cref="Exception"></exception>
        public AutomationElement GetWindowDescendant(Window modalWindow, string Name, ControlType controlType)
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
                throw new Exception($"UnExpected error occurred when filtering modal window's descendants.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a descendant automation element from the specified modal window that matches the provided
        /// filtering criteria.
        /// </summary>
        /// <param name="modalWindow">The modal window from which to search for the descendant element. Must not be <see langword="null"/>.</param>
        /// <param name="Name">The name of the descendant element to locate. Must not be <see langword="null"/>.</param>
        /// <returns>The <see cref="AutomationElement"/> that matches the specified criteria, or <see langword="null"/> if no
        /// matching element is found.</returns>
        /// <exception cref="Exception"></exception>
        public AutomationElement GetWindowDescendant(Window modalWindow, string Name)
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
                throw new Exception($"UnExpected error occurred when filtering modal window's descendants.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        // Fetch Data from Elements
        /// <summary>
        /// Retrieves the text content from the specified <see cref="TextBox"/> control.
        /// </summary>
        /// <param name="textBox">The <see cref="TextBox"/> control from which to retrieve the text.</param>
        /// <returns>The text content of the <see cref="TextBox"/>. If the <see cref="TextBox"/> is empty, an empty string is
        /// returned.</returns>
        /// <exception cref="Exception"></exception>
        public string GetValueFromTextBox(TextBox textBox)
        {
            try
            {
                ITextPattern textPattern = textBox.Patterns.Text.PatternOrDefault;
                return textPattern.DocumentRange.GetText(-1);
            }
            catch (Exception ex)
            {
                throw new Exception($"UnExpected error occurred when fetching data from {textBox}.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        //KeyBoard Operations
        /// <summary>
        /// Copies the currently focused content to the clipboard by simulating keyboard shortcuts.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void CopyFocusedContentToClipBoard()
        {
            try
            {
                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_C);
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Error while CopyContentToClipBoard().\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Simulates a paste operation by sending a keyboard shortcut for pasting content from the clipboard.
        /// </summary>
        /// <exception cref="Exception"></exception>
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

        //MouseOperations
        /// <summary>
        /// Simulates a click action on the specified UI automation element.
        /// </summary>
        /// <param name="element">The <see cref="AutomationElement"/> representing the UI element to be clicked. Must not be <see
        /// langword="null"/>.</param>
        /// <exception cref="Exception"></exception>
        public void ClickButton(AutomationElement element)
        {
            try
            {
                element.AsButton().Invoke();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while trying to click on the element.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Selects a menu item represented by the specified <see cref="AutomationElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="AutomationElement"/> representing the menu item to be selected.  This parameter cannot be
        /// <see langword="null"/>.</param>
        /// <exception cref="Exception">Thrown if an error occurs while attempting to select the menu item. The exception message provides
        /// additional details.</exception>
        public void SelectMenuItem(AutomationElement element)
        {
            try
            {
                element.AsMenuItem().Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while trying to select the menu item.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
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
                throw new Exception($"Error occurred while trying to close the application.\n Error type: {ex.ToString()},\n Error message: {ex.Message}");
            }
        }

    }
}
