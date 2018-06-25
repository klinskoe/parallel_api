using System;
using System.Diagnostics;
using System.Windows;

namespace GoogleSearch
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		GoogleService _service = new GoogleService();

		public MainWindow()
		{
			InitializeComponent();
		}

		private async void buttonSearch_Click(object sender, RoutedEventArgs e)
		{
			try
			{
                var results = await _service.GetResults(textBoxQuery.Text);

                listBoxResults.ItemsSource = results;
			}
			catch(Exception ex)
			{
				// If the program ends up here check that you
				// have api key and engine id assigned
				MessageBox.Show("Error occured", "Google Search");
			}
		}

        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
		{
			var item = listBoxResults.SelectedItem as SearchResult;
			Process.Start(item.Url);
		}
	}
}