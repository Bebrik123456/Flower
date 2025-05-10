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

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var password = PasswordTextBox.Text;
            string stringconnection = "Server=localhost;Database=FlowerDB;User Id=root;Password=;";
            string query = "SELECT Login, Password, Role FROM User WHERE `Login` = @username AND `Password` = @password";
            string passwordDB = PasswordTextBox.Text;
            string loginDB = LoginTextBox.Text;

            MySqlConnection con = new MySqlConnection(stringconnection);
            con.Open();

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", loginDB);
            cmd.Parameters.AddWithValue("@password", passwordDB);

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int role = reader.GetInt32("Role"); 

                Hide();
                
                if (role == 0)
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else if (role == 1)
                {
                    EmployeeWindow emploeeWindow = new EmployeeWindow();
                    emploeeWindow.Show();
                }
                else if (role == 2)
                {
                    Delivery deliveryWindow = new Delivery();
                    deliveryWindow.Show();
                }
                
                this.Close();
            }
            else
            {
                OshibkaLabel.Content = "Неверно введена почта или пароль";
            }
        }
        catch (Exception ex)
        {
            OshibkaLabel.Content = ex.Message;
        }
    }
}