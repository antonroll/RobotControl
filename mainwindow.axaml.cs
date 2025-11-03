using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using InventorySystem;   // din Robot / ItemSorterRobot

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {
        // disse to er dem XAML'en binder til
        public ObservableCollection<OrderDisplay> Orders { get; } = new();
        public ObservableCollection<OrderDisplay> ProcessedOrders { get; } = new();

        private readonly ItemSorterRobot _robot;

        public MainWindow()
        {
            InitializeComponent();

            _robot = new ItemSorterRobot();

            // lav lige noget data så dine DataGrids ikke er tomme
            Orders.Add(new OrderDisplay
            {
                Time = DateTime.Now.AddDays(-2).ToString("g"),
                LinesSummary = "M3 screw x1, M3 nut x2, pen x1",
                TotalPrice = "DKK 50"
            });

            Orders.Add(new OrderDisplay
            {
                Time = DateTime.Now.ToString("g"),
                LinesSummary = "M3 nut x2",
                TotalPrice = "DKK 20"
            });
        }

        private async void OnProcessOrderClick(object? sender, RoutedEventArgs e)
        {
            if (StatusMessages == null)
                return;

            StatusMessages.Text += "Processing next order...\n";

            // her laver vi bare noget simpelt robot-kald for at teste
            _robot.PickUp(1);
            await Task.Delay(9500);

            StatusMessages.Text += "Done.\n";

            // flyt evt. første ordre til 'Processed'
            if (Orders.Count > 0)
            {
                ProcessedOrders.Add(Orders[0]);
                Orders.RemoveAt(0);
            }
        }
    }

    // lille helper-klasse kun til at vise i DataGrid
    public class OrderDisplay
    {
        public string Time { get; set; } = "";
        public string LinesSummary { get; set; } = "";
        public string TotalPrice { get; set; } = "";
    }
}
