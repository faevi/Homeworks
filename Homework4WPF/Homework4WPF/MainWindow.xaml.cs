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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private double _value = 0;
        private bool   _isValueSet = false;
        private string _operation;
        private bool   _isOperationSet = false;

        public MainWindow()
        {
            InitializeComponent(); 

            //задаем логику кнопок
            foreach (UIElement element in buttonGrid.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Click += ButtonOperation;     
                }
            }
        }

        //случай когда надо очитстить оба регистра
        private double buttonC()
        {
            _isValueSet = false;
            _isOperationSet = false;
            return 0;
        }
        private double DoOperation(double x, string operation, double y = 0)
        {
            double result = operation switch
            {
                "/" => x / y,
                "*" => x * y,
                "+" => x + y,
                "-" => x - y,
                "+/-" => -1 * Convert.ToDouble(mainPanel.Text),
                "del" => mainPanel.Text.Length < 2 ? 0 : Convert.ToDouble(mainPanel.Text.Remove(mainPanel.Text.Length - 1)),
                "1/x" => 1.0 / Convert.ToDouble(mainPanel.Text),
                "x^2" => Math.Pow(Convert.ToDouble(mainPanel.Text),2),
                "sqrt(x)" => Math.Sqrt(Convert.ToDouble(mainPanel.Text)),
                "%" => Convert.ToDouble(mainPanel.Text) / 100,
                "CE" => 0.0,
                "C" => buttonC()
            };
            return result;
        }

        private bool isBinaryOPeration(string operation) => operation switch
        {
            "/" => true,
            "*" => true,
            "+" => true,
            "-" => true,
            _ => false
        };

        private void ButtonOperation(object sender, RoutedEventArgs e)
        {
            string buttonPurpose = (string)((Button)e.OriginalSource).Content;

            //Проверяем на цифру
            if (Int32.TryParse(buttonPurpose, out int output) || buttonPurpose ==  ".")
            {
                // если уже задано число и идет запись второго
                if (_isValueSet)
                {
                    mainPanel.Text = "";
                    _isValueSet = false;
                }

                // если на панели 0 и мы пишем не дробное
                if (mainPanel.Text == "0" && buttonPurpose !=  ".")
                {
                    mainPanel.Text="";
                }
                mainPanel.Text += buttonPurpose;
                return;
            } else if (buttonPurpose != "=") // если не цифра, а операция
            {
                // если не бинарная операция
                if (!isBinaryOPeration(buttonPurpose))
                {
                    mainPanel.Text = DoOperation(Convert.ToDouble(mainPanel.Text), buttonPurpose).ToString();
                    return;
                }
                // если уже была задана опреация
                if (_isOperationSet)
                {
                    _value = DoOperation(_value, _operation, Convert.ToDouble(mainPanel.Text));                    
                } else // если не задана операция
                {
                    _value = Convert.ToDouble(mainPanel.Text);
                }
                _operation = buttonPurpose;
                _isOperationSet = true;               
            } else // если знак равно
            {
                if(_operation == null)
                {
                    return;
                }

                _value = DoOperation(_value, _operation, Convert.ToDouble(mainPanel.Text));
                _isOperationSet = false;
            }
            mainPanel.Text = _value.ToString();
            _isValueSet = true;
        }
    }
}
