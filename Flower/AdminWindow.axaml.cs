using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Flower;

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
        LoadOrders();
    }


    private void CreateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Create create = new Create();
        Hide();
        create.Show();
        this.Close();
    }

    private void ListEploeeButton(object? sender, RoutedEventArgs e)
    {
        List_of_employees list = new List_of_employees();
        Hide();
        list.Show();
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
            string a;

            if (order.ReadyForDelivery == "0")
            {
                a = "Не готов к доставке";
            }
            else
            { 
                a = "Готов к доставке";
            }
           string orderInfo = $"{order.Id} | {order.Name} | {order.Adress} | {order.PaymentStatus} | {order.Accepted} | {a}  ";
            OrdersListBox.Items.Add(orderInfo); // Просто добавляем текст
        }
    }
    private List<Order> GetOrdersFromDatabase()
    {
        var orders = new List<Order>();

        var conn = new MySqlConnection("Server=localhost;Database=FlowerDB;User Id=root;Password=;");
        {
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM FlowerDB.Task", conn);
            var reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        Id = reader.GetInt32("ID"),
                        Name = reader.GetString("Name"),
                        Adress = reader.GetString("Adress"),
                        PaymentStatus = reader.GetString("PaymentStatus"),
                        Accepted = reader.GetString("Accepted"),
                        ReadyForDelivery = reader.GetString("ReadyForDelivery")
                    });
                }
            }
        }

        return orders;
    }





    


    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Adress { get; set; }

        public string PaymentStatus { get; set; }

        public string Accepted { get; set; }

        public string ReadyForDelivery { get; set; }

    }
}
