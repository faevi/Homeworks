﻿
namespace HomeWork2
{
    class StackCalculator
    {
        private static int operationStackSize = 0;
        private static int outStackSize = 0;
        private static Operation[] operationStack;
        private static string[] outStack;
        private static bool isNextSign = true;
        private static bool isPreviousOperation = false;

        protected StackCalculator(in string formula)
        {
            operationStackSize = 0;
            outStackSize = 0;
            isNextSign = true;
            isPreviousOperation = false;
            operationStack = new Operation[formula.Length];
            outStack = new string[formula.Length];
        }

        protected struct Operation
        {
            public char operationName { get; set; }
            public int operationPriority { get; set; }
            public Operation(char name)
            {
                this.operationName = name;
                this.operationPriority = name switch
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

        //private enum Operation
        //{
        //    Add,
        //    Subtract,
        //    Multiply,
        //    Divide
        //}

        protected static double DoOperation(double x, double y, Operation op)
        {
            double result = op.operationName switch
            {
                '/' => x / y,
                '*' => x * y,
                '+' => x + y,
                '-' => x - y
            };
            return result;
        }

        protected static void ChangeStack(Operation op, StackCalculator calc)
        {
            //Console.WriteLine("Im trying to add operation: {0}, outStackSize: {1}, opStack Size: {2}", op.operationName, outStackSize, operationStackSize);
            //outStackSize++;
            switch (op.operationName)
            {
                case '(':
                    calc.ifOpenScobe(op);
                    break;
                case ')':
                    calc.ifCloseScobe();
                    break;
                case '+':
                    calc.ifBinary(op);
                    break;
                case '-':
                    calc.ifBinary(op);
                    break;
                case '/':
                    calc.ifBinary(op);
                    break;
                case '*':
                    calc.ifBinary(op);
                    break;
            }
        }

        protected virtual void ifOpenScobe(in Operation ch)
        {
            operationStack[operationStackSize] = ch;
            operationStackSize++;
            isNextSign = true;
            isPreviousOperation = false;
        }

        protected virtual void ifCloseScobe()
        {
            outStackSize++;
            while (operationStack[operationStackSize - 1].operationName != '(')
            {
                outStack[outStackSize] += operationStack[operationStackSize - 1].operationName;
                outStackSize++;


                if (operationStackSize - 1 == 0)
                {
                    Console.WriteLine("Wrong formula format!");
                    return;
                }

                operationStackSize--;
            }
            isPreviousOperation = false;
            operationStackSize--;
            isNextSign = false;
        }

        protected virtual void ifBinary(Operation op) // Вот это поебень я сделал
        {
            //ShowOpStack();
            //ShowOutStack();
            if (operationStackSize == 0 || operationStack[operationStackSize - 1].operationPriority == 0)
            {
                operationStack[operationStackSize] = op;
                if (outStackSize != 0 && operationStackSize == 0)
                {
                    outStackSize--;
                }
                outStackSize++;
            }
            else if (operationStack[operationStackSize - 1].operationPriority > op.operationPriority)
            {
                operationStack[operationStackSize] = op;
            }
            else
            {
                operationStackSize--;

                while (operationStack[operationStackSize].operationPriority <= op.operationPriority ||
                    operationStack[operationStackSize].operationPriority != 0)
                {
                    //Console.WriteLine("Im on opStack position: {0}, meanwhile outStackPosition: {1}", operationStackSize, outStackSize);
                    outStackSize++;
                    outStack[outStackSize] += operationStack[operationStackSize].operationName;
                    outStackSize++;

                    if (operationStackSize == 0)
                    {
                        break;
                    }
                    operationStackSize--;
                }

                operationStack[operationStackSize] = op;
            }
            isNextSign = false;
            isPreviousOperation = true;
            operationStackSize++;
            //ShowOpStack();
            //ShowOutStack();
        }


        protected virtual void CreateStack(in string formula, StackCalculator calc)
        {
            operationStack = new Operation[formula.Length];
            outStack = new string[formula.Length];
            operationStackSize = 0;
            outStackSize = 0;
            isNextSign = true;
            isPreviousOperation = false;

            char[] CH = formula.ToCharArray();

            foreach (char ch in CH) //алгоритм преобразованя из инфиксной нотации в обратную польскую запись
            {
                //Console.WriteLine("Trying to Add: {0}", ch);
                int bar;
                if (int.TryParse(ch.ToString(), out bar) || ch == '.' || isNextSign && ch != '(' && ch != ')')
                {
                    outStack[outStackSize] += ch;
                    isNextSign = false;
                    continue;
                }
                ChangeStack(new Operation(ch), calc);
            }
            // перенос оставшегося стека операций в выохдной стек
            //ShowOpStack();
            //ShowOutStack();
            for (int stackNode = operationStackSize; stackNode > 0; --stackNode)
            {
                outStackSize++;
                if (!isPreviousOperation)
                {
                    outStackSize--;
                    isPreviousOperation = true;
                }
                outStack[outStackSize] += operationStack[stackNode - 1].operationName;
            }
            outStackSize++;
            //ShowOpStack();
            //ShowOutStack();
        }

        public static string DoCallculation(string formula)
        {
            StackCalculator calc = new StackCalculator(formula);
            calc.CreateStack(formula, calc);
            int step = 0;
            double firstValue = 0;
            double secondValue = 0;

            while (step + 2 < outStackSize)
            {   
                double bar;
                if (!double.TryParse(outStack[step + 2], out bar))
                {
                    if (double.TryParse(outStack[step], out firstValue)
                        && double.TryParse(outStack[step + 1], out secondValue))
                    {
                        char charOperation = outStack[step + 2].ToCharArray()[0];
                        Operation op = new Operation(charOperation);
                        outStack[step + 2] = Convert.ToString(DoOperation(firstValue, secondValue, op));
                    }
                }
                step++;
            }
            return outStack[step + 1];
        }

        public static void ShowOutStack()
        {
            Console.WriteLine("Out Stack with {0} elemnts: ", outStackSize);
            for (int step = 0; step < outStackSize; step++)
            {
                Console.WriteLine(outStack[step] + " ");
            }
            //Console.WriteLine();
        }

        public static void ShowOpStack()
        {
            Console.WriteLine("Op Stack with {0} elemnts: ", operationStackSize);
            for (int step = 0; step < operationStackSize; step++)
            {
                Console.WriteLine(operationStack[step].operationName + " ");
            }
            //Console.WriteLine();
        }
    }
}
