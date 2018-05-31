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
using System.Windows.Media.Animation;
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

		static int RowsCount = 7;

		private void Window_Loaded(object sender,RoutedEventArgs e)
		{
			for(int i = 0;i < RowsCount;i++)
			{
				RowDefinition rd1 = new RowDefinition();
				rd1.Height = new GridLength(1,GridUnitType.Star);
				RowDefinition rd2 = new RowDefinition();
				rd2.Height = new GridLength(1,GridUnitType.Star);

				ColumnDefinition cd1 = new ColumnDefinition();
				cd1.Width = new GridLength(1,GridUnitType.Star);
				ColumnDefinition cd2 = new ColumnDefinition();
				cd2.Width = new GridLength(1,GridUnitType.Star);

				bordergrid.RowDefinitions.Add(rd1);
				textblockgrid.RowDefinitions.Add(rd2);
				bordergrid.ColumnDefinitions.Add(cd1);
				textblockgrid.ColumnDefinitions.Add(cd2);
			}

			NewBorder();
			
		}
		SolidColorBrush redSB = new SolidColorBrush(Colors.Red);
		private void NewBorder()
		{
			for(int i = 0;i < bordergrid.RowDefinitions.Count;i++)
			{
				for(int j = 0;j < bordergrid.ColumnDefinitions.Count;j++)
				{
					Border border = new Border();
					border.BorderThickness = new Thickness(1);
					border.VerticalAlignment = VerticalAlignment.Stretch;
					border.HorizontalAlignment = HorizontalAlignment.Stretch;
					TextBlock textblock = new TextBlock();
					textblock.FontWeight = FontWeights.Heavy;
					textblock.Foreground = Brushes.White;
					textblock.Text = (i * RowsCount + j).ToString();
					textblock.VerticalAlignment = VerticalAlignment.Center;
					textblock.HorizontalAlignment = HorizontalAlignment.Center;
					border.Child = textblock;
					//border没有background无法拉伸
					border.Background = textblockgrid.Background;
					border.Margin = new Thickness(3);

					CustomBorder cborder = new CustomBorder();
					cborder.BorderThickness = new Thickness(3);
					cborder.Margin = new Thickness(3);
					cborder.VerticalAlignment = VerticalAlignment.Stretch;
					cborder.HorizontalAlignment = HorizontalAlignment.Stretch;
					cborder.Background = bordergrid.Background;
					border.MouseEnter += border_MouseEnter;
					border.MouseLeave += border_MouseLeave;
					//border.MouseLeftButtonDown += Cborder_MouseLeftButtonDown;
					bordergrid.Children.Add(cborder);
					Grid.SetRow(cborder,i);
					Grid.SetColumn(cborder,j);
					textblockgrid.Children.Add(border);
					Grid.SetRow(border,i);
					Grid.SetColumn(border,j);
				}
			}
		}
		LinearGradientBrush leftLgb = new LinearGradientBrush(Colors.Gray,Colors.Transparent,new Point(1,0),new Point(0,0));
		LinearGradientBrush rightLgb = new LinearGradientBrush(Colors.Gray,Colors.Transparent,new Point(0,0),new Point(1,0));
		LinearGradientBrush topLgb = new LinearGradientBrush(Colors.Gray,Colors.Transparent,new Point(0,1),new Point(0,0));
		LinearGradientBrush bottomLgb = new LinearGradientBrush(Colors.Gray,Colors.Transparent,new Point(0,0),new Point(0,1));

		private void Cborder_MouseLeftButtonDown(object sender,MouseButtonEventArgs e)
		{
			Border border = sender as Border;
			int gridNum = textblockgrid.Children.IndexOf(border);
			CustomBorder cborder = bordergrid.Children[gridNum] as CustomBorder;
			if(border.IsMouseOver)
			{
				cborder.Background = Brushes.Gray;
				cborder.BorderBrush = Brushes.LightGray;
			}
			//CustomBorder cborder = sender as CustomBorder;
			//if(cborder.IsMouseOver)
			//{
			//	cborder.Background = Brushes.Gray;
			//	cborder.BorderBrush = Brushes.LightGray;
			//}
		}

		private void border_MouseLeave(object sender,MouseEventArgs e)
		{
			Border border = sender as Border;
			int gridNum = textblockgrid.Children.IndexOf(border);
			CustomBorder cborder = bordergrid.Children[gridNum] as CustomBorder;
			if(!border.IsMouseOver)
			{
				cborder.Background = bordergrid.Background;
				cborder.BorderBrush = Brushes.Transparent;
				ClearAroundCborder(gridNum);
			}
		}

		private void ClearAroundCborder(int bordergridNum)
		{
			DoubleAnimation daV = new DoubleAnimation(0.5,0,new Duration(TimeSpan.FromSeconds(1)));
			#region 九个情况
			//左上
			if(bordergridNum == 0)
			{
				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				//Right.LeftBorderBrush = null;
				//Right.TopBorderBrush = null;
				//Right.BottomBorderBrush = null;
				//Right.BorderBrush = null;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//Bottom.LeftBorderBrush = null;
				//Bottom.RightBorderBrush = null;
				//Bottom.TopBorderBrush = null;
				//Bottom.BorderBrush = null;

				CustomBorder rightBottom = bordergrid.Children[bordergridNum + RowsCount + 1] as CustomBorder;
				rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//rightBottom.LeftBorderBrush = null;
				//rightBottom.TopBorderBrush = null;
				//rightBottom.BorderBrush = null;
			}
			//右上
			else if(bordergridNum == RowsCount - 1)
			{
				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				//Left.TopBorderBrush = null;
				//Left.RightBorderBrush = null;
				//Left.BottomBorderBrush = null;
				//Left.BorderBrush = null;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//Bottom.LeftBorderBrush = null;
				//Bottom.RightBorderBrush = null;
				//Bottom.TopBorderBrush = null;
				//Bottom.BorderBrush = null;

				CustomBorder leftBottom = bordergrid.Children[bordergridNum + RowsCount - 1] as CustomBorder;
				leftBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//leftBottom.RightBorderBrush = null;
				//leftBottom.TopBorderBrush = null;
				//leftBottom.BorderBrush = null;
			}
			//左下
			else if(bordergridNum == RowsCount * (RowsCount - 1))
			{
				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				//Top.LeftBorderBrush = null;
				//Top.RightBorderBrush = null;
				//Top.BottomBorderBrush = null;
				//Top.BorderBrush = null;

				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				//Right.LeftBorderBrush = null;
				//Right.TopBorderBrush = null;
				//Right.BottomBorderBrush = null;
				//Right.BorderBrush = null;

				CustomBorder rightTop = bordergrid.Children[bordergridNum - RowsCount + 1] as CustomBorder;
				rightTop.BeginAnimation(UIElement.OpacityProperty,daV);
				//rightTop.LeftBorderBrush = null;
				//rightTop.BottomBorderBrush = null;
				//rightTop.BorderBrush = null;
			}
			//右下
			else if(bordergridNum == RowsCount * RowsCount - 1)
			{
				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				//Left.TopBorderBrush = null;
				//Left.RightBorderBrush = null;
				//Left.BottomBorderBrush = null;
				//Left.BorderBrush = null;

				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				//Top.LeftBorderBrush = null;
				//Top.RightBorderBrush = null;
				//Top.BottomBorderBrush = null;
				//Top.BorderBrush = null;

				CustomBorder leftTop = bordergrid.Children[bordergridNum - RowsCount - 1] as CustomBorder;
				leftTop.BeginAnimation(UIElement.OpacityProperty,daV);
				//leftTop.RightBorderBrush = null;
				//leftTop.BottomBorderBrush = null;
				//leftTop.BorderBrush = null;
			}
			//上
			else if(bordergridNum > 0 && bordergridNum < RowsCount - 1)
			{
				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				//Right.LeftBorderBrush = null;
				//Right.TopBorderBrush = null;
				//Right.BottomBorderBrush = null;
				//Right.BorderBrush = null;

				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				//Left.TopBorderBrush = null;
				//Left.RightBorderBrush = null;
				//Left.BottomBorderBrush = null;
				//Left.BorderBrush = null;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//Bottom.LeftBorderBrush = null;
				//Bottom.RightBorderBrush = null;
				//Bottom.TopBorderBrush = null;
				//Bottom.BorderBrush = null;

				CustomBorder leftBottom = bordergrid.Children[bordergridNum + RowsCount - 1] as CustomBorder;
				leftBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//leftBottom.RightBorderBrush = null;
				//leftBottom.TopBorderBrush = null;
				//leftBottom.BorderBrush = null;

				CustomBorder rightBottom = bordergrid.Children[bordergridNum + RowsCount + 1] as CustomBorder;
				rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//rightBottom.LeftBorderBrush = null;
				//rightBottom.TopBorderBrush = null;
				//rightBottom.BorderBrush = null;
			}
			//下
			else if(bordergridNum > RowsCount * (RowsCount - 1) && bordergridNum < RowsCount * RowsCount - 1)
			{
				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				//Left.TopBorderBrush = null;
				//Left.RightBorderBrush = null;
				//Left.BottomBorderBrush = null;
				//Left.BorderBrush = null;

				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				//Top.LeftBorderBrush = null;
				//Top.RightBorderBrush = null;
				//Top.BottomBorderBrush = null;
				//Top.BorderBrush = null;

				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				//Right.LeftBorderBrush = null;
				//Right.TopBorderBrush = null;
				//Right.BottomBorderBrush = null;
				//Right.BorderBrush = null;

				CustomBorder rightTop = bordergrid.Children[bordergridNum - RowsCount + 1] as CustomBorder;
				rightTop.BeginAnimation(UIElement.OpacityProperty,daV);
				//rightTop.LeftBorderBrush = null;
				//rightTop.BottomBorderBrush = null;
				//rightTop.BorderBrush = null;

				CustomBorder leftTop = bordergrid.Children[bordergridNum - RowsCount - 1] as CustomBorder;
				leftTop.BeginAnimation(UIElement.OpacityProperty,daV);
				//leftTop.RightBorderBrush = null;
				//leftTop.BottomBorderBrush = null;
				//leftTop.BorderBrush = null;
			}
			//左
			else if(bordergridNum % RowsCount == 0)
			{
				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				//Top.LeftBorderBrush = null;
				//Top.RightBorderBrush = null;
				//Top.BottomBorderBrush = null;
				//Top.BorderBrush = null;

				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				//Right.LeftBorderBrush = null;
				//Right.TopBorderBrush = null;
				//Right.BottomBorderBrush = null;
				//Right.BorderBrush = null;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//Bottom.LeftBorderBrush = null;
				//Bottom.RightBorderBrush = null;
				//Bottom.TopBorderBrush = null;
				//Bottom.BorderBrush = null;

				CustomBorder rightBottom = bordergrid.Children[bordergridNum + RowsCount + 1] as CustomBorder;
				rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//rightBottom.LeftBorderBrush = null;
				//rightBottom.TopBorderBrush = null;
				//rightBottom.BorderBrush = null;

				CustomBorder rightTop = bordergrid.Children[bordergridNum - RowsCount + 1] as CustomBorder;
				rightTop.BeginAnimation(UIElement.OpacityProperty,daV);
				//rightTop.LeftBorderBrush = null;
				//rightTop.BottomBorderBrush = null;
				//rightTop.BorderBrush = null;
			}
			//右
			else if((bordergridNum + 1) % RowsCount == 0)
			{
				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				//Left.TopBorderBrush = null;
				//Left.RightBorderBrush = null;
				//Left.BottomBorderBrush = null;
				//Left.BorderBrush = null;

				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				//Top.LeftBorderBrush = null;
				//Top.RightBorderBrush = null;
				//Top.BottomBorderBrush = null;
				//Top.BorderBrush = null;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//Bottom.LeftBorderBrush = null;
				//Bottom.RightBorderBrush = null;
				//Bottom.TopBorderBrush = null;
				//Bottom.BorderBrush = null;

				CustomBorder leftTop = bordergrid.Children[bordergridNum - RowsCount - 1] as CustomBorder;
				leftTop.BeginAnimation(UIElement.OpacityProperty,daV);
				//leftTop.RightBorderBrush = null;
				//leftTop.BottomBorderBrush = null;
				//leftTop.BorderBrush = null;

				CustomBorder leftBottom = bordergrid.Children[bordergridNum + RowsCount - 1] as CustomBorder;
				leftBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//leftBottom.RightBorderBrush = null;
				//leftBottom.TopBorderBrush = null;
				//leftBottom.BorderBrush = null;
			}
			//剩下
			else
			{
				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				//Top.LeftBorderBrush = null;
				//Top.RightBorderBrush = null;
				//Top.BottomBorderBrush = null;
				//Top.BorderBrush = null;

				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				//Left.TopBorderBrush = null;
				//Left.RightBorderBrush = null;
				//Left.BottomBorderBrush = null;
				//Left.BorderBrush = null;

				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				//Right.LeftBorderBrush = null;
				//Right.TopBorderBrush = null;
				//Right.BottomBorderBrush = null;
				//Right.BorderBrush = null;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//Bottom.LeftBorderBrush = null;
				//Bottom.RightBorderBrush = null;
				//Bottom.TopBorderBrush = null;
				//Bottom.BorderBrush = null;

				CustomBorder leftTop = bordergrid.Children[bordergridNum - RowsCount - 1] as CustomBorder;
				leftTop.BeginAnimation(UIElement.OpacityProperty,daV);
				//leftTop.RightBorderBrush = null;
				//leftTop.BottomBorderBrush = null;
				//leftTop.BorderBrush = null;

				CustomBorder leftBottom = bordergrid.Children[bordergridNum + RowsCount - 1] as CustomBorder;
				leftBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//leftBottom.RightBorderBrush = null;
				//leftBottom.TopBorderBrush = null;
				//leftBottom.BorderBrush = null;

				CustomBorder rightBottom = bordergrid.Children[bordergridNum + RowsCount + 1] as CustomBorder;
				rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				//rightBottom.LeftBorderBrush = null;
				//rightBottom.TopBorderBrush = null;
				//rightBottom.BorderBrush = null;

				CustomBorder rightTop = bordergrid.Children[bordergridNum - RowsCount + 1] as CustomBorder;
				rightTop.BeginAnimation(UIElement.OpacityProperty,daV);
				//rightTop.LeftBorderBrush = null;
				//rightTop.BottomBorderBrush = null;
				//rightTop.BorderBrush = null;
			}
			#endregion
		}

		private void border_MouseEnter(object sender,MouseEventArgs e)
		{
			Border border = sender as Border;
			int gridNum = textblockgrid.Children.IndexOf(border);
			CustomBorder cborder = bordergrid.Children[gridNum] as CustomBorder;
			if(border.IsMouseOver)
			{
				cborder.Background = Brushes.Gray;
				cborder.BorderBrush = Brushes.LightGray;
				ChanegeAroundCborder(gridNum);
				DoubleAnimation daV = new DoubleAnimation(0.5,1,new Duration(TimeSpan.FromSeconds(1)));
				cborder.BeginAnimation(UIElement.OpacityProperty,daV);
			}
		}



		private void ChanegeAroundCborder(int bordergridNum)
		{
			DoubleAnimation daV = new DoubleAnimation(0.5,0.5,new Duration(TimeSpan.FromSeconds(1)));

			#region 九个情况
			//左上
			if(bordergridNum == 0)
			{
				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = Brushes.Gray;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = rightLgb;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightBottom = bordergrid.Children[bordergridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = bottomLgb;
				rightBottom.TopBorderBrush = rightLgb;
				rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				rightBottom.BorderBrush = Brushes.Transparent;
			}
			//右上
			else if(bordergridNum == RowsCount - 1)
			{
				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder leftBottom = bordergrid.Children[bordergridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = bottomLgb;
				leftBottom.TopBorderBrush = leftLgb;
				leftBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				leftBottom.BorderBrush = Brushes.Transparent;
			}
			//左下
			else if(bordergridNum == RowsCount * (RowsCount - 1))
			{
				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder rightTop = bordergrid.Children[bordergridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = topLgb;
				rightTop.BottomBorderBrush = rightLgb;
				rightTop.BeginAnimation(UIElement.OpacityProperty,daV);
				rightTop.BorderBrush = Brushes.Transparent;
			}
			//右下
			else if(bordergridNum == RowsCount * RowsCount - 1)
			{
				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder leftTop = bordergrid.Children[bordergridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = topLgb;
				leftTop.BottomBorderBrush = leftLgb;
				leftTop.BeginAnimation(UIElement.OpacityProperty,daV);
				leftTop.BorderBrush = Brushes.Transparent;
			}
			//上
			else if(bordergridNum > 0 && bordergridNum < RowsCount - 1)
			{
				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder leftBottom = bordergrid.Children[bordergridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = bottomLgb;
				leftBottom.TopBorderBrush = leftLgb;
				leftBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				leftBottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightBottom = bordergrid.Children[bordergridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = bottomLgb;
				rightBottom.TopBorderBrush = rightLgb;
				rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				rightBottom.BorderBrush = Brushes.Transparent;
			}
			//下
			else if(bordergridNum > RowsCount * (RowsCount - 1) && bordergridNum < RowsCount * RowsCount - 1)
			{
				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder rightTop = bordergrid.Children[bordergridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = topLgb;
				rightTop.BottomBorderBrush = rightLgb;
				rightTop.BeginAnimation(UIElement.OpacityProperty,daV);
				rightTop.BorderBrush = Brushes.Transparent;

				CustomBorder leftTop = bordergrid.Children[bordergridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = topLgb;
				leftTop.BottomBorderBrush = leftLgb;
				leftTop.BeginAnimation(UIElement.OpacityProperty,daV);
				leftTop.BorderBrush = Brushes.Transparent;
			}
			//左
			else if(bordergridNum % RowsCount == 0)
			{
				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightBottom = bordergrid.Children[bordergridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = bottomLgb;
				rightBottom.TopBorderBrush = rightLgb;
				rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				rightBottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightTop = bordergrid.Children[bordergridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = topLgb;
				rightTop.BottomBorderBrush = rightLgb;
				rightTop.BeginAnimation(UIElement.OpacityProperty,daV);
				rightTop.BorderBrush = Brushes.Transparent;
			}
			//右
			else if((bordergridNum + 1) % RowsCount == 0)
			{
				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder leftTop = bordergrid.Children[bordergridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = topLgb;
				leftTop.BottomBorderBrush = leftLgb;
				leftTop.BeginAnimation(UIElement.OpacityProperty,daV);
				leftTop.BorderBrush = Brushes.Transparent;

				CustomBorder leftBottom = bordergrid.Children[bordergridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = bottomLgb;
				leftBottom.TopBorderBrush = leftLgb;
				leftBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				leftBottom.BorderBrush = Brushes.Transparent;
			}
			//剩下
			else
			{
				CustomBorder Top = bordergrid.Children[bordergridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.BeginAnimation(UIElement.OpacityProperty,daV);
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Left = bordergrid.Children[bordergridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.BeginAnimation(UIElement.OpacityProperty,daV);
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Right = bordergrid.Children[bordergridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = bordergrid.Children[bordergridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder leftTop = bordergrid.Children[bordergridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = topLgb;
				leftTop.BottomBorderBrush = leftLgb;
				leftTop.BeginAnimation(UIElement.OpacityProperty,daV);
				leftTop.BorderBrush = Brushes.Transparent;

				CustomBorder leftBottom = bordergrid.Children[bordergridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = bottomLgb;
				leftBottom.TopBorderBrush = leftLgb;
				leftBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				leftBottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightBottom = bordergrid.Children[bordergridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = bottomLgb;
				rightBottom.TopBorderBrush = rightLgb;
				rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				rightBottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightTop = bordergrid.Children[bordergridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = topLgb;
				rightTop.BottomBorderBrush = rightLgb;
				rightTop.BeginAnimation(UIElement.OpacityProperty,daV);
				rightTop.BorderBrush = Brushes.Transparent;
			}
			#endregion
		}
	}
}
