using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Project.BD;
using Avalonia.Styling;

namespace Project
{
    public partial class MainWindow : Window
    {
        Database database = new ();

        public MainWindow()
        {
            database.LoadDataUsers();
            database.LoadDataMessages();
            InitializeComponent();
        }

        public void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (!Fields_Is_Fill())
            {
                ErrorWindow errorWindow = new () { ErrorText = "Не все поля заполнены!" };
                errorWindow.Update();
                errorWindow.Show();
            }    
            else
            {
                Login.Foreground = Avalonia.Media.Brushes.Black;
                bool Error = false;
                foreach (var User in database.Users)
                {
                    if (User.Login == Login.Text)
                    {
                        Error = true;
                        ErrorWindow errorWindow = new () { ErrorText = "Пользователь с ткаим именем уже существует!" };
                        errorWindow.Update();
                        errorWindow.Show();
                        Login.Foreground = Avalonia.Media.Brushes.Red;
                        break;
                    }
                }
                if (!Error)
                {
                    TableUsers TableUser = new () { Login = Login.Text, Password = Password.Text };
                    database.InsertDataUsers(TableUser);
                }
            }
        }

        public void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (!Fields_Is_Fill())
            {
                ErrorWindow errorWindow = new() { ErrorText = "Не все поля заполнены!" };
                errorWindow.Update();
                errorWindow.Show();
            }
            else
            {
                bool search = false;
                foreach (var user in database.Users)
                {
                    if (user.Login == Login.Text)
                    {
                        search = true;
                        if (user.Password != Password.Text)
                        {
                            ErrorWindow errorWindow = new() { ErrorText = "Неверный пароль!" };
                            errorWindow.Update();
                            errorWindow.Show();
                            break;
                        }
                        
                        ChatWindow chatWindow = new() { UserName = Login.Text, database = database};
                        chatWindow.Update();
                        chatWindow.Show();
                    }
                }
                if (!search)
                {
                    ErrorWindow errorWindow = new() { ErrorText = "Такого пользователя не существует!" };
                    errorWindow.Update();
                    errorWindow.Show();
                }
            }
        }
        Window window = new ();
        public bool Fields_Is_Fill()
        {
            
            if (Login.Text == null || Password.Text == null)
                return false;
            return true;
        }
    }
}