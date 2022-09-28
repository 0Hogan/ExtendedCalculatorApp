namespace Calculator;

public static class Calculator
{
    // Returns the result of an operation between two values.
    public static double Calculate(double value1, double value2, string mathOperator)
    {
        double result = 0;

        switch (mathOperator)
        {
            case "^": // Exponent
                result = value1;
                for (int i = 1; i < value2; i++)
                    result *= value1;
                break;
            case "%": // Modulo
                result = value1 % value2;
                break;
            case "÷": // Division
                result = value1 / value2;
                break;
            case "×": // Multiplication
                result = value1 * value2;
                break;
            case "+": // Addition
                result = value1 + value2;
                break;
            case "-": // Subtraction
                result = value1 - value2;
                break;
        }

        return result;
    }

    // Takes in a mathematical prefix expression as a string and returns the result. STILL NEEDS TO BE DEFINED.
    public static double EvaluatePrefixExpression(string prefix)
    {
        double result = 0.0;

        int numberOfOperations = 0;

        for (int i = 0; i < prefix.Length; i++)
        {
            if (getPrecedence(prefix[i]) > 0)
                numberOfOperations++;
        }    

        // Split the prefix string into individual operators/operands.
        string[] splitPrefix = prefix.Split('#');
        int splitPrefixSize = splitPrefix.Length;

        for (int i = 0; i < numberOfOperations; i++)
        {
            int operatorIndex = 0;
            int operand1Index = 1;
            int operand2Index = 2;
            
            // Slide over the array until we find an operator immediately followed by two numbers. 
            while (!isAnOperator(splitPrefix[operatorIndex]) && !isNumeric(splitPrefix[operand1Index]) && !isNumeric(splitPrefix[operand2Index]))
            {
                operatorIndex++;
                operand1Index++;
                operand2Index++;
                if (operand2Index > splitPrefixSize)
                    throw new Exception("Crap. Uh, 42?");
            }
            // Calculate the result of the operation.
            result = Calculate(Convert.ToDouble(splitPrefix[operand1Index]), Convert.ToDouble(splitPrefix[operand2Index]), splitPrefix[operatorIndex]);

            // Store the result back in the array of strings.
            splitPrefix[operatorIndex] = Convert.ToString(result);

            // Shrink the array and get rid of the numbers already used. (splitPrefixSize uses the operator and the two operands, but keeps the result of the operation for a net loss of 2 elements).
            splitPrefixSize -= 2;
            for (int j = operand2Index; j < splitPrefixSize; j++)
            {
                splitPrefix[j - 1] = splitPrefix[j];
            }
        }

        return result;
    }

