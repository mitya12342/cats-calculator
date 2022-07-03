using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace cats_calculator
{
    public class Calculator
    {
        string decimal_separator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;

        double first;
        double second;
        int NumberInput = 0;
        bool WaitNumber = true;
        string Sign;
        int Length;
        bool MPressed = false;
        double MemValue;
        bool ResultClicked = false;
        bool WaitSign = false;
        bool SpecialOpClicked = false;
        bool ButtonLock = false;
        bool PercentClicked = false;

        public string InnerMainDisplay { get; private set; } = "0";
        public string InnerHistoryDisplay { get; private set; } = "";
        public string InnerMemoryIndicator { get; private set; } = "";


        public void NumberPress(string num)
        {
            if (!ButtonLock)
            {
                if ((InnerMainDisplay.Length <= 15) || WaitNumber)
                {
                    if (WaitNumber || InnerMainDisplay.StartsWith("0") && !InnerMainDisplay.Contains(decimal_separator))
                    {
                        if (SpecialOpClicked)
                        {
                            InnerHistoryDisplay = InnerHistoryDisplay.Remove(InnerHistoryDisplay.Length - Length, Length);
                            SpecialOpClicked = false;
                        }
                        InnerMainDisplay = num;
                    }
                    else
                    {
                        InnerMainDisplay += num;
                    }
                }
                WaitNumber = false;
                WaitSign = true;
                ResultClicked = false;
                PercentClicked = false;
            }

        }

        public void DeleteAllPress()
        {
            first = 0;
            second = 0;
            InnerMainDisplay = "0";
            InnerHistoryDisplay = "";
            NumberInput = 0;
            WaitNumber = true;
            WaitSign = false;
            ResultClicked = false;
            SpecialOpClicked = false;
            ButtonLock = false;
            PercentClicked = false;

        }

        public void DeleteOncePress()
        {
            if (!ButtonLock)
            {
                if ((InnerMainDisplay.Contains("-") && (InnerMainDisplay.Length == 2)) || (InnerMainDisplay.Length == 1) || InnerMainDisplay.Contains("E") || (InnerMainDisplay == "-0,") || ResultClicked)
                {
                    InnerMainDisplay = "0";
                }
                else
                {
                    InnerMainDisplay = InnerMainDisplay.Remove(InnerMainDisplay.Length - 1, 1);
                }
                ResultClicked = false;
            }
        }

        public void ButtonPointPress()
        {
            if (!ButtonLock)
            {
                if (!InnerMainDisplay.Contains(decimal_separator))
                {
                    if (ResultClicked)
                    {
                        InnerMainDisplay = $"0{decimal_separator}";
                        WaitNumber = false;
                    }
                    else
                    {
                        InnerMainDisplay += decimal_separator;
                    }
                }
                PercentClicked = false;
                WaitNumber = false;
                WaitSign = true;
            }

        }

        public void ClickOp(string str)
        {

            if (!ButtonLock)
            {
                if (SpecialOpClicked)
                {
                    InnerHistoryDisplay = InnerHistoryDisplay.Remove(InnerHistoryDisplay.Length - Length, Length);
                    SpecialOpClicked = false;
                }
                if (InnerMainDisplay.EndsWith(decimal_separator))
                {
                    InnerMainDisplay = InnerMainDisplay.Remove(InnerMainDisplay.Length - 1, 1);
                }
                if (InnerMainDisplay == "-0")
                {
                    InnerMainDisplay = "0";
                }
                if (WaitSign || InnerMainDisplay.StartsWith("0"))
                {
                    if (NumberInput == 0)
                    {
                        Sign = str;
                        first = double.Parse(InnerMainDisplay);
                        NumberInput++;
                        InnerHistoryDisplay += Sign;
                        WaitSign = false;
                    }
                    else if (NumberInput >= 1)
                    {
                        WaitSign = false;
                        second = double.Parse(InnerMainDisplay);
                        InnerHistoryDisplay += InnerMainDisplay;
                        if (Sign == "+")
                        {
                            InnerMainDisplay = (first + second).ToString();
                            first += second;
                        }
                        if (Sign == "-")
                        {
                            InnerMainDisplay = (first - second).ToString();
                            first -= second;
                        }
                        if (Sign == "*")
                        {
                            InnerMainDisplay = (first * second).ToString();
                            first *= second;
                        }
                        if (Sign == "/")
                        {
                            if (second == 0)
                            {
                                InnerMainDisplay = "Деление на 0";
                                ButtonLock = true;
                            }
                            else
                            {
                                InnerMainDisplay = (first / second).ToString();
                                first /= second;
                            }
                        }
                        if (ButtonLock)
                        {
                            return;
                        }
                        Sign = str;
                        InnerHistoryDisplay += Sign;
                        first = double.Parse(InnerMainDisplay);
                    }
                    WaitNumber = true;
                }
                else if (InnerHistoryDisplay.Length != 0)
                {
                    InnerHistoryDisplay = InnerHistoryDisplay.Remove(InnerHistoryDisplay.Length - 1, 1) + str;
                    Sign = str;
                }
                ResultClicked = false;
                SpecialOpClicked = false;
                PercentClicked = false;
            }
        }

        public void NegOrPosClick()
        {
            if (!ButtonLock)
            {
                if ((InnerMainDisplay.Length == 1) && InnerMainDisplay.Contains("0"))
                {

                }
                else
                {
                    if (InnerMainDisplay.Contains("-"))
                    {
                        InnerMainDisplay = InnerMainDisplay.Remove(0, 1);
                    }
                    else
                    {
                        InnerMainDisplay = "-" + InnerMainDisplay;
                    }
                }
            }

        }

        public void ClickRes()
        {
            if (!ButtonLock)
            {
                if (!ResultClicked)
                {
                    second = double.Parse(InnerMainDisplay);
                }
                else
                {
                    first = double.Parse(InnerMainDisplay);
                }
                if (Sign == "+")
                {
                    InnerMainDisplay = (first + second).ToString();
                }
                if (Sign == "-")
                {
                    InnerMainDisplay = (first - second).ToString();
                }
                if (Sign == "*")
                {
                    InnerMainDisplay = (first * second).ToString();
                }
                if (Sign == "/")
                {
                    if (second == 0)
                    {
                        InnerMainDisplay = "Деление на 0";
                        ButtonLock = true;
                    }
                    else
                    {
                        InnerMainDisplay = (first / second).ToString();
                    }
                }
                InnerHistoryDisplay = "";
                NumberInput = 0;
                WaitNumber = true;
                ResultClicked = true;
                WaitSign = true;
                PercentClicked = false;
            }
        }

        public void ClickDeleteAdd()
        {
            if (!ButtonLock)
            {
                InnerMainDisplay = "0";
                WaitNumber = true;
                if (SpecialOpClicked)
                {
                    InnerHistoryDisplay = InnerHistoryDisplay.Remove(InnerHistoryDisplay.Length - Length, Length);
                    SpecialOpClicked = false;
                }
                PercentClicked = false;
            }
        }

        public void MRClick()
        {
            if (!ButtonLock)
            {
                WaitNumber = true;
                if (MPressed)
                {
                    InnerMainDisplay = MemValue.ToString();
                }
                else
                {
                    InnerMainDisplay = "0";

                }
            }
        }

        public void MSClick()
        {
            if (!ButtonLock)
            {
                WaitNumber = true;
                MPressed = true;
                MemValue = double.Parse(InnerMainDisplay);
                InnerMemoryIndicator = "M";
            }
        }

        public void MCClick()
        {
            if (!ButtonLock)
            {
                if (MPressed)
                {
                    MPressed = false;
                    InnerMemoryIndicator = "";
                    MemValue = 0;
                    WaitNumber = true;
                }
            }
        }

        public void MPlusClick()
        {
            if (!ButtonLock)
            {
                WaitNumber = true;
                MPressed = true;
                MemValue += double.Parse(InnerMainDisplay);
                InnerMemoryIndicator = "M";
            }
        }

        public void MMinusClick()
        {
            if (!ButtonLock)
            {
                WaitNumber = true;
                MPressed = true;
                MemValue -= double.Parse(InnerMainDisplay);
                InnerMemoryIndicator = "M";
            }

        }

        public void SpecialOpClick(string str)
        {
            if (!ButtonLock)
            {
                if (!SpecialOpClicked)
                {
                    Length = 6 + InnerMainDisplay.Length;
                }
                else
                {
                    InnerHistoryDisplay = InnerHistoryDisplay.Remove(InnerHistoryDisplay.Length - Length, Length);
                    Length = 6 + InnerMainDisplay.Length;
                }
                if (str == "%")
                {
                    if (PercentClicked)
                    {
                        InnerHistoryDisplay = InnerHistoryDisplay.Remove(InnerHistoryDisplay.Length - InnerMainDisplay.Length, InnerMainDisplay.Length);
                    }
                    second = double.Parse(InnerMainDisplay);
                    InnerMainDisplay = (first / 100 * second).ToString();
                    InnerHistoryDisplay += InnerMainDisplay;
                    WaitNumber = true;
                    PercentClicked = true;
                }
                if (str == "√")
                {
                    if (double.Parse(InnerMainDisplay) < 0)
                    {
                        InnerMainDisplay = "Недопустимый ввод";
                        WaitNumber = true;
                        ButtonLock = true;
                    }
                    else
                    {
                        InnerHistoryDisplay = InnerHistoryDisplay + "sqrt(" + InnerMainDisplay + ")";
                        InnerMainDisplay = (Math.Sqrt(double.Parse(InnerMainDisplay))).ToString();
                        WaitNumber = true;
                        SpecialOpClicked = true;
                        PercentClicked = false;
                    }
                }
                if (str == "1/x")
                {
                    if (double.Parse(InnerMainDisplay) == 0)
                    {
                        InnerMainDisplay = "Деление на 0";
                        ButtonLock = true;
                    }
                    else
                    {
                        InnerHistoryDisplay = InnerHistoryDisplay + "reciproc(" + InnerMainDisplay + ")";
                        InnerMainDisplay = (1 / double.Parse(InnerMainDisplay)).ToString();
                        WaitNumber = true;
                        SpecialOpClicked = true;
                        PercentClicked = false;
                    }
                }
            }
        }

    }
}