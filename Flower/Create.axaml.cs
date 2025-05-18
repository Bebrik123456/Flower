using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MySql.Data.MySqlClient;
using Tmds.DBus.Protocol;

namespace Flower;

public partial class Create : Window
{
    public Create()
    {
        InitializeComponent();
    }

    private void BackButton(object? sender, RoutedEventArgs e)
    {
        Hide();
        AdminWindow adminWindow = new AdminWindow();
        adminWindow.Show();
        this.Close();
      
    }

    public void CreateTask(object sender, RoutedEventArgs e)
    {
        try
        {
            string stringconnection = "Server=localhost;Database=FlowerDB;User Id=root;Password=;";
            var NameTask = NameTextBox.Text;
            var Adress = ComboBox.SelectionBoxItem.ToString();
            string a = Adress;
            int b = 1;

            switch (a)
            {
                case "Пушкино":
                    b = 1;
                    break;
                case "Лермонтово":
                    b = 2;
                    break;
                case "Толстого":
                    b = 3;
                    break;
            }


            MySqlConnection conn = new(stringconnection);
            conn.Open();
            string query = "INSERT INTO Task (Name,Adress,PaymentStatus,Accepted,ReadyForDelivery) VALUES (@name,@adress,@paystatus,@accepted,@readyfordelivery)";
            MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@name", NameTask);
            cmd.Parameters.AddWithValue("@adress",b);
            cmd.Parameters.AddWithValue("@paystatus", 1);
            cmd.Parameters.AddWithValue("@accepted", 1);
            cmd.Parameters.AddWithValue("@readyfordelivery", 1);
            cmd.ExecuteNonQuery();
            TestLabel.Content = "Заказ успешно создан";
            TestLabel.Foreground = Brushes.Green;
        }
        catch (Exception ex)
        {
            TestLabel.Content = ex.Message;    
            
        }

        // var item = (ComboBoxItem)Adress;
        //  TestLabel.Content = ComboBox.SelectionBoxItem + NameTextBox.Text;
    }


}