using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;

namespace IPlugin {
    public interface IPlugin {
        Control Control { get; }
        MenuItem MenuItem { get; }
        ToolBar ToolBar { get; }
        string Name { get; }
    }
}
