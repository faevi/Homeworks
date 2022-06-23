namespace HomeWork2
{
    public class StackCalculator
    {
        private int _operationStackSize = 0;
        private int _outStackSize = 0;
        private Operation[] _operationStack;
        private string[] _outStack;
        private bool _isNextSign = true;
        private bool _isPreviousOperation = false;

        protected StackCalculator(string formula)
        {
            _operationStackSize = 0;
            _outStackSize = 0;
            _isNextSign = true;
            _isPreviousOperation = false;
            _operationStack = new Operation[formula.Length];
            _outStack = new string[formula.Length];
        }

        protected struct Operation
        {
            public char OperationName { get; set; }
            public int  OperationPriority { get; set; }
            public Operation(char name)
            {
                this.OperationName = name;
                this.OperationPriority = name switch
                {
                    '/' => 1,
                    '*' => 1,
                    '+' => 2,
                    '-' => 2,
                    '(' => 0,
                    ')' => 0
                };
            }
        }

        protected static double DoOperation(double x, double y, Operation op)
        {
            double result = op.OperationName switch
            {
                '/' => x / y,
                '*' => x * y,
                '+' => x + y,
                '-' => x - y
            };
            return result;
        }

        protected void ChangeStack(Operation op, StackCalculator calc)
        {
            switch (op.OperationName)
            {
                case '(':
                    calc.IfOpenScobe(op);
                    break;
                case ')':
                    calc.IfCloseScobe();
                    break;
                case '+':
                    calc.IfBinary(op);
                    break;
                case '-':
                    calc.IfBinary(op);
                    break;
                case '/':
                    calc.IfBinary(op);
                    break;
                case '*':
                    calc.IfBinary(op);
                    break;
            }
        }

        // если операция открытой скобки
        protected virtual void IfOpenScobe(in Operation ch)
        {
            _operationStack[_operationStackSize] = ch;
            _operationStackSize++;
            _isNextSign = true;
            _isPreviousOperation = false;
        }

        // если операция закрытой скобки
        protected virtual void IfCloseScobe()
        {
            _outStackSize++;
            while (_operationStack[_operationStackSize - 1].OperationName != '(')
            {
                _outStack[_outStackSize] += _operationStack[_operationStackSize - 1].OperationName;
                _outStackSize++;


                if (_operationStackSize - 1 == 0)
                {
                    Console.WriteLine("Wrong formula format!");
                    return;
                }

                _operationStackSize--;
            }
            _isPreviousOperation = false;
            _operationStackSize--;
            _isNextSign = false;
        }

        // Если это бинарная операция
        protected virtual void IfBinary(Operation op)
        {
            // если до этого операционный стак был пустой
            // или предыдущей операцией была скобка
            if (_operationStackSize == 0 || _operationStack[_operationStackSize - 1].OperationPriority == 0)
            {
                _operationStack[_operationStackSize] = op;

                if (_outStackSize != 0 && _operationStackSize == 0)
                {
                    _outStackSize--;
                }

                _outStackSize++;
            }
            // если предыдщая операция была меньшего приоритета
            else if (_operationStack[_operationStackSize - 1].OperationPriority > op.OperationPriority)
            {
                _operationStack[_operationStackSize] = op;
            }
            // если большего приоритета
            else
            {
                _operationStackSize--;

                while (_operationStack[_operationStackSize].OperationPriority <= op.OperationPriority ||
                    _operationStack[_operationStackSize].OperationPriority != 0)
                {
                    _outStackSize++;
                    _outStack[_outStackSize] += _operationStack[_operationStackSize].OperationName;
                    _outStackSize++;

                    if (_operationStackSize == 0)
                    {
                        break;
                    }
                    _operationStackSize--;
                }

                _operationStack[_operationStackSize] = op;
            }
            _isNextSign = false;
            _isPreviousOperation = true;
            _operationStackSize++;
        }

        // Инициализирует стэк калькулятора перед операцией
        protected virtual void CreateStack(in string formula, StackCalculator calc)
        {
            _operationStack = new Operation[formula.Length];
            _outStack = new string[formula.Length];
            _operationStackSize = 0;
            _outStackSize = 0;
            _isNextSign = true;
            _isPreviousOperation = false;

            char[] CH = formula.ToCharArray();

            foreach (char ch in CH) //алгоритм преобразованя из инфиксной нотации в обратную польскую запись
            {
                int bar;

                if (int.TryParse(ch.ToString(), out bar) || ch == '.' || _isNextSign && ch != '(' && ch != ')')
                {
                    _outStack[_outStackSize] += ch;
                    _isNextSign = false;
                    continue;
                }

                ChangeStack(new Operation(ch), calc);
            }

            // перенос оставшегося стека операций в выохдной стек
            for (int stackNode = _operationStackSize; stackNode > 0; --stackNode)
            {
                _outStackSize++;

                if (!_isPreviousOperation)
                {
                    _outStackSize--;
                    _isPreviousOperation = true;
                }

                _outStack[_outStackSize] += _operationStack[stackNode - 1].OperationName;
            }
            _outStackSize++;
        }

        // Само вычисление, проход по постфисной записи с выполнением операций
        public string DoCallculation(string formula)
        {
            StackCalculator calc = new StackCalculator(formula);
            calc.CreateStack(formula, calc);
            int step = 0;
            double firstValue = 0;
            double secondValue = 0;

            while (step + 2 < _outStackSize)
            {
                double bar;

                if (!double.TryParse(_outStack[step + 2], out bar))
                {
                    if (double.TryParse(_outStack[step], out firstValue)
                        && double.TryParse(_outStack[step + 1], out secondValue))
                    {
                        char charOperation = _outStack[step + 2].ToCharArray()[0];
                        Operation op = new Operation(charOperation);
                        _outStack[step + 2] = Convert.ToString(DoOperation(firstValue, secondValue, op));
                    }
                }

                step++;
            }
            return _outStack[step + 1];
        }

        // демонстрация постфиксной записи
        public void ShowOutStack()
        {
            Console.WriteLine("Out Stack with {0} elemnts: ", _outStackSize);

            for (int step = 0; step < _outStackSize; step++)
            {
                Console.WriteLine(_outStack[step] + " ");
            }
        }

        // демонстрация стэка операций
        public void ShowOpStack()
        {
            Console.WriteLine("Op Stack with {0} elemnts: ", _operationStackSize);

            for (int step = 0; step < _operationStackSize; step++)
            {
                Console.WriteLine(_operationStack[step].OperationName + " ");
            }
        }
    }
}


