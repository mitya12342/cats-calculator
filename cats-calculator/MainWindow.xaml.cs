using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace cats_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Calculator Calculator = new Calculator();

        public MainWindow()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            InitializeComponent();
        }

        public void UpdateDisplay()
        {
            Display.Content = Calculator.InnerMainDisplay;
            DisplayHistory.Content = Calculator.InnerHistoryDisplay;
            Display_M.Content = Calculator.InnerMemoryIndicator;
        }

        private void Button_Number(object sender, RoutedEventArgs e)
        {
            Calculator.NumberPress((sender as Button).Content.ToString());
            UpdateDisplay();
        }

        private void Button_Delete_All(object sender, RoutedEventArgs e)
        {
            Calculator.DeleteAllPress();
            UpdateDisplay();

        }

        private void Button_Delete_Once(object sender, RoutedEventArgs e)
        {
            Calculator.DeleteOncePress();
            UpdateDisplay();

        }

        private void Button_Point(object sender, RoutedEventArgs e)
        {
            Calculator.ButtonPointPress();
            UpdateDisplay();

        }

        private void Button_Click_Op(object sender, RoutedEventArgs e)
        {
            Calculator.ClickOp((sender as Button).Content.ToString());
            UpdateDisplay();

        }

        private void Neg_Or_Pos_Button_Click(object sender, RoutedEventArgs e)
        {
            Calculator.NegOrPosClick();
            UpdateDisplay();

        }

        private void Button_Click_Res(object sender, RoutedEventArgs e)
        {
            Calculator.ClickRes();
            UpdateDisplay();


        }

        private void Button_Delete_Add(object sender, RoutedEventArgs e)
        {
            Calculator.ClickDeleteAdd();
            UpdateDisplay();

        }

        private void Button_MR_Click(object sender, RoutedEventArgs e)
        {
            Calculator.MRClick();
            UpdateDisplay();

        }

        private void Button_MS_Click(object sender, RoutedEventArgs e)
        {
            Calculator.MSClick();
            UpdateDisplay();

        }

        private void Button_MC_Click(object sender, RoutedEventArgs e)
        {
            Calculator.MCClick();
            UpdateDisplay();

        }

        private void Button_M_Plus_Click(object sender, RoutedEventArgs e)
        {
            Calculator.MPlusClick();
            UpdateDisplay();

        }

        private void Button_M_Minus_Click(object sender, RoutedEventArgs e)
        {
            Calculator.MMinusClick();
            UpdateDisplay();

        }

        private void ButtonSpecialOp(object sender, RoutedEventArgs e)
        {
            Calculator.SpecialOpClick((sender as Button).Content.ToString());
            UpdateDisplay();

        }

        private void WinTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length == 1)
            {
                Char inputChar = e.Text[0];
                if (Char.IsDigit(inputChar))
                {
                    Calculator.NumberPress(inputChar.ToString());

                }
                else if ((inputChar == ',' || inputChar == '.'))
                {
                    Calculator.ButtonPointPress();
                }
                else if (inputChar == '\b' || inputChar == '←')
                {
                    Calculator.DeleteOncePress();
                }
                else if (inputChar == '=' || inputChar == '\n')
                {
                    Calculator.ClickRes();
                }
                else if (inputChar == '+' || inputChar == '-' || inputChar == '*' || inputChar == '/')
                {
                    Calculator.ClickOp(inputChar.ToString());
                }
                UpdateDisplay();

            }
            e.Handled = true;
        }

        private void Keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Space)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                Calculator.ClickRes();
                UpdateDisplay();
                e.Handled = true;

            }
        }
    }
}