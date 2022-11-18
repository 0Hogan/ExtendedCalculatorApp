using Calculator.ViewModel;

namespace Calculator;

public partial class MainPage : ContentPage
{
    public MainPage(HistoryViewModel Historyviewmodel)
    {
        InitializeComponent();
        OnClear(this, null);
        inputString = "0";
        infixExpression = "0";
        onNumberInputClearResult = true;
        BindingContext = Historyviewmodel;
    }

    bool onNumberInputClearResult = true; // Used to determine when to keep the result of the immediately previous operation to use it in the current operation.
    string inputString = "0";        // This will be the expression that we show back to the user.
    string infixExpression = "0";    // This will be the expression upon which we perform the calculations.
    System.Collections.Generic.Stack<bool> parenthesesIndicatesSquareRoot = new Stack<bool>(); // A stack to indicate whether the closing parentheses of a pair of parentheses should be followed by an exponent call (This is really only used for the "square root" operation.

    void OnSelectNumber(object sender, EventArgs e)
    {
        if (onNumberInputClearResult)
        {
            this.resultText.Text = "";
            infixExpression = "";
            inputString = "";
        }
        onNumberInputClearResult = false;
        Button button = (Button)sender;
        string pressed = button.Text;

        infixExpression += pressed;
        inputString += pressed;

        this.resultText.Text += pressed;
        this.CurrentCalculation.Text = infixExpression;

    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        onNumberInputClearResult = false;

        Button button = (Button)sender;
        string pressed = button.Text;
        switch (pressed)
        {
            case "%": // Percent sign can be rewritten for the infixExpression as "/100" or "x.01".
                infixExpression += "/100";
                break;
            case "mod(": // Modulo expression gets added to the infixExpression as a percent sign.
                infixExpression += "%(";
                parenthesesIndicatesSquareRoot.Push(false);
                break;
            case "sqrt(": // Square root begins by pushing "(", but will add "^(1/2)" right after the corresponding closing parentheses.
                infixExpression += "(";
                parenthesesIndicatesSquareRoot.Push(true);
                break;
            case "(":    // Normal reaction, but pushes false to the stack to indicate that it's corresponding closing parentheses should NOT be followed by a "^(1/2)"
                infixExpression += pressed;
                parenthesesIndicatesSquareRoot.Push(false);
                break;
            case ")":
                if (parenthesesIndicatesSquareRoot.Count <= 0) // If the parentheses stack is empty, there is no corresponding "(", which means the function should simply return without adding the ")" to either infixExpression or the inputString.
                    return;
                infixExpression += ")";
                if (parenthesesIndicatesSquareRoot.Pop())
                    infixExpression += "^(1/2)";
                break;
            case "×": // Multiplication sign looks like an 'x', but it's actually a funky character.
                infixExpression += "x";
                break;
            case "÷": // Division sign is a funky character.
                infixExpression += "/";
                break;
            default:
                infixExpression += pressed;
                break;
        }
        
        inputString += pressed;
        this.resultText.Text += pressed;
        this.CurrentCalculation.Text = infixExpression;

    }

    void OnClear(object sender, EventArgs e)
    {
        infixExpression = "0";
        inputString = "0";
        this.CurrentCalculation.Text = "0";
        this.resultText.Text = "0";
        onNumberInputClearResult = true;
        this.CurrentCalculation.Text = infixExpression;
    }

    void OnCalculate(object sender, EventArgs e)
    {
        string result = Calculator.EvaluatePrefixExpression(Calculator.InfixToPrefix(infixExpression)); // Evaluate the given expression and store the result as a string inside result.
        this.CurrentCalculation.Text = inputString; // Show the original inputted expression
        this.resultText.Text = result; // Show the result of the operation.
        onNumberInputClearResult = true; // Ensure that typing in a new number will erase the current result while typing in an operator will use the current result.
        if (result[0] == '-')
            infixExpression = "(0" + result + ")";
        else
            infixExpression = result; // Store the current result in infixExpression (in case the user wants to build on the result).
        inputString = result;   // Store the current result in inputString (in case the user wants to build on the result).
        
    }    

    void OnNegative(object sender, EventArgs e)
    {
        if (onNumberInputClearResult)
        {
            this.resultText.Text = "";
            infixExpression = "";
            inputString = "";
            onNumberInputClearResult = false;
        }

        if (infixExpression.Length == 0 || Calculator.isAnOperator(Convert.ToString(infixExpression[infixExpression.Length - 1])))
        {
            infixExpression += "(0-1)*";
            inputString += "-";
            this.resultText.Text += "-";
        }
        else
        {
            infixExpression += "*(0-1)";
            inputString += "×(-1)";
            this.resultText.Text += "×(-1)";
        }
        this.CurrentCalculation.Text = infixExpression;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    void OnPercentage(object sender, EventArgs e)
    {
        inputString += '%';
        infixExpression += "/100";
        this.resultText.Text += "%";
        this.CurrentCalculation.Text = infixExpression;
    }


}
