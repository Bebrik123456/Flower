using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Flower;

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
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


}