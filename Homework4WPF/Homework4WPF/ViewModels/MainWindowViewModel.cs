using Calculator.ViewModels.Base;
using Calculator.Infrastructure.Commands;
using System.Windows.Input;
using HomeWork2;
using System;

namespace Calculator.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _inputCommand="";
        private string _result="";
        private bool _isPrevOperation=false;

        private bool IsPrevOperation
        {
            get => _isPrevOperation;
            set => Set(ref _isPrevOperation, value);
        }
        public string Result 
        { 
            get => _result;
            set => Set (ref _result, value); 
        }
        public string InputCommand
        {
            get => _inputCommand;
            set => Set (ref _inputCommand, value);
        }

        #region DoCallculationCommand
        public ICommand DoCallculationCommand { get; }

        private bool CanDoCallculationCommandExecute(object p) => InputCommand is null ? false : true;

        private void OnDoCallculationCommandExecuted(object p)
        {
            StackCalculator calculator = new StackCalculator(InputCommand);
            Result = calculator.DoCallculation(InputCommand);
            InputCommand = Result;
            IsPrevOperation = false;
        }
        #endregion
        #region AddNumberToInputCommand
        public ICommand AddNumberToInputCommand { get; }

        private bool CanAddNumberToInputCommandExecute(object p) => true;

        private void OnAddNumberToInputCommandExecuted(object p)
        {
            if (IsPrevOperation)
            {
                Result ="";
            }

            string buttonPurpose = p.ToString();
            InputCommand += buttonPurpose;
            Result += buttonPurpose;
            IsPrevOperation = false;
        }
        #endregion
        #region AddOperationToInputCommand
        public ICommand AddOperationToInputCommand { get; }

        private bool CanAddOperationToInputCommandExecute(object p) => true;

        private void OnAddOperationToInputCommandExecuted(object p)
        {
            string buttonPurpose = p.ToString();

            if (IsPrevOperation)
            {
                InputCommand.Remove(InputCommand.Length - 1);
            }

            InputCommand+=buttonPurpose;
            IsPrevOperation=true;
        }
        #endregion
        #region PercentCommand
        public ICommand PercentCommand { get; }

        private bool CanPercentCommandExecute(object p) => CanDoCallculationCommandExecute(p);

        private void OnPercentCommandExecuted(object p)
        {
            OnDoCallculationCommandExecuted(p);
            Result = (Convert.ToDouble(Result) / 100.0).ToString();
            InputCommand = Result;
        }
        #endregion
        #region SqrtCommand
        public ICommand AddSqrtCommand { get; }

        private bool CanAddSqrtCommandExecute(object p) => CanAddOperationToInputCommandExecute(p);

        private void OnAddSqrtCommandExecuted(object p)
        {
            OnDoCallculationCommandExecuted(p);
            Result = Math.Sqrt(Convert.ToDouble(Result)).ToString();
            InputCommand = Result;
        }
        #endregion
        #region Reverse
        public ICommand ReverseCommand { get; }

        private bool CanReverseCommandExecute(object p) => CanAddOperationToInputCommandExecute(p);

        private void OnReverseCommandExecuted(object p)
        {
            OnDoCallculationCommandExecuted(p);
            Result = (1.0 / Convert.ToDouble(Result)).ToString();
            InputCommand = Result;
        }
        #endregion
        #region SqrCommand
        public ICommand AddSqrCommand { get; }

        private bool CanAddSqrCommandExecute(object p) => CanAddOperationToInputCommandExecute(p);

        private void OnAddSqrCommandExecuted(object p)
        {
            OnDoCallculationCommandExecuted(p);
            Result = Math.Pow(Convert.ToDouble(Result),2).ToString();
            InputCommand = Result;
        }
        #endregion
        #region CeCommand
        public ICommand CeCommand { get; }

        private bool CanCeCommandExecute(object p) => true;

        private void OnCeCommandExecuted(object p)
        {
            Result = "";
        }
        #endregion
        #region CCommand
        public ICommand CCommand { get; }

        private bool CanCCommandExecute(object p) => true;

        private void OnCCommandExecuted(object p)
        {
            Result = "";
            InputCommand = "";
            IsPrevOperation = false;
        }
        #endregion
        #region DelCommand
        public ICommand DelCommand { get; }

        private bool CanDelCommandExecute(object p) => true;

        private void OnDelCommandExecuted(object p)
        {
            if (Result != "0")
            {
                InputCommand = InputCommand.Remove(InputCommand.Length - 1);
            }

            Result = Result.Length < 2 ? "0" : Result.Remove(Result.Length - 1);            
        }
        #endregion
        public MainWindowViewModel()
        {
            #region Комманды
            DoCallculationCommand = new LambdaCommand(OnDoCallculationCommandExecuted, CanDoCallculationCommandExecute);
            AddNumberToInputCommand = new LambdaCommand(OnAddNumberToInputCommandExecuted,CanAddNumberToInputCommandExecute);
            AddOperationToInputCommand = new LambdaCommand(OnAddOperationToInputCommandExecuted, CanAddOperationToInputCommandExecute);
            PercentCommand = new LambdaCommand(OnPercentCommandExecuted, CanPercentCommandExecute);
            CeCommand = new LambdaCommand(OnCeCommandExecuted, CanCeCommandExecute);
            CCommand = new LambdaCommand(OnCCommandExecuted, CanCCommandExecute);
            DelCommand = new LambdaCommand(OnDelCommandExecuted, CanDelCommandExecute);
            AddSqrtCommand = new LambdaCommand(OnAddSqrtCommandExecuted, CanAddSqrtCommandExecute);
            AddSqrCommand = new LambdaCommand(OnAddSqrCommandExecuted, CanAddSqrCommandExecute);
            ReverseCommand = new LambdaCommand(OnReverseCommandExecuted, CanReverseCommandExecute);
            #endregion
        }


    }
}
