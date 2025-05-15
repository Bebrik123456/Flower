using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

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
}