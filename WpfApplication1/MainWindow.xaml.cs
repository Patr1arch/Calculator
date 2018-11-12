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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double first; // Первое вводимое число
        double second; // Второе вводимое число
        int NumberInput = 0;
        bool IsNumberWait = true; // Ожидание калькулятора числа
        string Sign; // Знак
        int lenghtspecialar; 
        bool IsMPressed = false;
        double value_M;
        bool IsResClick = false;
        bool IsSignWait = false;
        bool IsSpecialArClick = false;
        bool IsButtonsFreezing = false;
        bool IsPercentClick = false;

        public MainWindow()
        {
            InitializeComponent();
            TextBox_Main.Focus();
        }

        private void Button_Number(object sender, RoutedEventArgs e) // Кнопка ввода на TextBox_Main цифр
        {
            if (!IsButtonsFreezing)
            {
                if ((TextBox_Main.Text.Length <= 15) || (IsNumberWait)) // Если клавиатура вмещает 16 цифр или если ждем число
                {
                    if ((IsNumberWait) || (TextBox_Main.Text.StartsWith("0")) && !(TextBox_Main.Text.Contains(","))) // Если нуждаемся в числе или начинается стркоа с нуля
                    {
                        if (IsSpecialArClick)
                        {
                            TextBox_Add.Text = TextBox_Add.Text.Remove(TextBox_Add.Text.Length - lenghtspecialar, lenghtspecialar);
                            IsSpecialArClick = false;
                        }
                        TextBox_Main.Text = (sender as Button).Content.ToString(); // Добавляем нажатое число в главное окно.
                    }
                    else
                    {
                        TextBox_Main.Text = TextBox_Main.Text + (sender as Button).Content.ToString(); // либо добавлять цифры в строку.
                    }
                }
                IsNumberWait = false; // Мы больше не нуждаемся в чилсе
                IsSignWait = true; // Мы нуждаемся в знаке
                IsResClick = false;
                IsPercentClick = false;
            }
        }

        private void Button_Delete_All(object sender, RoutedEventArgs e)
        {
            first = 0;
            second = 0;
            TextBox_Main.Text = "0";
            TextBox_Add.Text = "";
            NumberInput = 0;
            IsNumberWait = true;
            IsSignWait = false;
            IsResClick = false;
            IsSpecialArClick = false;
            IsButtonsFreezing = false;
            IsPercentClick = false;
        }

        private void Button_Delete_Once(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                if (((TextBox_Main.Text.Contains("-")) && (TextBox_Main.Text.Length == 2)) || (TextBox_Main.Text.Length == 1) || TextBox_Main.Text.Contains("E") || (TextBox_Main.Text == "-0,") || (IsResClick))
                {
                    TextBox_Main.Text = "0";
                }
                else
                {
                    TextBox_Main.Text = TextBox_Main.Text.Remove(TextBox_Main.Text.Length - 1, 1);
                }
                IsResClick = false;
            }  
        }

        private void Button_Point(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                if (!TextBox_Main.Text.Contains(","))
                {
                    if (IsResClick)
                    {
                        TextBox_Main.Text = "0,";
                        IsNumberWait = false;
                    }
                    else
                    {
                        TextBox_Main.Text = TextBox_Main.Text + (sender as Button).Content.ToString();
                    }
                }
                IsPercentClick = false;
                IsNumberWait = false;
                IsSignWait = true;
            }
        }

        private void TextBox_Main_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TextBox_Main_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void TextBox_Main_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Button_Click_Ar(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                if (IsSpecialArClick)
                {
                    TextBox_Add.Text = TextBox_Add.Text.Remove(TextBox_Add.Text.Length - lenghtspecialar, lenghtspecialar);
                    IsSpecialArClick = false;
                }
                if (TextBox_Main.Text.EndsWith(","))
                {
                    TextBox_Main.Text = TextBox_Main.Text.Remove(TextBox_Main.Text.Length - 1, 1);
                }
                if (TextBox_Main.Text == "-0")
                {
                    TextBox_Main.Text = "0";
                }
                if ((IsSignWait) || (TextBox_Main.Text.StartsWith("0")))
                {
                    if (NumberInput == 0)
                    {
                        Sign = (sender as Button).Content.ToString();
                        first = double.Parse(TextBox_Main.Text);
                        NumberInput++;
                        TextBox_Add.Text = TextBox_Main.Text + Sign;
                        IsSignWait = false;
                    }
                    else if (NumberInput >= 1)
                    {
                        IsSignWait = false;
                        second = double.Parse(TextBox_Main.Text);
                        TextBox_Add.Text = TextBox_Add.Text + TextBox_Main.Text;
                        if (Sign == "+")
                        {
                            TextBox_Main.Text = (first + second).ToString();
                            first = (first + second);
                        }
                        if (Sign == "-")
                        {
                            TextBox_Main.Text = (first - second).ToString();
                            first = (first - second);
                        }
                        if (Sign == "*")
                        {
                            TextBox_Main.Text = (first * second).ToString();
                            first = (first * second);
                        }
                        if (Sign == "/")
                        {
                            if (second == 0)
                            {
                                TextBox_Main.Text = "Error:divided by zero!";
                                IsButtonsFreezing = true;
                            }
                            else
                            {
                                TextBox_Main.Text = (first / second).ToString();
                                first = (first / second);
                            }
                        }
                        if (IsButtonsFreezing)
                        {
                            return;
                        }
                        Sign = (sender as Button).Content.ToString();
                        TextBox_Add.Text = TextBox_Add.Text + Sign;
                        first = double.Parse(TextBox_Main.Text);
                    }
                    IsNumberWait = true;
                }
                else if (TextBox_Add.Text.Length != 0)
                {
                    TextBox_Add.Text = TextBox_Add.Text.Remove(TextBox_Add.Text.Length - 1, 1) + (sender as Button).Content;
                    Sign = (sender as Button).Content.ToString();
                }
                IsResClick = false;
                IsSpecialArClick = false;
                IsPercentClick = false;
            }
        }

        private void Neg_Or_Pos_Click(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                if ((TextBox_Main.Text.Length == 1) && (TextBox_Main.Text.Contains("0")))
                {

                }
                else
                {
                    if ((TextBox_Main.Text.Contains("-")))
                    {
                        TextBox_Main.Text = TextBox_Main.Text.Remove(0, 1);
                    }
                    else
                    {
                        TextBox_Main.Text = "-" + TextBox_Main.Text;
                    }
                }
            }
        }

        private void Button_Click_Res(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                if (!IsResClick)
                {
                    second = double.Parse(TextBox_Main.Text);
                }
                else
                {
                    first = double.Parse(TextBox_Main.Text);
                }
                if (Sign == "+")
                {
                    TextBox_Main.Text = (first + second).ToString();
                }
                if (Sign == "-")
                {
                    TextBox_Main.Text = (first - second).ToString();
                }
                if (Sign == "*")
                {
                    TextBox_Main.Text = (first * second).ToString();
                }
                if (Sign == "/")
                {
                    if (second == 0)
                    {
                        TextBox_Main.Text = "Error:divided by zero!";
                        IsButtonsFreezing = true;
                    }
                    else
                    {
                        TextBox_Main.Text = (first / second).ToString();
                    }
                }
                TextBox_Add.Text = "";
                NumberInput = 0;
                IsNumberWait = true;
                IsResClick = true;
                IsSignWait = true;
                IsPercentClick = false;
            }
            
        }

        private void Button_Delete_Add(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                TextBox_Main.Text = "0";
                IsNumberWait = true;
                if (IsSpecialArClick)
                {
                    TextBox_Add.Text = TextBox_Add.Text.Remove(TextBox_Add.Text.Length - lenghtspecialar, lenghtspecialar);
                    IsSpecialArClick = false;
                }
                IsPercentClick = false;
            }  
        }

        private void Button_MR_Click(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                IsNumberWait = true;
                if (IsMPressed)
                {
                    TextBox_Main.Text = value_M.ToString();
                }
            }   
        }

        private void Button_MS_Click(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                IsNumberWait = true;
                IsMPressed = true;
                value_M = double.Parse(TextBox_Main.Text);
                label_M.Content = "M";
            }
        }

        private void Button_MC_Click(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                if (IsMPressed)
                {
                    IsMPressed = false;
                    label_M.Content = "";
                    value_M = 0;
                    IsNumberWait = true;
                }
            }
        }

        private void Button_M_Plus_Click(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                IsNumberWait = true;
                IsMPressed = true;
                value_M = value_M + double.Parse(TextBox_Main.Text);
                label_M.Content = "M";
            }   
        }

        private void Button_M_Minus_Click(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                IsNumberWait = true;
                IsMPressed = true;
                value_M = value_M - double.Parse(TextBox_Main.Text);
                label_M.Content = "M";
            }
        }

        private void ButtonSpecialAr(object sender, RoutedEventArgs e)
        {
            if (!IsButtonsFreezing)
            {
                if (!IsSpecialArClick)
                {
                    lenghtspecialar = 6 + TextBox_Main.Text.Length;
                }
                else
                {
                    TextBox_Add.Text = TextBox_Add.Text.Remove(TextBox_Add.Text.Length - lenghtspecialar, lenghtspecialar);
                    lenghtspecialar = 6 + TextBox_Main.Text.Length;
                }
                if ((sender as Button).Content.ToString() == "%")
                {
                    if (IsPercentClick)
                    {
                        TextBox_Add.Text = TextBox_Add.Text.Remove(TextBox_Add.Text.Length - TextBox_Main.Text.Length, TextBox_Main.Text.Length);
                    }
                    second = double.Parse(TextBox_Main.Text);
                    TextBox_Main.Text = (first / 100 * second).ToString();
                    TextBox_Add.Text = TextBox_Add.Text + TextBox_Main.Text;
                    IsNumberWait = true;
                    IsPercentClick = true;
                }
                if ((sender as Button).Content.ToString() == "√")
                {
                    if (double.Parse(TextBox_Main.Text) < 0)
                    {
                        TextBox_Main.Text = "Error:negative value at the root!";
                        IsNumberWait = true;
                        IsButtonsFreezing = true;
                    }
                    else
                    {
                        TextBox_Add.Text = TextBox_Add.Text + "sqrt(" + TextBox_Main.Text + ")";
                        TextBox_Main.Text = (Math.Sqrt(double.Parse(TextBox_Main.Text))).ToString();
                        IsNumberWait = true;
                        IsSpecialArClick = true;
                        IsPercentClick = false;
                    }
                }
                if ((sender as Button).Content.ToString() == "x^2")
                {
                    TextBox_Add.Text = TextBox_Add.Text + "squa(" + TextBox_Main.Text + ")";
                    TextBox_Main.Text = (Math.Pow(double.Parse(TextBox_Main.Text), 2)).ToString();
                    IsNumberWait = true;
                    IsSpecialArClick = true;
                    IsPercentClick = false;
                }
                if ((sender as Button).Content.ToString() == "x^3")
                {
                    TextBox_Add.Text = TextBox_Add.Text + "cube(" + TextBox_Main.Text + ")";
                    TextBox_Main.Text = (Math.Pow(double.Parse(TextBox_Main.Text), 3)).ToString();
                    IsNumberWait = true;
                    IsSpecialArClick = true;
                    IsPercentClick = false;
                }
                if ((sender as Button).Content.ToString() == "1/x")
                {
                    if (double.Parse(TextBox_Main.Text) == 0) 
                    {
                        TextBox_Main.Text = "Error:divided by zero!";
                        IsButtonsFreezing = true;
                    }
                    else
                    {
                        TextBox_Add.Text = TextBox_Add.Text + "reci(" + TextBox_Main.Text + ")";
                        TextBox_Main.Text = (1 / double.Parse(TextBox_Main.Text)).ToString();
                        IsNumberWait = true;
                        IsSpecialArClick = true;
                        IsPercentClick = false;
                    }
                }
            }
        }
    }
}
