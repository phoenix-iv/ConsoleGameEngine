namespace ConsoleGameEngine.Tools.ImageEditor
{
    public partial class MainPage : ContentPage
    {
        private readonly ImageController _controller;

        public MainPage()
        {
            InitializeComponent();
            _controller = new ImageController(GraphicsView);
            using var stream = FileSystem.OpenAppPackageFileAsync("Helicopter.txt").Result;
            using var reader = new StreamReader(stream);
            string? line;
            var lines  = new List<string>();
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
            var image = new Graphics.Image(lines.ToArray());
            _controller.SetImage(image);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _controller.Redraw();
        }

        private void GraphicsView_StartHoverInteraction(object sender, TouchEventArgs e)
        {
            _controller.Hover(e);
        }

        private void GraphicsView_MoveHoverInteraction(object sender, TouchEventArgs e)
        {
            _controller.Hover(e);
        }

        private void GraphicsView_EndHoverInteraction(object sender, EventArgs e)
        {
            _controller.EndHover();
        }

    }
}