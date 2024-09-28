using System;

namespace ConsoleCalculator
{
    public interface IAddition
    {
        double Add(double a, double b);
    }

    public interface ILogger
    {
        void Log(string message);
        void LogError(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[LOG] {message}");
            Console.ResetColor();
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}");
            Console.ResetColor();
        }
    }

    public class Calculator : IAddition
    {
        private readonly ILogger _logger;

        public Calculator(ILogger logger)
        {
            _logger = logger;
        }

        public double Add(double a, double b)
        {
            double result = a + b;
            _logger.Log($"Выполнено сложение: {a} + {b} = {result}");
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();

            IAddition calculator = new Calculator(logger);

            try
            {
                Console.Write("Введите первое число: ");
                double num1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Введите второе число: ");
                double num2 = Convert.ToDouble(Console.ReadLine());

                double result = calculator.Add(num1, num2);

                Console.WriteLine($"Результат: {num1} + {num2} = {result}");
            }
            catch (FormatException)
            {
                logger.LogError("Ошибка: Введено некорректное значение.");
                Console.WriteLine("Ошибка: Введено некорректное значение. Пожалуйста, введите числа.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Неожиданная ошибка: {ex.Message}");
                Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            }
            finally
            {
                logger.Log("Работа программы завершена.");
                Console.WriteLine("Работа программы завершена.");
            }
        }
    }
}
