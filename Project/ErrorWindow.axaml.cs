using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Project
{
    public partial class ErrorWindow : Window
    {
        public string ErrorText { set; get; } = string.Empty; 

        public ErrorWindow()
        {
            InitializeComponent();
        }

        public void Update()
        {
            Error.Text = ErrorText;
        }
        
    }
}