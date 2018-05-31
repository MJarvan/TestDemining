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
				RowDefinition rd = new RowDefinition();
				rd.Height = new GridLength(1,GridUnitType.Star);

				ColumnDefinition cd = new ColumnDefinition();
				cd.Width = new GridLength(1,GridUnitType.Star);
				grid.RowDefinitions.Add(rd);
				grid.ColumnDefinitions.Add(cd);
			}

			NewBorder();
			
		}
		SolidColorBrush redSB = new SolidColorBrush(Colors.Red);
		private void NewBorder()
		{
			for(int i = 0;i < grid.RowDefinitions.Count;i++)
			{
				for(int j = 0;j < grid.ColumnDefinitions.Count;j++)
				{
					CustomBorder cborder = new CustomBorder();
					cborder.BorderThickness = new Thickness(3);
					TextBlock tb1 = new TextBlock();
					tb1.FontWeight = FontWeights.Heavy;
					tb1.Foreground = Brushes.White;
					tb1.Text = (i * RowsCount + j).ToString();
					tb1.VerticalAlignment = VerticalAlignment.Center;
					tb1.HorizontalAlignment = HorizontalAlignment.Center;
					cborder.Margin = new Thickness(3);
					cborder.Child = tb1;
					cborder.VerticalAlignment = VerticalAlignment.Stretch;
					cborder.HorizontalAlignment = HorizontalAlignment.Stretch;
					cborder.Background = grid.Background;
					cborder.MouseEnter += Cborder_MouseEnter;
					cborder.MouseLeave += Cborder_MouseLeave;
					//cborder.MouseLeftButtonDown += Cborder_MouseLeftButtonDown;
					//cborder.LeftBorderBrush = blackSB;
					//cborder.RightBorderBrush = redSB;
					//cborder.TopBorderBrush = blackSB;
					//cborder.BottomBorderBrush = blackSB;
					grid.Children.Add(cborder);
					Grid.SetRow(cborder,i);
					Grid.SetColumn(cborder,j);
					Grid.SetZIndex(cborder,0);
				}
			}
		}
		LinearGradientBrush leftLgb = new LinearGradientBrush(Colors.Gray,Colors.Transparent,new Point(1,0),new Point(0,0));
		LinearGradientBrush rightLgb = new LinearGradientBrush(Colors.Gray,Colors.Transparent,new Point(0,0),new Point(1,0));
		LinearGradientBrush topLgb = new LinearGradientBrush(Colors.Gray,Colors.Transparent,new Point(0,1),new Point(0,0));
		LinearGradientBrush bottomLgb = new LinearGradientBrush(Colors.Gray,Colors.Transparent,new Point(0,0),new Point(0,1));

		private void Cborder_MouseLeftButtonDown(object sender,MouseButtonEventArgs e)
		{
			CustomBorder cborder = sender as CustomBorder;
			int gridNum = grid.Children.IndexOf(cborder);
			if(cborder.IsMouseOver)
			{
				cborder.Background = Brushes.Gray;
				cborder.BorderBrush = Brushes.LightGray;
				ChanegeAroundCborder(gridNum);
			}
		}

		private void Cborder_MouseLeave(object sender,MouseEventArgs e)
		{
			CustomBorder cborder = sender as CustomBorder;
			int gridNum = grid.Children.IndexOf(cborder);
			if(!cborder.IsMouseOver)
			{
				cborder.Background = grid.Background;
				cborder.BorderBrush = Brushes.Transparent;
				ClearAroundCborder(gridNum);
			}
		}

		private void ClearAroundCborder(int gridNum)
		{
			#region 九个情况
			//左上
			if(gridNum == 0)
			{
				DoubleAnimation daV = new DoubleAnimation(0.5,1,new Duration(TimeSpan.FromSeconds(1)));
				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.BeginAnimation(UIElement.OpacityProperty,daV);
				Right.LeftBorderBrush = null;
				Right.TopBorderBrush = null;
				Right.BottomBorderBrush = null;
				//Right.Opacity = 1;
				Right.BorderBrush = null;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				Bottom.LeftBorderBrush = null;
				Bottom.RightBorderBrush = null;
				Bottom.TopBorderBrush = null;
				//Bottom.Opacity = 1;
				Bottom.BorderBrush = null;

				CustomBorder rightBottom = grid.Children[gridNum + RowsCount + 1] as CustomBorder;
				rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				rightBottom.LeftBorderBrush = null;
				rightBottom.TopBorderBrush = null;
				//rightBottom.Opacity = 1;
				rightBottom.BorderBrush = null;
			}
			//右上
			else if(gridNum == RowsCount - 1)
			{
				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = null;
				Left.RightBorderBrush = null;
				Left.BottomBorderBrush = null;
				Left.Opacity = 1;
				Left.BorderBrush = null;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = null;
				Bottom.RightBorderBrush = null;
				Bottom.TopBorderBrush = null;
				Bottom.Opacity = 1;
				Bottom.BorderBrush = null;

				CustomBorder leftBottom = grid.Children[gridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = null;
				leftBottom.TopBorderBrush = null;
				leftBottom.Opacity = 1;
				leftBottom.BorderBrush = null;
			}
			//左下
			else if(gridNum == RowsCount * (RowsCount - 1))
			{
				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = null;
				Top.RightBorderBrush = null;
				Top.BottomBorderBrush = null;
				Top.Opacity = 1;
				Top.BorderBrush = null;

				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = null;
				Right.TopBorderBrush = null;
				Right.BottomBorderBrush = null;
				Right.Opacity = 1;
				Right.BorderBrush = null;

				CustomBorder rightTop = grid.Children[gridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = null;
				rightTop.BottomBorderBrush = null;
				rightTop.Opacity = 1;
				rightTop.BorderBrush = null;
			}
			//右下
			else if(gridNum == RowsCount * RowsCount - 1)
			{
				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = null;
				Left.RightBorderBrush = null;
				Left.BottomBorderBrush = null;
				Left.Opacity = 1;
				Left.BorderBrush = null;

				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = null;
				Top.RightBorderBrush = null;
				Top.BottomBorderBrush = null;
				Top.Opacity = 1;
				Top.BorderBrush = null;

				CustomBorder leftTop = grid.Children[gridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = null;
				leftTop.BottomBorderBrush = null;
				leftTop.Opacity = 1;
				leftTop.BorderBrush = null;
			}
			//上
			else if(gridNum > 0 && gridNum < RowsCount - 1)
			{
				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = null;
				Right.TopBorderBrush = null;
				Right.BottomBorderBrush = null;
				Right.Opacity = 1;
				Right.BorderBrush = null;

				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = null;
				Left.RightBorderBrush = null;
				Left.BottomBorderBrush = null;
				Left.Opacity = 1;
				Left.BorderBrush = null;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = null;
				Bottom.RightBorderBrush = null;
				Bottom.TopBorderBrush = null;
				Bottom.Opacity = 1;
				Bottom.BorderBrush = null;

				CustomBorder leftBottom = grid.Children[gridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = null;
				leftBottom.TopBorderBrush = null;
				leftBottom.Opacity = 1;
				leftBottom.BorderBrush = null;

				CustomBorder rightBottom = grid.Children[gridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = null;
				rightBottom.TopBorderBrush = null;
				rightBottom.Opacity = 1;
				rightBottom.BorderBrush = null;
			}
			//下
			else if(gridNum > RowsCount * (RowsCount - 1) && gridNum < RowsCount * RowsCount - 1)
			{
				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = null;
				Left.RightBorderBrush = null;
				Left.BottomBorderBrush = null;
				Left.Opacity = 1;
				Left.BorderBrush = null;

				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = null;
				Top.RightBorderBrush = null;
				Top.BottomBorderBrush = null;
				Top.Opacity = 1;
				Top.BorderBrush = null;

				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = null;
				Right.TopBorderBrush = null;
				Right.BottomBorderBrush = null;
				Right.Opacity = 1;
				Right.BorderBrush = null;

				CustomBorder rightTop = grid.Children[gridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = null;
				rightTop.BottomBorderBrush = null;
				rightTop.Opacity = 1;
				rightTop.BorderBrush = null;

				CustomBorder leftTop = grid.Children[gridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = null;
				leftTop.BottomBorderBrush = null;
				leftTop.Opacity = 1;
				leftTop.BorderBrush = null;
			}
			//左
			else if(gridNum % RowsCount == 0)
			{
				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = null;
				Top.RightBorderBrush = null;
				Top.BottomBorderBrush = null;
				Top.Opacity = 1;
				Top.BorderBrush = null;

				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = null;
				Right.TopBorderBrush = null;
				Right.BottomBorderBrush = null;
				Right.Opacity = 1;
				Right.BorderBrush = null;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = null;
				Bottom.RightBorderBrush = null;
				Bottom.TopBorderBrush = null;
				Bottom.Opacity = 1;
				Bottom.BorderBrush = null;

				CustomBorder rightBottom = grid.Children[gridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = null;
				rightBottom.TopBorderBrush = null;
				rightBottom.Opacity = 1;
				rightBottom.BorderBrush = null;

				CustomBorder rightTop = grid.Children[gridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = null;
				rightTop.BottomBorderBrush = null;
				rightTop.Opacity = 1;
				rightTop.BorderBrush = null;
			}
			//右
			else if((gridNum + 1) % RowsCount == 0)
			{
				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = null;
				Left.RightBorderBrush = null;
				Left.BottomBorderBrush = null;
				Left.Opacity = 1;
				Left.BorderBrush = null;

				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = null;
				Top.RightBorderBrush = null;
				Top.BottomBorderBrush = null;
				Top.Opacity = 1;
				Top.BorderBrush = null;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = null;
				Bottom.RightBorderBrush = null;
				Bottom.TopBorderBrush = null;
				Bottom.Opacity = 1;
				Bottom.BorderBrush = null;

				CustomBorder leftTop = grid.Children[gridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = null;
				leftTop.BottomBorderBrush = null;
				leftTop.Opacity = 1;
				leftTop.BorderBrush = null;

				CustomBorder leftBottom = grid.Children[gridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = null;
				leftBottom.TopBorderBrush = null;
				leftBottom.Opacity = 1;
				leftBottom.BorderBrush = null;
			}
			//剩下
			else
			{
				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = null;
				Top.RightBorderBrush = null;
				Top.BottomBorderBrush = null;
				Top.Opacity = 1;
				Top.BorderBrush = null;

				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = null;
				Left.RightBorderBrush = null;
				Left.BottomBorderBrush = null;
				Left.Opacity = 1;
				Left.BorderBrush = null;

				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = null;
				Right.TopBorderBrush = null;
				Right.BottomBorderBrush = null;
				Right.Opacity = 1;
				Right.BorderBrush = null;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = null;
				Bottom.RightBorderBrush = null;
				Bottom.TopBorderBrush = null;
				Bottom.Opacity = 1;
				Bottom.BorderBrush = null;

				CustomBorder leftTop = grid.Children[gridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = null;
				leftTop.BottomBorderBrush = null;
				leftTop.Opacity = 1;
				leftTop.BorderBrush = null;

				CustomBorder leftBottom = grid.Children[gridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = null;
				leftBottom.TopBorderBrush = null;
				leftBottom.Opacity = 1;
				leftBottom.BorderBrush = null;

				CustomBorder rightBottom = grid.Children[gridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = null;
				rightBottom.TopBorderBrush = null;
				rightBottom.Opacity = 1;
				rightBottom.BorderBrush = null;

				CustomBorder rightTop = grid.Children[gridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = null;
				rightTop.BottomBorderBrush = null;
				rightTop.Opacity = 1;
				rightTop.BorderBrush = null;
			}
			#endregion
		}

		private void Cborder_MouseEnter(object sender,MouseEventArgs e)
		{
			CustomBorder cborder = sender as CustomBorder;
			int gridNum = grid.Children.IndexOf(cborder);
			if(cborder.IsMouseOver)
			{
				cborder.Background = Brushes.Gray;
				cborder.BorderBrush = Brushes.LightGray;
				ChanegeAroundCborder(gridNum);
			}
		}



		private void ChanegeAroundCborder(int gridNum)
		{
			#region 九个情况
			//左上
			if(gridNum == 0)
			{
				//DoubleAnimation daV = new DoubleAnimation(1,0.5,new Duration(TimeSpan.FromSeconds(1)));
				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = Brushes.Gray;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = rightLgb;
				//Right.BeginAnimation(UIElement.OpacityProperty,daV);
				Right.Opacity = 0.5;
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				//Bottom.BeginAnimation(UIElement.OpacityProperty,daV);
				Bottom.Opacity = 0.5;
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightBottom = grid.Children[gridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = bottomLgb;
				rightBottom.TopBorderBrush = rightLgb;
				//rightBottom.BeginAnimation(UIElement.OpacityProperty,daV);
				rightBottom.Opacity = 0.5;
				rightBottom.BorderBrush = Brushes.Transparent;
			}
			//右上
			else if(gridNum == RowsCount - 1)
			{
				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.Opacity = 0.5;
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.Opacity = 0.5;
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder leftBottom = grid.Children[gridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = bottomLgb;
				leftBottom.TopBorderBrush = leftLgb;
				leftBottom.Opacity = 0.5;
				leftBottom.BorderBrush = Brushes.Transparent;
			}
			//左下
			else if(gridNum == RowsCount * (RowsCount - 1))
			{
				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.Opacity = 0.5;
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.Opacity = 0.5;
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder rightTop = grid.Children[gridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = topLgb;
				rightTop.BottomBorderBrush = rightLgb;
				rightTop.Opacity = 0.5;
				rightTop.BorderBrush = Brushes.Transparent;
			}
			//右下
			else if(gridNum == RowsCount * RowsCount - 1)
			{
				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.Opacity = 0.5;
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.Opacity = 0.5;
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder leftTop = grid.Children[gridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = topLgb;
				leftTop.BottomBorderBrush = leftLgb;
				leftTop.Opacity = 0.5;
				leftTop.BorderBrush = Brushes.Transparent;
			}
			//上
			else if(gridNum > 0 && gridNum < RowsCount - 1)
			{
				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.Opacity = 0.5;
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.Opacity = 0.5;
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.Opacity = 0.5;
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder leftBottom = grid.Children[gridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = bottomLgb;
				leftBottom.TopBorderBrush = leftLgb;
				leftBottom.Opacity = 0.5;
				leftBottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightBottom = grid.Children[gridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = bottomLgb;
				rightBottom.TopBorderBrush = rightLgb;
				rightBottom.Opacity = 0.5;
				rightBottom.BorderBrush = Brushes.Transparent;
			}
			//下
			else if(gridNum > RowsCount * (RowsCount - 1) && gridNum < RowsCount * RowsCount - 1)
			{
				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.Opacity = 0.5;
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.Opacity = 0.5;
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.Opacity = 0.5;
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder rightTop = grid.Children[gridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = topLgb;
				rightTop.BottomBorderBrush = rightLgb;
				rightTop.Opacity = 0.5;
				rightTop.BorderBrush = Brushes.Transparent;

				CustomBorder leftTop = grid.Children[gridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = topLgb;
				leftTop.BottomBorderBrush = leftLgb;
				leftTop.Opacity = 0.5;
				leftTop.BorderBrush = Brushes.Transparent;
			}
			//左
			else if(gridNum % RowsCount == 0)
			{
				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.Opacity = 0.5;
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.Opacity = 0.5;
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.Opacity = 0.5;
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightBottom = grid.Children[gridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = bottomLgb;
				rightBottom.TopBorderBrush = rightLgb;
				rightBottom.Opacity = 0.5;
				rightBottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightTop = grid.Children[gridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = topLgb;
				rightTop.BottomBorderBrush = rightLgb;
				rightTop.Opacity = 0.5;
				rightTop.BorderBrush = Brushes.Transparent;
			}
			//右
			else if((gridNum + 1) % RowsCount == 0)
			{
				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.Opacity = 0.5;
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.Opacity = 0.5;
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.Opacity = 0.5;
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder leftTop = grid.Children[gridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = topLgb;
				leftTop.BottomBorderBrush = leftLgb;
				leftTop.Opacity = 0.5;
				leftTop.BorderBrush = Brushes.Transparent;

				CustomBorder leftBottom = grid.Children[gridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = bottomLgb;
				leftBottom.TopBorderBrush = leftLgb;
				leftBottom.Opacity = 0.5;
				leftBottom.BorderBrush = Brushes.Transparent;
			}
			//剩下
			else
			{
				CustomBorder Top = grid.Children[gridNum - RowsCount] as CustomBorder;
				Top.LeftBorderBrush = topLgb;
				Top.RightBorderBrush = topLgb;
				Top.BottomBorderBrush = Brushes.Gray;
				Top.Opacity = 0.5;
				Top.BorderBrush = Brushes.Transparent;

				CustomBorder Left = grid.Children[gridNum - 1] as CustomBorder;
				Left.TopBorderBrush = leftLgb;
				Left.RightBorderBrush = Brushes.Gray;
				Left.BottomBorderBrush = leftLgb;
				Left.Opacity = 0.5;
				Left.BorderBrush = Brushes.Transparent;

				CustomBorder Right = grid.Children[gridNum + 1] as CustomBorder;
				Right.LeftBorderBrush = rightLgb;
				Right.TopBorderBrush = rightLgb;
				Right.BottomBorderBrush = Brushes.Gray;
				Right.Opacity = 0.5;
				Right.BorderBrush = Brushes.Transparent;

				CustomBorder Bottom = grid.Children[gridNum + RowsCount] as CustomBorder;
				Bottom.LeftBorderBrush = bottomLgb;
				Bottom.RightBorderBrush = bottomLgb;
				Bottom.TopBorderBrush = Brushes.Gray;
				Bottom.Opacity = 0.5;
				Bottom.BorderBrush = Brushes.Transparent;

				CustomBorder leftTop = grid.Children[gridNum - RowsCount - 1] as CustomBorder;
				leftTop.RightBorderBrush = topLgb;
				leftTop.BottomBorderBrush = leftLgb;
				leftTop.Opacity = 0.5;
				leftTop.BorderBrush = Brushes.Transparent;

				CustomBorder leftBottom = grid.Children[gridNum + RowsCount - 1] as CustomBorder;
				leftBottom.RightBorderBrush = bottomLgb;
				leftBottom.TopBorderBrush = leftLgb;
				leftBottom.Opacity = 0.5;
				leftBottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightBottom = grid.Children[gridNum + RowsCount + 1] as CustomBorder;
				rightBottom.LeftBorderBrush = bottomLgb;
				rightBottom.TopBorderBrush = rightLgb;
				rightBottom.Opacity = 0.5;
				rightBottom.BorderBrush = Brushes.Transparent;

				CustomBorder rightTop = grid.Children[gridNum - RowsCount + 1] as CustomBorder;
				rightTop.LeftBorderBrush = topLgb;
				rightTop.BottomBorderBrush = rightLgb;
				rightTop.Opacity = 0.5;
				rightTop.BorderBrush = Brushes.Transparent;
			}
			#endregion
		}
	}
}
