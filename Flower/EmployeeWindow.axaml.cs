using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Flower;

public partial class EmployeeWindow : Window
{
    public EmployeeWindow()
    {
        InitializeComponent();
        LoadOrders();
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
            string orderInfo = $"{order.Id} | {order.Name} | {order.Adress}";
            OrdersListBox.Items.Add(orderInfo); // Просто добавляем текст
        }
    }
    
    
    public void ListClick()
    {
        
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
                        Accepted= reader.GetString("Accepted"),
                        ReadyForDelivery = reader.GetString("ReadyForDelivery")
                    });
                }
            }
        }
        
        return orders;
    }

    private void OrdersListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var listBox = sender as ListBox;
        if (listBox != null && listBox.SelectedItem != null)
        {
            string selectedItem = listBox.SelectedItem.ToString();
          //  InsertIntoDatabase(selectedItem);
        }
    }
 
   
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

    

