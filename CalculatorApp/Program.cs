namespace CalculatorApp
{
    internal abstract class Program
    {
        private static void Main(string[] args)
        {
            string[] possibleOperationSigns = ["+", "-", "*", "x", "/"];
            Console.Write("Enter the first number of the operation: ");
            var firstNumber = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Enter Operation sign (+, -, * or x, /): ");
            var operationSign = Console.ReadLine();
            
            Console.Write("Enter the second number of the operation: ");
            var secondNumber = Convert.ToDouble((Console.ReadLine()));
            
            if (!possibleOperationSigns.Contains(operationSign) || operationSign == null)
            {
                Console.WriteLine("Invalid Operation Sign");
                return;
            }

            var result = Calculate(firstNumber, operationSign, secondNumber);
            Console.WriteLine();
            Console.WriteLine($"Result of the operation {firstNumber} {operationSign} {secondNumber} is: {result}");

        }

        private static double Calculate(double a, string sign, double b)
        {
            double result = 0;
            switch (sign)
            {
                case "+":
                    result = SumTwoNumbers(a, b);
                    break;
                case "-":
                    result = SubtractTwoNumbers(a, b);
                    break;
                case "*" or "x":
                    result = MultiplyTwoNumber(a, b);
                    break;
                case "/":
                    result = DivideTwoNumbers(a, b);
                    break;
                default:
                    Console.WriteLine("Invalid Operator");
                    break;
            }
            return result;
        }

        private static double SumTwoNumbers(double a, double b)
        {
            return a + b;
        }
        private static double SubtractTwoNumbers(double a, double b)
        {
            return a - b;
        }

        private static double MultiplyTwoNumber(double a, double b)
        {
            return a * b;
        }

        private static double DivideTwoNumbers(double a, double b)
        {
            return a / b;
        }
    }
};