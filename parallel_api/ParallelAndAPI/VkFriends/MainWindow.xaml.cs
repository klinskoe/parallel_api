using System.Windows;

namespace VkFriends
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VkService _service = new VkService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void getFriendsButton_Click(object sender, RoutedEventArgs e)
        {
            string screenName = screenNameTextBox.Text;

            Title = "Loading...";
            friendsListBox.ItemsSource = await _service.GetFriends(screenName, count: 50);
            Title = "VK Friends";
        }
    }
}