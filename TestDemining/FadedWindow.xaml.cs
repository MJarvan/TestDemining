using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestDemining
{
	/// <summary>
	/// Window1.xaml 的交互逻辑
	/// </summary>
	public partial class FadedWindow:Window
	{
		public FadedWindow()
		{
			InitializeComponent();
		}
		LinearGradientBrush linebrush = new LinearGradientBrush();
		LinearGradientBrush Linebrush
		{
			get
			{
				linebrush.StartPoint = new Point(0,0);
				linebrush.EndPoint = new Point(1,0);
				linebrush.GradientStops.Add(new GradientStop(Colors.Transparent,0));
				linebrush.GradientStops.Add(new GradientStop(Colors.LightGray,1));
				return linebrush;
			}
		}

		static int RowsCount = 7;

		private void Window_Loaded(object sender,RoutedEventArgs e)
		{
			for(int i = 0;i < RowsCount;i++)
			{
				RowDefinition rd = new RowDefinition();
				rd.Height = new GridLength(1,GridUnitType.Star);

				ColumnDefinition cd = new ColumnDefinition();
				cd.Width = new GridLength(1,GridUnitType.Star);
				grid.RowDefinitions.Add(rd);
				grid.ColumnDefinitions.Add(cd);
			}

			NewBorder();

		}

		private void NewBorder()
		{
			for(int i = 0;i < grid.RowDefinitions.Count;i++)
			{
				for(int j = 0;j < grid.ColumnDefinitions.Count;j++)
				{
					CustomBorder cborder = new CustomBorder();
					cborder.BorderThickness = new Thickness(5);
					TextBlock tb1 = new TextBlock();
					tb1.FontWeight = FontWeights.Heavy;
					tb1.Foreground = Brushes.White;
					tb1.Text = (i * RowsCount + j).ToString();
					tb1.VerticalAlignment = VerticalAlignment.Center;
					tb1.HorizontalAlignment = HorizontalAlignment.Center;

					cborder.Child = tb1;
					cborder.VerticalAlignment = VerticalAlignment.Stretch;
					cborder.HorizontalAlignment = HorizontalAlignment.Stretch;
					cborder.Background = grid.Background;
					cborder.MouseEnter += Cborder_MouseEnter;
					cborder.MouseLeave += Cborder_MouseLeave;
					//cborder.LeftBorderBrush = blackSB;
					//cborder.RightBorderBrush = redSB;
					//cborder.TopBorderBrush = blackSB;
					//cborder.BottomBorderBrush = blackSB;
					grid.Children.Add(cborder);
					Grid.SetRow(cborder,i);
					Grid.SetColumn(cborder,j);
				}
			}
		}

		private void Cborder_MouseLeave(object sender,MouseEventArgs e)
		{
			CustomBorder cborder = sender as CustomBorder;
			if(!cborder.IsMouseOver)
			{
				cborder.BorderBrush = Brushes.Transparent;
			}
		}

		private void Cborder_MouseEnter(object sender,MouseEventArgs e)
		{
			CustomBorder cborder = sender as CustomBorder;
			int gridNum = grid.Children.IndexOf(cborder);
			if(cborder.IsMouseOver)
			{
				cborder.BorderBrush = Brushes.Gray;
			}

			GetAroundCborder(gridNum);
			
		}

		private void GetAroundCborder(int gridNum)
		{
			#region step1
			try
			{
				CustomBorder leftTop = grid.Children[gridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = Linebrush;
				leftTop.BottomBorderBrush = Linebrush;
			}
			catch
			{
			}
			try
			{
				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = Linebrush;
				Top.RightBorderBrush = Linebrush;
				Top.BottomBorderBrush = Linebrush;
			}
			catch
			{
			}
			try
			{
				CustomBorder rightTop = grid.Children[gridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = Linebrush;
				rightTop.BottomBorderBrush = Linebrush;
			}
			catch
			{
			}
			try
			{
				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = Linebrush;
				Left.RightBorderBrush = Linebrush;
				Left.BottomBorderBrush = Linebrush;
			}
			catch
			{
			}
			try
			{
				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = Linebrush;
				Right.TopBorderBrush = Linebrush;
				Right.BottomBorderBrush = Linebrush;
			}
			catch
			{
			}
			try
			{
				CustomBorder leftBottom = grid.Children[gridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = Linebrush;
				leftBottom.TopBorderBrush = Linebrush;
			}
			catch
			{
			}
			try
			{
				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = Linebrush;
				Bottom.RightBorderBrush = Linebrush;
				Bottom.TopBorderBrush = Linebrush;
			}
			catch
			{
			}
			try
			{
				CustomBorder rightBottom = grid.Children[gridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = Linebrush;
				rightBottom.TopBorderBrush = Linebrush;
			}
			catch
			{
			}
			#endregion

		}
	}
}
