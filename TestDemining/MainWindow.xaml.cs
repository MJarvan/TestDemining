using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestDemining
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow:Window
	{
		BombEF bombEF = new BombEF();
		Random random = new Random();
		List<int> checkList = new List<int>();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender,RoutedEventArgs e)
		{
			deminingGrid.Children.Clear();
			checkList.Clear();

			//绑定炸弹实时更新
			Binding binding = new Binding()
			{
				Path = new PropertyPath("BombNum"),
				Source = bombEF,
				Mode = BindingMode.TwoWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			};
			BindingOperations.SetBinding(BombNum,TextBlock.TextProperty,binding);

			GenerteBomb();

			GenerteSquare();
		}

		private void GenerteBomb()
		{
			bombEF.BombNum = (deminingGrid.RowDefinitions.Count * deminingGrid.ColumnDefinitions.Count) / 5;
			for(int i = 0;i < bombEF.BombNum;i++)
			{
				int rowDef = 0;
				int colDef = 0;
				int BombDef = NextInt(random,0,deminingGrid.RowDefinitions.Count * deminingGrid.ColumnDefinitions.Count);
				if(checkList.Count != 0)
				{
					BombDef = CheckSameBomb(BombDef);
				}
				else
				{
					checkList.Add(BombDef);
				}

				if(BombDef < 10)
				{
					colDef = BombDef;
				}
				else
				{
					rowDef = BombDef / 10;
					colDef = BombDef % 10;
				}
				TextBlock textblock = new TextBlock();
				textblock.Text = string.Empty;
				textblock.Margin = new Thickness(5);
				textblock.Background = new SolidColorBrush(Colors.Red);
				KeyValuePair<int,bool?> dic = new KeyValuePair<int,bool?>(0,true);
				textblock.Tag = dic;
				deminingGrid.Children.Add(textblock);
				Grid.SetRow(textblock,rowDef);
				Grid.SetColumn(textblock,colDef);
				Panel.SetZIndex(textblock,0);
			}
		}

		private int CheckSameBomb(int bombDef)
		{
			int FineTimes = 0;
			foreach(int num in checkList)
			{
				if(num == bombDef)
				{
					bombDef = NextInt(random,0,deminingGrid.RowDefinitions.Count * deminingGrid.ColumnDefinitions.Count);
					return CheckSameBomb(bombDef);
				}
				else
				{
					FineTimes++;
				}
			}
			if(FineTimes == checkList.Count)
			{
				checkList.Add(bombDef);
			}
			return bombDef;
		}

		private void GenerteSquare()
		{
			for(int i = 0;i < deminingGrid.RowDefinitions.Count;i++)
			{
				for(int j = 0;j < deminingGrid.ColumnDefinitions.Count;j++)
				{
					int bombDef = i * 10 + j;
					int FineTimes = 0;

					foreach(int num in checkList)
					{
						if(bombDef != num)
						{
							FineTimes++;
						}
					}

					if(FineTimes == checkList.Count)
					{
						SearchAroundBomb(i,j,bombDef);
					}
					Button button = new Button();
					button.BorderThickness = new Thickness(0);
					button.Background = new SolidColorBrush(Colors.LightGray);
					button.Tag = false;
					Border border = new Border();
					border.BorderThickness = new Thickness(0.5);
					border.BorderBrush = new SolidColorBrush(Colors.Black);
					border.Child = button;
					deminingGrid.Children.Add(border);
					Grid.SetRow(border,i);
					Grid.SetColumn(border,j);
					Panel.SetZIndex(border,1);
				}
			}
		}

		private void SearchAroundBomb(int rowDef,int colDef,int selfNum)
		{
			TextBlock textblock = new TextBlock();
			
			int bNumber = 0;
			int styleNum = 0;

			#region 炸弹附近数字分配
			//一共九种
			if(rowDef == 0)
			{
				//第一种左上角的点
				if(colDef == 0)
				{
					styleNum = 1;
					foreach(int num in checkList)
					{
						if(num == (selfNum + 1) || num == (selfNum + 10) || num == (selfNum + 10 + 1))
						{
							bNumber++;
						}
					}
				}
				//第二种右上角的点
				else if(colDef == deminingGrid.ColumnDefinitions.Count - 1)
				{
					styleNum = 2;
					foreach(int num in checkList)
					{
						if(num == (selfNum - 1) || num == (selfNum + 10) || num == (selfNum + 10 - 1))
						{
							bNumber++;
						}
					}
				}
				//第三种上边两点之间
				else
				{
					styleNum = 3;
					foreach(int num in checkList)
					{
						if(num == (selfNum - 1) || num == (selfNum + 1) || num == (selfNum + 10 - 1) || num == (selfNum + 10) || num == (selfNum + 10 + 1))
						{
							bNumber++;
						}
					}
				}
			}
			else if(colDef == 0)
			{
				//第四种左下角的点
				if(rowDef == deminingGrid.RowDefinitions.Count - 1)
				{
					styleNum = 4;
					foreach(int num in checkList)
					{
						if(num == (selfNum + 1) || num == (selfNum - 10) || num == (selfNum - 10 + 1))
						{
							bNumber++;
						}
					}
				}
				//第五种左边两点之间
				else
				{
					styleNum = 5;
					foreach(int num in checkList)
					{
						if(num == (selfNum - 10) || num == (selfNum - 10 + 1) || num == (selfNum + 1) || num == (selfNum + 10) || num == (selfNum + 10 + 1))
						{
							bNumber++;
						}
					}
				}
			}
			else if(rowDef == deminingGrid.RowDefinitions.Count - 1)
			{
				//第六种右下角的点
				if(colDef == deminingGrid.ColumnDefinitions.Count - 1)
				{
					styleNum = 6;
					foreach(int num in checkList)
					{
						if(num == (selfNum - 1) || num == (selfNum - 10) || num == (selfNum - 10 - 1))
						{
							bNumber++;
						}
					}
				}
				//第七种下边两点之间
				else
				{
					styleNum = 7;
					foreach(int num in checkList)
					{
						if(num == (selfNum - 1) || num == (selfNum - 10 - 1) || num == (selfNum + 1) || num == (selfNum - 10) || num == (selfNum - 10 + 1))
						{
							bNumber++;
						}
					}
				}
			}
			else if(colDef == deminingGrid.ColumnDefinitions.Count - 1)
			{
				//第八种右边两点之间
				styleNum = 8;
				foreach(int num in checkList)
				{
					if(num == (selfNum - 1) || num == (selfNum - 10 - 1) || num == (selfNum + 10) || num == (selfNum - 10) || num == (selfNum + 10 - 1))
					{
						bNumber++;
					}
				}
			}
			else
			{
				//第九种其余的8格
				styleNum = 9;
				foreach(int num in checkList)
				{
					if(num == (selfNum - 1) || num == (selfNum - 10) || num == (selfNum - 10 - 1) || num == (selfNum - 10 + 1) || num == (selfNum + 1) || num == (selfNum + 10) || num == (selfNum + 10 - 1) || num == (selfNum + 10 + 1))
					{
						bNumber++;
					}
				}
			}
			#endregion

			if(bNumber != 0)
			{
				textblock.VerticalAlignment = VerticalAlignment.Center;
				textblock.HorizontalAlignment = HorizontalAlignment.Center;
				textblock.FontSize = 30;
				textblock.Text = bNumber.ToString();
				KeyValuePair<int,bool?> dic = new KeyValuePair<int,bool?>(styleNum,false);
				textblock.Tag = dic;
			}
			else
			{
				textblock.Text = string.Empty;
				KeyValuePair<int,bool?> dic = new KeyValuePair<int,bool?>(styleNum,null);
				textblock.Tag = dic;
			}
			deminingGrid.Children.Add(textblock);
			Grid.SetRow(textblock,rowDef);
			Grid.SetColumn(textblock,colDef);
			Panel.SetZIndex(textblock,0);
		}

		private void Grid_PreviewMouseDown(object sender,MouseButtonEventArgs e)
		{
			if(e.Source.GetType() == typeof(Button))
			{
				Button button = e.Source as Button;

				if(e.LeftButton == MouseButtonState.Pressed && e.RightButton == MouseButtonState.Released)
				{
					ButtonClick(button,true);
				}
				else if(e.LeftButton == MouseButtonState.Released && e.RightButton == MouseButtonState.Pressed)
				{
					ButtonClick(button,false);
				}
			}
			else if(e.Source.GetType() == typeof(TextBlock))
			{
				TextBlock textblock = e.Source as TextBlock;
				KeyValuePair<int,bool?> dic = (KeyValuePair<int,bool?>)textblock.Tag;

				if(e.LeftButton == MouseButtonState.Pressed && e.RightButton == MouseButtonState.Pressed)
				{
					int rowTBDef = Grid.GetRow(textblock);
					int colTBDef = Grid.GetColumn(textblock);
					int selfNum = rowTBDef * 10 + colTBDef;
					List<Button> listBTN = new List<Button>();
					if(dic.Value == false)
					{
						int tNumber = Convert.ToInt32(textblock.Text);
						int bNumber = 0;
						for(int i = bombEF.BombNum;i < deminingGrid.Children.Count;i++)
						{
							var a = deminingGrid.Children[i];
							if(a.GetType() == typeof(Border))
							{
								Border border = a as Border;
								int rowBTNDef = Grid.GetRow(border);
								int colBTNDef = Grid.GetColumn(border);
								int buttonNum = rowBTNDef * 10 + colBTNDef;

								Button button = border.Child as Button;
								if(button.Visibility == Visibility.Visible)
								{
									if(CheckDicKey(dic.Key,buttonNum,selfNum))
									{
										listBTN.Add(button);
										if((bool)button.Tag)
										{
											bNumber++;
										}
									}
								}
							}
						}
						if(bNumber == tNumber)
						{
							foreach(Button btn in listBTN)
							{
								ButtonClick(btn,true);
							}
						}
					}
					else if(dic.Value == null)
					{
						for(int i = bombEF.BombNum;i < deminingGrid.Children.Count;i++)
						{
							var a = deminingGrid.Children[i];
							if(a.GetType() == typeof(Border))
							{
								Border border = a as Border;
								int rowBTNDef = Grid.GetRow(border);
								int colBTNDef = Grid.GetColumn(border);
								int buttonNum = rowBTNDef * 10 + colBTNDef;

								Button button = border.Child as Button;
								if(button.Visibility == Visibility.Visible)
								{
									if(CheckDicKey(dic.Key,buttonNum,selfNum))
									{
										listBTN.Add(button);
									}
								}
							}
						}
						foreach(Button btn in listBTN)
						{
							ButtonClick(btn,true);
						}
					}
				}
			}
		}

		private bool CheckDicKey(int key,int buttonNum,int selfNum)
		{
			bool returnBool = false;

			#region 数字分配选择方式
			switch(key)
			{
				case 1:
					{
						if(buttonNum == (selfNum + 1) || buttonNum == (selfNum + 10) || buttonNum == (selfNum + 10 + 1))
						{
							returnBool = true;
						}
						break;
					}
				case 2:
					{
						if(buttonNum == (selfNum - 1) || buttonNum == (selfNum + 10) || buttonNum == (selfNum + 10 - 1))
						{
							returnBool = true;
						}
						break;
					}
				case 3:
					{
						if(buttonNum == (selfNum - 1) || buttonNum == (selfNum + 1) || buttonNum == (selfNum + 10 - 1) || buttonNum == (selfNum + 10) || buttonNum == (selfNum + 10 + 1))
						{
							returnBool = true;
						}
						break;
					}
				case 4:
					{
						if(buttonNum == (selfNum + 1) || buttonNum == (selfNum - 10) || buttonNum == (selfNum - 10 + 1))
						{
							returnBool = true;
						}
						break;
					}
				case 5:
					{
						if(buttonNum == (selfNum - 10) || buttonNum == (selfNum - 10 + 1) || buttonNum == (selfNum + 1) || buttonNum == (selfNum + 10) || buttonNum == (selfNum + 10 + 1))
						{
							returnBool = true;
						}
						break;
					}
				case 6:
					{
						if(buttonNum == (selfNum - 1) || buttonNum == (selfNum - 10) || buttonNum == (selfNum - 10 - 1))
						{
							returnBool = true;
						}
						break;
					}
				case 7:
					{
						if(buttonNum == (selfNum - 1) || buttonNum == (selfNum - 10 - 1) || buttonNum == (selfNum + 1) || buttonNum == (selfNum - 10) || buttonNum == (selfNum - 10 + 1))
						{
							returnBool = true;
						}
						break;
					}
				case 8:
					{
						if(buttonNum == (selfNum - 1) || buttonNum == (selfNum - 10 - 1) || buttonNum == (selfNum + 10) || buttonNum == (selfNum - 10) || buttonNum == (selfNum + 10 - 1))
						{
							returnBool = true;
						}
						break;
					}
				case 9:
					{
						if(buttonNum == (selfNum - 1) || buttonNum == (selfNum - 10) || buttonNum == (selfNum - 10 - 1) || buttonNum == (selfNum - 10 + 1) || buttonNum == (selfNum + 1) || buttonNum == (selfNum + 10) || buttonNum == (selfNum + 10 - 1) || buttonNum == (selfNum + 10 + 1))
						{
							returnBool = true;
						}
						break;
					}
			}
			#endregion

			return returnBool;
		}

		private void ButtonClick(Button button,bool state)
		{
			if(state)
			{
				if(!(bool)button.Tag)
				{
					button.Visibility = Visibility.Hidden;
					Border border = button.Parent as Border;
					int rowBTNDef = Grid.GetRow(border);
					int colBTNDef = Grid.GetColumn(border);

					//单纯的button不靠循环怎么拿到底层的textblock
					//if(tbdic.Value == null)
					{
						for(int i = bombEF.BombNum;i < deminingGrid.Children.Count;i++)
						{
							var a = deminingGrid.Children[i];
							if(a.GetType() == typeof(TextBlock))
							{
								TextBlock textblock = a as TextBlock;
								int rowTBDef = Grid.GetRow(textblock);
								int colTBDef = Grid.GetColumn(textblock);
								KeyValuePair<int,bool?> dic = (KeyValuePair<int,bool?>)textblock.Tag;
								if(rowBTNDef == rowTBDef && colBTNDef == colTBDef && dic.Value == true)
								{
									bombEF.BombNum--;
									MessageBox.Show("BOOM你输了!");
								}
							}
						}
					}
					//else if(tbdic.Value == false)
					{
						for(int i = 0;i < bombEF.BombNum;i++)
						{
							var a = deminingGrid.Children[i];
							if(a.GetType() == typeof(TextBlock))
							{
								TextBlock textblock = a as TextBlock;
								int rowTBDef = Grid.GetRow(textblock);
								int colTBDef = Grid.GetColumn(textblock);
								KeyValuePair<int,bool?> dic = (KeyValuePair<int,bool?>)textblock.Tag;
								if(rowBTNDef == rowTBDef && colBTNDef == colTBDef && dic.Value == true)
								{
									bombEF.BombNum--;
									MessageBox.Show("BOOM你输了!");
								}
							}
						}
					}
				}
			}
			else
			{
				if(!(bool)button.Tag && bombEF.BombNum != 0)
				{
					bombEF.BombNum--;
					button.Background = new SolidColorBrush(Colors.Green);
					button.Tag = true;
				}
				else if((bool)button.Tag && bombEF.BombNum != 0)
				{
					bombEF.BombNum++;
					button.Background = new SolidColorBrush(Colors.LightGray);
					button.Tag = false;
				}
				else
				{
					MessageBox.Show("你的排除已经用完!");
				}
			}
		}

		/// <summary>
		/// 生成设置范围内的Double的随机数
		/// </summary>
		/// <param name="random">Random</param>
		/// <param name="miniInt">生成随机数的最小值</param>
		/// <param name="maxInt">生成随机数的最大值</param>
		/// <returns>当Random等于NULL的时候返回0;</returns>
		public static int NextInt(Random random,int miniInt,int maxInt)
		{
			if(random != null)
			{
				return random.Next(miniInt,maxInt) + miniInt;
			}
			else
			{
				return 0;
			}
		}
	}

	/// <summary>
	/// 炸弹数实体
	/// </summary>
	public class BombEF:INotifyPropertyChanged
	{
		private int bombNum;
		public int BombNum
		{
			get
			{
				return bombNum;
			}
			set
			{
				bombNum = value;
				OnChangedProperties("bombNum");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnChangedProperties(string propertyName)
		{
			this.PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
		}
	}
}
