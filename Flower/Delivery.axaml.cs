using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Flower;

public partial class Delivery : Window
{
   

    public Delivery()
    {
        InitializeComponent();
        LoadOrders();
      
    }
    
    private void MyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Получаем выбранный элемент
        var selectedItem = OrdersListBox.SelectedItem as string;
        if (selectedItem != null)
        {
            // Извлекаем ID заказа из строки (предполагаем формат "ID | Name | Address...")
            var parts = selectedItem.Split('|');
            if (parts.Length > 0 && int.TryParse(parts[0].Trim(), out int orderId))
            {
                // Удаляем из БД
                DeleteFromDatabase(orderId);
            
                // Удаляем из ListBox
                OrdersListBox.Items.Remove(selectedItem);
            }
        }
    }

    private void DeleteFromDatabase(int orderId)
    {
        string connectionString = "Server=localhost;Database=FlowerDB;User Id=root;Password=;";
        string query = "DELETE FROM Task WHERE ID = @OrderId;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.ExecuteNonQuery();
            }
        }
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
            string orderInfo = $"{order.Id} | {order.Name} | {order.Adress} | {order.PaymentStatus} | {order.Accepted} | {order.ReadyForDelivery} ";
            OrdersListBox.Items.Add(orderInfo); // Просто добавляем текст
        }
    }
    private List<Order> GetOrdersFromDatabase()
    {
        var orders = new List<Order>();

        var conn = new MySqlConnection("Server=localhost;Database=FlowerDB;User Id=root;Password=;");
        {
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM Task WHERE ReadyForDelivery = '1'", conn);  
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