using System.Threading.Tasks;
using System.Windows;

namespace ParallelExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ParallelClick(object sender, RoutedEventArgs e)
        {
            numbersListBox.Items.Clear();

            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Dispatcher.Invoke(() => numbersListBox.Items.Add(i + 1));
                    Task.Delay(500).Wait();
                }
            });

            // async-await
            numbersListBox.Items.Add(await GetResult());
        }

        private Task<string> GetResult()
        {
            return Task.Run(() =>
            {
                Task.Delay(3000).Wait();
                return "result";
            });
        }
    }
}