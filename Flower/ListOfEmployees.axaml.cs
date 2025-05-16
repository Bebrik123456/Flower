using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Flower;

public partial class List_of_employees : Window
{
    public List_of_employees()
    {
        InitializeComponent();
        LoadOrders();
    }


    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        AdminWindow adminWindow = new AdminWindow();
        Hide();
        adminWindow.Show();
        this.Close();
    }
    private void LoadOrders()
    {
        // 1. Очищаем список
        OrdersListBox.Items.Clear();

        // 2. Получаем данные из БД
        var orders = GetOrdersFromDatabase();

        // 3. Заполняем ListBox вручную
        foreach (var order in orders)
        {
            // Формируем строку для отображения
            string orderInfo = $"{order.Id} | {order.Login} | {order.Adress} | {order.Role} ";
            OrdersListBox.Items.Add(orderInfo); // Просто добавляем текст
        }
    }
    private List<Order> GetOrdersFromDatabase()
    {
        var orders = new List<Order>();

        var conn = new MySqlConnection("Server=localhost;Database=FlowerDB;User Id=root;Password=;");
        {
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM FlowerDB.User", conn);
            var reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        Id = reader.GetInt32("ID"),
                        Login = reader.GetString("Login"),
                        Adress = reader.GetString("Adress"),
                        Role = reader.GetString("Role")
                    });
                }
            }
        }
        
        return orders;
    }
    public class Order
    {
        public int Id { get; set; }
        
        public string Login { get; set; }
    
        public string Role { get; set; }
    
        public string Adress { get; set; }
    
   
    
    }
}