    // Takes in an infix expression and converts it into a prefix expression (Thereby eliminating any parentheses from the expression). NOTE: DOES NOT CURRENTLY SUPPORT 5(4+3) OR SUCH EXPRESSIONS. MUST BE EXPLICIT LIKE 5*(4+3).
    public static string InfixToPrefix(string infix)
    {
        string prefix = "";
        string reversedInfix = reverseString(infix);

        System.Collections.Generic.Stack<char> tokens = new Stack<char>();

        // Scan the reversed infix expression.
        for (int i = 0; i < reversedInfix.Length; i++)
        {
            // This case is a little weird. Since reversedInfix is, well, reversed, '(' is now the closing parentheses and '(' is now the opening parentheses. Since this is a closing parentheses, we want to pop operators off the top of the token stack and into the prefix string until we come across the opening parentheses (')').
            if (reversedInfix[i] == '(')
            {
                while (tokens.Count > 0 && tokens.Peek() != ')')
                {
                    prefix += tokens.Pop();
                }
                tokens.Pop(); // Get rid of the opening parentheses.

                continue;
            }

            // Again, this case is a little weird for the same reason as the above.
            if (reversedInfix[i] == ')')
            {
                tokens.Push(reversedInfix[i]);
                continue;
            }

            // Don't add a delimiter to the beginning or end of the expression.
            if (i != 0 && i + 1 != reversedInfix.Length)
            {
                // If the current character is not a number (or decimal point), but the last character added to the prefix string is a number, add ',' to serve as a delimiter between numbers.
                if (!isNumeric(reversedInfix[i]) && isNumeric(prefix[prefix.Length - 1]))
                    prefix += '#';
                // Otherwise, if the last character added to the prefix string is neither '#' nor a number, add '#' as a delimiter between special characters.
                else if ((prefix[prefix.Length - 1] != '#') && (!isNumeric(prefix[prefix.Length - 1])))
                    prefix += '#';
            }

            // If it's a number, push it to the top of the stack.
            if (isNumeric(reversedInfix[i]))
            {
                prefix += reversedInfix[i];
                continue;
            }

            int operatorPrecedence = getPrecedence(reversedInfix[i]);
            
            // If the code makes it to this point, we should be dealing with an operator. If it's not a valid operator, throw an error.
            if ( operatorPrecedence < 0)
                throw new Exception("Unrecognized operator.");

            // If it's an operator with equal or higher precedence than the last operator pushed to the tokens stack (or if the stack is empty), push it to the top of the stack.
            if ((tokens.Count == 0 ) || (operatorPrecedence >= getPrecedence(tokens.Peek())))
            {
                tokens.Push(reversedInfix[i]); // Push the operator to the top of the token stack.
                continue;
            }

            // If the operator on the top of the tokens stack has a lower priority than the current operator being examined, move the operators from the top of the stack to the prefix string until an operator with equal or higher precedence is found (or the bottom of the stack is encountered). Then push the current operator to the stack.
            while (tokens.Count > 0 && (operatorPrecedence < getPrecedence(tokens.Peek())))
            {
                prefix += tokens.Pop(); // Pop an operator off the top of the stack and add it to the back of the prefix string.

                if (tokens.Count > 0 && (operatorPrecedence >= getPrecedence(tokens.Peek())))
                    tokens.Push(reversedInfix[i]);
            }
        }

        // If there are any operators remaining on the tokens stack, pop them off and add them to the prefix string now.
        while (tokens.Count > 0)
            prefix += tokens.Pop();

        // Reverse the prefix string to get the actual prefix notation.
        prefix = reverseString(prefix);

        return prefix;
    }

    // Takes in a string and returns the reverse of it.
    private static string reverseString (string s)
    {
        char[] chars = s.ToCharArray(); // Copy the string to an array of characters.
        Array.Reverse(chars); // Reverse the array.
        return new string(chars); // Return the character array as a string.
    }

    // Returns true if the inputted character is a number (or a decimal point) and false otherwise.
    private static bool isNumeric(char c)
    {
        if (c >= '0' && c <= '9') // If it's a number, return true.
            return true;
        if (c == '.') // Obviously not a number, but is a decimal point (and therefore part of a number), so return true.
            return true;
        return false; // If something else, c is not a number.
    }

    // Returns true if the inputted string is a number (or a decimal point) and false otherwise.
    private static bool isNumeric(string s)
    {
        if (s[0] >= '0' && s[0] <= '9')
            return true;
        if (s[0] == '.')
            return true;
        return false;
    }

    private static bool isAnOperator(string s)
    {
        if (getPrecedence(s[0]) >= 0)
            return true;
        else
            return false;
    }

    // Returns a number indicating the precedence of the operator (where higher number = higher precedence), or -1 if not an operator.
    private static int getPrecedence(char c)
    {
        switch (c)
        {
            case '(':   // Parentheses
                return 4;
            case ')':   // Parentheses
                return 4;
            case '^':   // Exponent
                return 3;
            case 'x':   // Multiplication
                return 2;
            case '/':   // Division
                return 2;
            case '%':   // Modulo
                return 2;
            case '+':   // Addition
                return 1;
            case '-':   // Subtraction
                return 1;
            default:
                return -1; // Not an operator.
        }
    }
}

public static class DoubleExtensions
{
    public static string ToTrimmedString(this double target, string decimalFormat)
    {
        string strValue = target.ToString(decimalFormat); //Get the stock string

        //If there is a decimal point present
        if (strValue.Contains("."))
        {
            //Remove all trailing zeros
            strValue = strValue.TrimEnd('0');

            //If all we are left with is a decimal point
            if (strValue.EndsWith(".")) //then remove it
                strValue = strValue.TrimEnd('.');
        }

        return strValue;
    }
}
