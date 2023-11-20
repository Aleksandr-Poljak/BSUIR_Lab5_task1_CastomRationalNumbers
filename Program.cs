using BSUIR_Lab_5_task1;
using System.Transactions;
// Объявление и приведение типов.
Rational numR1 = new Rational(1, 2);
Rational numR2 = new Rational(5, 3);
Rational numR3 = new Rational(-2,4);
numR3.Noramlize(); 
// Нормализация дроби не автоматическая. Необходимо вызывать метод вручную, если нужно.Метод сокращает сам объект и возвращает ссылку на себя
Rational numR4 = 3.14; 
numR4.Noramlize();
Rational numR5 = 4;

double num1D = (double)new Rational(1, 2);
double num2D = (double)new Rational(-3, 4);
double num3D = 0.1;

int num1I = 1;
int num2I = (int) new Rational(4, 2);
Console.WriteLine($"numR1 = {numR1}\nnumR2 = {numR2}\nnumR3 = {numR3}\nnumR4 = {numR4}\nnumR5 = {numR5}\n" +
    $"num1D = {num1D}\nnum2D = {num2D}\nnum3D = {num3D}\nnum1I = {num1I}\nnum2I = {num2I}");

// Демонстрация перегруженых мат опреаций.
Console.WriteLine($"{numR1} + {numR2} = {numR1 + numR2}");
Console.WriteLine($"{numR2} + {4} = {numR2 + 4}");
Console.WriteLine($"{numR3} + {3.5} = {(numR3 + 3.5).Noramlize()}"); 

Console.WriteLine($"{numR3} - {numR2} = {numR3 - numR2}");
Console.WriteLine($"{numR5} - {3} = { numR5 - 3}"); 
Console.WriteLine($"{numR5} - {3.1} = {numR5 - 3.1}");

Console.WriteLine($"{numR1} * {numR5} = {(numR1 * numR5).Noramlize()} ");
Console.WriteLine($"{numR1} * {2} = {(numR1 * 2).Noramlize()}");
Console.WriteLine($"{numR2} * {2.1} = {(numR2 * 2.12).Noramlize()} ");

Console.WriteLine($"{numR3} / {numR1} = {numR3 / numR1}");
Console.WriteLine($"{numR5} / {2} = {(numR5 / 2).Noramlize()} ");
Console.WriteLine($"{numR5} / {1.5} = {numR5 / 1.5}");

Console.WriteLine($"{numR2} > {numR1} {numR2 > numR1}");
Console.WriteLine($"{numR2} <{numR1} {numR2 < numR1}");
Console.WriteLine($"{numR2} == {numR1} {numR2 == numR1}");
Console.WriteLine($"{numR2} != {numR1} {numR2 != numR1}");
Console.WriteLine($"{numR1} {(numR1 ? true : false)}");
Rational numFalse = new Rational(0, 5);
Console.WriteLine($"{numFalse} {(numFalse ? true : false)}");
numR5++;
numR5.Noramlize();
Console.WriteLine($"{numR5} ++ = {numR5} = {(double) numR5}"); // Инкремент добовляет к дроби 1% от дроби 
numR5--;
numR5.Noramlize(); 
Console.WriteLine($"{numR5} -- = {numR5} ~= {(double)numR5}"); // Дикримент отнимает от  дроби 1% дроби
Console.WriteLine($"numR5['N'] = {numR5['N']}");
Console.WriteLine($"numR5['N'] = {numR5['D']}");
Console.WriteLine($"Hash {numR5.GetHashCode()}");
Console.WriteLine($"numR5.Equals(numR4) {numR5.Equals(numR4)}");

