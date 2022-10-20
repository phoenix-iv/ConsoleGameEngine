using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Tools.ImageEditor.Pages
{
    public partial class Index
    {
        private Graphics.Image? image;

        private async Task FileOpenMenuClick()
        {
            var fileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "txt" } },
                { DevicePlatform.iOS, new[] { "txt" } },
                { DevicePlatform.MacCatalyst, new[] { "txt" } },
                { DevicePlatform.macOS, new[] { "txt" } },
                { DevicePlatform.Tizen, new[] { "txt" } },
                { DevicePlatform.tvOS, new[] { "txt" } },
                { DevicePlatform.watchOS, new[] { "txt" } },
                { DevicePlatform.WinUI, new[] { "txt" } }
            });
            var options = new PickOptions { PickerTitle = "Please select a file", FileTypes = fileTypes };
            FileResult? result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                image = new Graphics.Image(File.ReadAllLines(result.FullPath));
            }
        }


    }
}
