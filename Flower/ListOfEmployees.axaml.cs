using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Flower;

public partial class List_of_employees : Window
{
    public List_of_employees()
    {
        InitializeComponent();
    }


    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        AdminWindow adminWindow = new AdminWindow();
        Hide();
        adminWindow.Show();
        this.Close();
    }

}