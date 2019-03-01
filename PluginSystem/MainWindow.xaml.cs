using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace PluginSystem {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        System.Collections.Generic.List<IPlugin.IPlugin> plugins
                = new System.Collections.Generic.List<IPlugin.IPlugin>();

        public MainWindow() {
            InitializeComponent();
            LoadPlugin();
        }

        private void LoadPlugin() {
            
            foreach (var file in Directory.GetFiles(@"plugins\", "*.dll")) {
                Assembly assembly = Assembly.LoadFrom(file);

                IEnumerable<Type> types = assembly.GetTypes();
                foreach (var type in types) {
                    bool flag = type.GetInterfaces().Contains(typeof(IPlugin.IPlugin));
                    if (flag) {
                        if (Activator.CreateInstance(type) is IPlugin.IPlugin plugin) {

                            plugins.Add(plugin);
                            
                            if (plugin.Control != null) {
                                LayoutAnchorable layoutAnchorable = new LayoutAnchorable();
                                layoutAnchorable.Title = plugin.Name;
                                layoutAnchorable.Content = plugin.Control;
                                anchorablePane.Children.Add(layoutAnchorable);
                                
                                System.Windows.Controls.MenuItem menuItem = new System.Windows.Controls.MenuItem();
                                menuItem.Header = plugin.Name;
                                menuItem.DataContext = layoutAnchorable;
                                menuItem.Click += MenuItem_Click;
                                menuItem_Plugins.Items.Add(menuItem);
                            }

                            if (plugin.MenuItem != null) {
                                this.menu.Items.Add(plugin.MenuItem);
                            }

                            if (plugin.ToolBar != null) {
                                mainGrid.Children.Add(plugin.ToolBar);
                            }
                            
                        }
                    }
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            if (!(sender as System.Windows.Controls.MenuItem).IsChecked) {
                ((sender as System.Windows.Controls.MenuItem).DataContext as LayoutAnchorable).Show(); 
            } else {
                ((sender as System.Windows.Controls.MenuItem).DataContext as LayoutAnchorable).Hide();
            }
            (sender as System.Windows.Controls.MenuItem).IsChecked = !((sender as System.Windows.Controls.MenuItem).IsChecked);
        }
    }
}
