using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Project.BD;
using Avalonia.Styling;

namespace Project
{
    public partial class ChatWindow : Window
    {
        public string UserName { get; set; } = string.Empty;
        public Database database;

        public ChatWindow()
        {
            InitializeComponent();
        }
        
        public void Update()
        {
            foreach (var message in database.Messages)
            {
                Chat.Text += message.Login + ": " + message.Messsage + "\n";
            }
        }

        public void Send_Click(object sender, RoutedEventArgs e)
        {
            Chat.Text += UserName + ": " + Message.Text + "\n";
            TableMessages message = new() { Login = UserName, Messsage = Message.Text };
            database.InsertDataMessages(message);
            Message.Clear();
        }
        
    }
}