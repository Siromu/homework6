using System;

// Описываем делегат. В делегате описывается сигнатура методов, на
// которые он сможет ссылаться в дальнейшем (хранить в себе)
public delegate double Fun(double x, double y);
class Program
{
    // Создаем метод, который принимает делегат
    // На практике этот метод сможет принимать любой метод
    // с такой же сигнатурой, как у делегата
    public static void Table(Fun F, double x, double y, double b)
    {
        Console.WriteLine("----- X ----- Y -----");
        while (x <= b && y <= b)
        {
            Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x, y));
            x += 1;
            y += 1;
        }
        Console.WriteLine("---------------------");
    }
    // Создаем метод для передачи его в качестве параметра в Table
    public static double MyFunc(double x, double y)
    {
        return y * Math.Pow(x, 2);
    }

    public static double MyFunc2(double x, double y)
    {
        return y * Math.Sin(x);
    }

    static void Main()
    {
        // Создаем новый делегат и передаем ссылку на него в метод Table
        Console.WriteLine("Таблица функции MyFunc:");
        // Параметры метода и тип возвращаемого значения, должны совпадать с делегатом
        Table(new Fun(MyFunc), -2, 2, 100);

        Console.WriteLine("Еще раз та же таблица, но вызов организован по новому");
        // Упрощение(c C# 2.0).Делегат создается автоматически.
        Table(MyFunc, -2, 2, 100);

        Console.WriteLine("Таблица функции MyFunc2:");
        Table(MyFunc2, 2, -2, 100); // Можно передавать уже созданные методы
        Console.WriteLine("Еще раз та же таблица, но вызов организован по новому");
        // Упрощение(c C# 2.0).Делегат создается автоматически.
        Table(MyFunc2, -2, 2, 100);

        Console.WriteLine("Таблица функции y*x^2:");
        // Упрощение(с C# 2.0). Использование анонимного метода
        Table(delegate (double x, double y) { return y * Math.Pow(x, 2); }, 0, 3, 100);
        Console.WriteLine("Таблица функции y*sin(x)");
        // Упрощение(с C# 2.0). Использование анонимного метода
        Table(delegate (double x, double y) { return y * Math.Sin(x); }, 0, 3, 100);
    }
}