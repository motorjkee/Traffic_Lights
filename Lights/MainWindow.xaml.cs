using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lights
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//Объявляем таймер и цвета
		DispatcherTimer timer = new DispatcherTimer();
		SolidColorBrush redColor = new SolidColorBrush(Colors.Red);
		SolidColorBrush yellowColor = new SolidColorBrush(Colors.Yellow);
		SolidColorBrush greenColor = new SolidColorBrush(Colors.Green);
		SolidColorBrush whiteColor = new SolidColorBrush(Colors.White);
		int sec = 1, redTime, yellowTime, greenTime, fullTime;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void startButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//читаем время из текст боксов и запускаем обработчик события с заданным интервалом
				redTime = int.Parse(redValue.Text);
				yellowTime = int.Parse(yellowValue.Text);
				greenTime = int.Parse(greenValue.Text);
				fullTime = redTime + yellowTime + greenTime;
				timer.Interval = new TimeSpan(0, 0, 1);
				timer.Tick += timer_Tick;
				timer.Start();
				startButton.IsEnabled = false;
				pauseButton.IsEnabled = true;
				stopButton.IsEnabled = true;
			}
			catch (FormatException)
			{
				MessageBox.Show("Поля не должны быть пустыми");
			}

		}
		//в обработчике описывается алгоритм включения сигналов 
		void timer_Tick(object sender, EventArgs e)
		{			
			tm.Content = sec;

			if (sec >= 0 && sec < redTime)
			{
				redCircle.Fill = redColor;
				yellowCircle.Fill = whiteColor;
				greenCircle.Fill = whiteColor;
			}
			if (sec >= redTime - yellowTime + 1 && sec <= redTime)
			{
				yellowCircle.Fill = new SolidColorBrush(Colors.Yellow);
			}
			if (sec > redTime && sec <= fullTime-yellowTime)
			{
				redCircle.Fill = whiteColor;
				yellowCircle.Fill = whiteColor;
				greenCircle.Fill = greenColor;
				if (sec >= fullTime-yellowTime-3 && sec%2 == 1)
					greenCircle.Fill = whiteColor;
				else
					greenCircle.Fill = greenColor;
			}
			if (sec > fullTime - yellowTime && sec <= fullTime)
			{
				yellowCircle.Fill = yellowColor;
				greenCircle.Fill = whiteColor;
			}
			if (sec >= fullTime)
			{
				sec = 0;
			}
			sec++;
		}

		private void pauseButton_Click(object sender, RoutedEventArgs e)
		{
			if (timer.IsEnabled) {
				timer.IsEnabled = !timer.IsEnabled;
				pauseButton.Content = "Возобновить";
			}
			else
			{
				timer.IsEnabled = !timer.IsEnabled;
				pauseButton.Content = "Пауза";
			}
		}

		private void stopButton_Click(object sender, RoutedEventArgs e)
		{
			sec = 1;
			tm.Content = sec;
			timer.Tick -= timer_Tick;
			redCircle.Fill = whiteColor;
			yellowCircle.Fill = whiteColor;
			greenCircle.Fill = whiteColor;
			startButton.IsEnabled = true;
			pauseButton.Content = "Пауза";

		}


	}
}
