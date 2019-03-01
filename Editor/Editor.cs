using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Editor {
    public class Editor : IPlugin.IPlugin {

        private Editor_Control _control = new Editor_Control();
        private MenuItem _menuItem = new MenuItem();
        private ToolBar _toolBar = null;

        public Editor() {
            InitializeMenu();
        }

        private void InitializeMenu() {
            this._menuItem.Header = "Editor";
            MenuItem upItem = new MenuItem {
                Header = "Up"
            };
            upItem.Click += Up_Click;
            this._menuItem.Items.Add(upItem);
            MenuItem lowItem = new MenuItem();
            lowItem = new MenuItem {
                Header = "Low"
            };
            lowItem.Click += Low_Click;
            this._menuItem.Items.Add(lowItem);
        }

        private void Low_Click(object sender, System.Windows.RoutedEventArgs e) {
            _control.Text = _control.Text.ToLower();
        }

        private void Up_Click(object sender, System.Windows.RoutedEventArgs e) {
            _control.Text = _control.Text.ToUpper();
        }

        public Control Control { get => _control; }
        public MenuItem MenuItem { get => _menuItem; }
        public ToolBar ToolBar { get => _toolBar; }
        public string Name => "Editor";
    }
}
