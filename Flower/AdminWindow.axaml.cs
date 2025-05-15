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


}