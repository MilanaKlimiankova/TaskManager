using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace TaskManager
{
	public partial class MainWindow : Window
	{
		public List<string> blackList { get; set; } = new List<string>();

		public MainWindow()
		{
			InitializeComponent();
			fillListView();
		}

		private void fillListView()
		{
			tasks_listview.Items.Clear();

            foreach (var item in Process.GetProcesses())
			{
				tasks_listview.Items.Add(item.ProcessName);
			}
		}

		private void main_txtbox_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox txtbox = (TextBox)sender;
			txtbox.Text = string.Empty;
			txtbox.Foreground = Brushes.Black;
			txtbox.GotFocus -= main_txtbox_GotFocus;
		}

		private void createTask_btn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
                Process.Start(main_txtbox.Text);
                fillListView();
            }
			catch (Exception ex)
			{
                MessageBox.Show($"Не удалось запустить процесс.");
            }
            
        }


		private void endTask_btn_Click(object sender, RoutedEventArgs e)
		{
			string processName = tasks_listview.SelectedItem.ToString();

			foreach (var item in Process.GetProcesses())
			{
				if (item.ProcessName == processName)
				{
					item.Kill();
				}
			}

            fillListView();
        }


		private void tasks_listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (tasks_listview.SelectedItem != null)
			{
                string processName = tasks_listview.SelectedItem.ToString();
                string processId = "";

                foreach (var item in Process.GetProcesses())
                {
                    if (item.ProcessName == processName)
                    {
                        processId = item.Id.ToString();
                    }
                }

                MessageBox.Show($"Process Name = {processName}\nProcess Id = {processId}");
            }		
		}

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Чтобы завершить процесс, нужно выбрать процесс из списка и нажать красную кнопку «Завершить». Выбранный процесс будет прерван. \n\nЧтобы запустить проесс, нужно ввести название процесса в текстовое поле и нажать зелёную кнопку «Запустить». Будет запущен выбранный процесс.\n\n");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Название: TaskManager\n\nАвтор: Клименкова М.А.\nПреподаватель: Бумай А.Ю.");

        }
    }
}
