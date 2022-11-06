using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


namespace mathsExerciseGenerator.Services
{

    public class MathsExercise
    {
        public double operand1;
        public double operand2;
        public char operation;
        public double result;
        public double fakeResult1;
        public double fakeResult2;

        public MathsExercise()
        {
            double[] operands = GenerateRandomOperands();
            operand1 = operands[0];
            operand2 = operands[1];
            operation = GenerateRandomOperation();
            result = CalculateRealAnswer();
            fakeResult1 = CalculateFakeAnswer();
            // Ensure you don't get two identical fake answers.
            do
            {
                fakeResult2 = CalculateFakeAnswer();
            } while (fakeResult1 == fakeResult2);
        }

        private double[] GenerateRandomOperands()
        {
            Random rnd = new Random();

            int lowerBound = -99;
            int upperBound = 99;
            
            double[] operands = new double[2];

            // Determine if the exercise will be performed with int or double type (0 means int, 1 means doubles.)
            if (rnd.Next(0,1) == 0)
            {
                operands[0] = rnd.Next(lowerBound, upperBound);
                operands[1] = rnd.Next(lowerBound, upperBound);
            }
            else
            {
                operands[0] = lowerBound + rnd.NextDouble() * (upperBound - lowerBound);
                operands[1] = lowerBound + rnd.NextDouble() * (upperBound - lowerBound);
            }

            return operands;
        }

        private char GenerateRandomOperation()
        {
            Random rnd = new Random();
            int operationSelection = rnd.Next(0, 3);
            char operation;
            switch(operationSelection)
            {
                case 0:
                    operation = '+';
                    break;
                case 1:
                    operation = '-';
                    break;
                case 2:
                    operation = '*';
                    break;
                case 3:
                    operation = '/';
                    break;
                default:
                    throw new Exception("Nice work, Michael - ya done screwed the GenerateRandomOperation() function up.");
            }
            return operation;
        }

        private double CalculateRealAnswer()
        {
            double answer = 0;
            switch (operation)
            {
                case '+':
                    answer = operand1 + operand2;
                    break;
                case '-':
                    answer = operand1 - operand2;
                    break;
                case '*':
                    answer = operand1 * operand2;
                    break;
                case '/':
                    answer = operand1 / operand2;
                    break;
                default:
                    throw new Exception("Nice work, Michael - ya done screwed something up with the operation in the CalculateRealAnswer() function.");
            }
            return answer;
        }

        private double CalculateFakeAnswer()
        {
            Random rnd = new Random();
            int seed = rnd.Next(0, 1);
            double fakeAnswer = 0.0;
            
            // Switch up the operation.
            if (seed == 0)
            {
                char fakeOperation;
                char[] operations = new char[3] { '+', '-', '*' };
                for (int i = 0; i < 3; i++)
                {
                    if (operations[i] == operation && operation != '/') // A little redundant to include the check for the '/' character, but redundancy won't hurt in this situation.
                        operations[i] = '/';
                }
                fakeOperation = operations[rnd.Next(0, 2)];
                switch(fakeOperation)
                {
                    case '+':
                        fakeAnswer = operand1 + operand2;
                        break;
                    case '-':
                        fakeAnswer = operand1 - operand2;
                        break;
                    case '*':
                        fakeAnswer = operand1 * operand2;
                        break;
                    case '/':
                        fakeAnswer = operand1 / operand2;
                        break;
                    default:
                        throw new Exception("Nice work, Michael - ya done screwed something up with the operation in the CalculateRealAnswer() function.");
                }
            }
            // Switch up one of the operands.
            else if (seed == 1)
            {
                int operandToModify = rnd.Next(0, 1);
                double localOperand1 = operand1;
                double localOperand2 = operand2;

                // Modify operand 1
                if (operandToModify == 0)
                    // We don't want to modify the fake operand by 0 and return that (since that will give the actual answer), so keep modifying until we get a different operand than the original.
                    while (localOperand1 == operand1)
                        localOperand1 += rnd.Next(-3, 3);
                // Modify operand 2
                else
                    // We don't want to modify the fake operand by 0 and return that (since that will give the actual answer), so keep modifying until we get a different operand than the original.
                    while (localOperand2 == operand2)
                        localOperand2 += rnd.Next(-3, 3);
                // Calculate the result of the operation with the modified operand.
                switch(operation)
                {
                    case '+':
                        fakeAnswer = localOperand1 + localOperand2;
                        break;
                    case '-':
                        fakeAnswer = localOperand1 - localOperand1;
                        break;
                    case '*':
                        fakeAnswer = localOperand1 * localOperand2;
                        break;
                    case '/':
                        fakeAnswer = localOperand1 / localOperand2;
                        break;
                    default:
                        throw new Exception("Nice work, Michael - ya done screwed something up with the operator in the CalculateFakeAnswer() function...");
                }
            }
            // Generate a random number that isn't equal to the actual result.
            else
            {
                do
                {
                    fakeAnswer = GenerateRandomOperands()[0];
                } while (fakeAnswer == result);
            }
            return fakeAnswer;
        }
        
        
    }

    public class MathsExerciseGenerator
    {
        public MathsExerciseGenerator() {}

        public string GetMathsExercisesJsonString()
        {
            MathsExercise[] exercises = new MathsExercise[10];

            for (int i = 0; i < exercises.Length; i++)
                exercises[i] = new MathsExercise();

            var jsonString = JsonConvert.SerializeObject(exercises, Formatting.Indented);
            // MathsExercise[] exercises = JsonConvert.DeserializeObject<MathsExercise[]>(jsonString);
            return jsonString;
        }
    }
}
