using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class WrapperClass : IDisposable
    {
        public Application Application { get; set; }
        public UIA3Automation UIA3Automation { get; set; }
        public UIA2Automation UIA2Automation { get; set; }
        public Window MainWindow { get; set; }
        public ConditionFactory AutomationConditionFactory { get; set; }

        public WrapperClass(string executable, UIA3Automation automation)
        {
            Application = Application.Launch(@"notepad.exe");
            UIA3Automation = automation;
            AutomationConditionFactory = UIA3Automation.ConditionFactory;
        }

        public WrapperClass(string executable, UIA2Automation automation)
        {
            Application = Application.Launch(@"notepad.exe");
            UIA2Automation = automation;
            AutomationConditionFactory = UIA3Automation.ConditionFactory;
        }

        public void CopyContentToClipBoard()
        {
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_C);
        }

        public void PasteContentFromClipBoard()
        {
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
        }

        public T GetElementByName<T>(string automationElementName) where T : AutomationElement
        {
            return MainWindow.FindFirstDescendant(AutomationConditionFactory.ByName(automationElementName)) as T;
        }
        public string GetValueFromTextBox(TextBox textBox)
        {
            ITextPattern textPattern = textBox.Patterns.Text.PatternOrDefault;
            return textPattern.DocumentRange.GetText(-1);
        }

        public void SetFocus(string name, ControlType controlType) { }

        public void Dispose()
        {
            Application.Close();
            Application.Dispose();
        }

        public T GetElementByNameFromWindow<T>(Window modalWindow, string automationElementName) where T : AutomationElement
        {
            return modalWindow.FindFirstDescendant(AutomationConditionFactory.ByName(automationElementName)) as T;
        }
    }
}
