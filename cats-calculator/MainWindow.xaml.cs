using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace cats_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        string num_text = "0";
        double shown_number = 0;
        string decimal_separator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
        public MainWindow()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            InitializeComponent();
        }

        private void UpdateShownNumber()
        {
            shown_number = Double.Parse(num_text);
            if (num_text == "0" || shown_number == 0.0)
            {
                Display.Content = "0";
            }
            else if (shown_number > 1e-15 && shown_number < 1e15)
            {
                string display_format = DigitGroupingCheckbox.IsChecked ? "#,0.################" : "0.################";
                Display.Content = shown_number.ToString(display_format);
                if (num_text.EndsWith(decimal_separator))
                {
                    Display.Content += decimal_separator;
                } else if (num_text.EndsWith("0") && num_text.Contains(decimal_separator))
                {
                    Display.Content = ((int)shown_number).ToString(display_format);
                    Display.Content += decimal_separator;
                    Display.Content += num_text.Split(decimal_separator.ToCharArray()[0])[1];
                }
            } else
            {
                Display.Content = shown_number.ToString("E15");
            }

            if (Display.Content.ToString().Length > 20)
            {
                Display.FontSize = 14;
            } else if (Display.Content.ToString().Length > 12)
            {
                Display.FontSize = 18;
            } else
            {
                Display.FontSize = 22;
            }
        }

        private void PutChar(Char inputChar)
        {
            if (Char.IsDigit(inputChar))
            {
                if (!(inputChar == 0 && num_text.StartsWith("0") && !num_text.Contains(decimal_separator)))
                {
                    if (num_text == "0")
                    {
                        num_text = inputChar.ToString();
                    }
                    else
                    {
                        num_text += inputChar;
                    }
                    UpdateShownNumber();
                }

            }
            else if ((inputChar == ',' || inputChar == '.') && !num_text.Contains(decimal_separator))
            {
                num_text += decimal_separator;
                UpdateShownNumber();
            }
            else if (inputChar == '\b' || inputChar == '←')
            {
                if (num_text.Length == 1)
                {
                    num_text = "0";
                }
                else
                {
                    num_text = num_text.Remove(num_text.Length - 1, 1);
                }
                UpdateShownNumber();
            }
        }

        private void DigitBtnClick(object sender, RoutedEventArgs e)
        {
            PutChar(((Button)sender).Content.ToString()[0]);
        }

        private void WinTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0)
            {
                PutChar(e.Text[0]);
            }
        }

        private void ToggleGrouping(object sender, RoutedEventArgs e)
        {
            UpdateShownNumber();
        }
    }
}
