using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Flower;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


  

    private async void Button_OnClick(object sender, RoutedEventArgs e)
    {
        
        await Task.Run(() => 
        {
            var password = PasswordTextBox.Text;
            string stringconnection = "Server=localhost;Database=FlowerDB;User Id=root;Password=;";
            string query = "SELECT Login,Password,Role  FROM User WHERE `Login` = @username AND `Password` = @password And `Role` = @role";
            string passwordDB = PasswordTextBox.Text;
            string loginDB = LoginTextBox.Text;
                    
            MySqlConnection con = new MySqlConnection(stringconnection);
             con.OpenAsync();
        
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", loginDB);
            cmd.Parameters.AddWithValue("@password", passwordDB);
            
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Hide();
             //   MainEmpty empty = new MainEmpty();
                //empty.Show();
                this.Close();
            }
            else
            {
            //    OshibkaLabel.Content = "Неверно введена почта или пароль";
            }
        });
        
    }
}