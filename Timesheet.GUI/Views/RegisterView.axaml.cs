using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Timesheet.GUI.Views
{
    public class RegisterView : UserControl
    {
        public RegisterView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
