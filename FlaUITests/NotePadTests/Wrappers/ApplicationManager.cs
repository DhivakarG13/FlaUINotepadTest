using System;
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
            Application = Application.Launch(@"notepad.exe");
            AutomationConditionFactory = automation.ConditionFactory;
            MainWindow = Application.GetMainWindow(automation);
        }

        public ApplicationManager(string executable, UIA2Automation automation)
        {
            Application = Application.Launch(@"notepad.exe");
            AutomationConditionFactory = automation.ConditionFactory;
            MainWindow = Application.GetMainWindow(automation);
        }

        
        // Fetch Data from Elements
        public string GetValueFromTextBox(TextBox textBox)
        {
            ITextPattern textPattern = textBox.Patterns.Text.PatternOrDefault;
            return textPattern.DocumentRange.GetText(-1);
        }


        //KeyBoard Operations
        public void CopyContentToClipBoard()
        {
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_C);
        }

        public void PasteContentFromClipBoard()
        {
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
        }


        // Search Operations
        public AutomationElement GetDescendant(string Name, ControlType controlType, string AutomationID, string className) 
        { 
            return MainWindow.FindFirstDescendant(AutomationConditionFactory.ByAutomationId(AutomationID)
                .And(AutomationConditionFactory.ByClassName(className)
                .And(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name)))));
        }

        public AutomationElement GetDescendant(string Name, ControlType controlType, string AutomationID) 
        {
            return MainWindow.FindFirstDescendant(AutomationConditionFactory.ByAutomationId(AutomationID)
                .And(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name))));
        }

        public AutomationElement GetDescendant(string Name, ControlType controlType) 
        {
            return MainWindow.FindFirstDescendant(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name)));
        }

        public AutomationElement GetDescendant(string Name) 
        {
            return MainWindow.FindFirstDescendant(AutomationConditionFactory.ByName(Name));
        }

        public Window GetModalWindow(string windowTitle) 
        {
            return MainWindow.ModalWindows.FirstOrDefault(modalWindow => modalWindow.Title.Contains(windowTitle));
        }

        public AutomationElement GetModalWindowDescendant(Window modalWindow, string Name, ControlType controlType, string AutomationID, string className)
        {
            return modalWindow.FindFirstDescendant(AutomationConditionFactory.ByAutomationId(AutomationID)
                .And(AutomationConditionFactory.ByClassName(className)
                .And(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name)))));
        }

        public AutomationElement GetModalWindowDescendant(Window modalWindow, string Name, ControlType controlType, string AutomationID)
        {
            return modalWindow.FindFirstDescendant(AutomationConditionFactory.ByAutomationId(AutomationID)
                .And(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name))));
        }

        public AutomationElement GetModalWindowDescendant(Window modalWindow, string Name, ControlType controlType)
        {
            return modalWindow.FindFirstDescendant(AutomationConditionFactory.ByControlType(controlType)
                .And(AutomationConditionFactory.ByName(Name)));
        }

        public AutomationElement GetModalWindowDescendant(Window modalWindow, string Name)
        {
            return modalWindow.FindFirstDescendant(AutomationConditionFactory.ByName(Name));
        }

        // Dispose
        public void Dispose()
        {
            Application.Close();
            Application.Dispose();
        }
    }
}
