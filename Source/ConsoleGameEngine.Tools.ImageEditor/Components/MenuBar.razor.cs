using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Tools.ImageEditor.Components
{
    public partial class MenuBar
    {
        [Parameter]
        public EventCallback OnFileOpenMenuClick { get; set; }
        [Parameter]
        public EventCallback OnFileSaveMenuClick { get; set; }
        [Parameter]
        public EventCallback OnFileSaveAsMenuClick { get; set; }

        private bool _collapseMenuBar = true;
        private string MenuBarCssClass => _collapseMenuBar ? "collapse" : "";
        private bool _collapseFileMenu = true;
        private string FileMenuCssClass => _collapseFileMenu ? "" : " show";

        public async Task FileOpenMenuClick() => await OnFileOpenMenuClick.InvokeAsync();
        public async Task FileSaveMenuClick() => await OnFileSaveMenuClick.InvokeAsync();
        public async Task FileSaveAsMenuClick() => await OnFileSaveAsMenuClick.InvokeAsync();

        private void ToggleMenuBar()
        {
            _collapseMenuBar = !_collapseMenuBar;
        }

        private void ToggleFileMenu()
        {
            _collapseFileMenu = !_collapseFileMenu;
        }

    }
}
