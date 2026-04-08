namespace CalculatorApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Enter the first number of the operation: ");
            if (!double.TryParse(Console.ReadLine(), out var firstNumber))
            {
                Console.WriteLine("Invalid Number!");
                return;
            }
            
            Console.Write("Enter Operation sign (+, -, * or x, /): ");
            var operationSign = Console.ReadLine();
            
            Console.Write("Enter the second number of the operation: ");
            if (!double.TryParse(Console.ReadLine(), out var secondNumber))
            {
                Console.WriteLine("Invalid Number!");
                return;
            }
            
            string[] possibleOperationSigns = ["+", "-", "*", "x", "/"];
            if (operationSign == null || !possibleOperationSigns.Contains(operationSign))
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
                    result = MultiplyTwoNumbers(a, b);
                    break;
                case "/":
                    if (b == 0)
                    {
                        Console.WriteLine("Cannot divide by 0");
                        break;
                    }
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

        private static double MultiplyTwoNumbers(double a, double b)
        {
            return a * b;
        }

        private static double DivideTwoNumbers(double a, double b)
        {
            return a / b;
        }
    }
};