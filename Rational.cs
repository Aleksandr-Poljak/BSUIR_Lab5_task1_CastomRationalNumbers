using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR_Lab_5_task1
{
    /// <summary>
    ///  Rational numbers class
    /// </summary>
    internal class Rational
    {
        private int numerator = 1;
        private int denominator = 1;
        public int Numerator
        {
            get { return numerator; }
            set => numerator = value;

        }
        public int Denominator
        {
            get { return denominator; }
            set
            {
                if(value >= 1)
                {
                    denominator = value;
                }
                else
                {
                    throw new ArgumentException("Знаменатель должен быть больше или равен 1.");
                }
            }
        }

        public Rational()
        {
            Numerator = 1;
            Denominator = 1;
        }
        public Rational(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        protected static int GetNODTwoNumber(int num1, int num2)
        {
            // Поиск НОД для двух целых чисел через алгоритм Евликида и рекурсию.
            int num1Abs = Math.Abs(num1);
            int num2Abs = Math.Abs(num2);

            if (num1Abs == num2Abs)
            {
                return num1Abs;
            }

            int numMax, numMin, result;
            if (num1Abs > num2Abs)
            {
                numMax = num1Abs;
                numMin = num2Abs;
            }
            else
            {
                numMax = num2Abs;
                numMin = num1Abs;
            }
            result = numMax % numMin;
            if (result == 0)
            {
                return numMin;
            }
            else
            {
                return GetNODTwoNumber(numMin, result);
            }
        }
        protected static int GetNOKTwoNumber(int num1, int num2)
        {
            // Поиск НОК двуч чисел через НОД.
            return num1 * num2 / GetNODTwoNumber (num1, num2);
        }
        public int GetNOD()
        {
            // Получить НОД для числителя и знаменателя.
            return GetNODTwoNumber(numerator, denominator);
        }
        /// <summary>
        /// Раскладывает  дробное число  на: знак(1- минус или 0 - плюс), целую часть, числитель(целое число) ,знаменатель(целое число)
        /// методом деления и приведения типов.
        /// </summary>
        protected static void DecomposeDouble(double num, out int sign, out int wholePart, out int numeratorPart, out int denominatorPart)
        {
            const double eps = 0.000001;
           
            double tempNum = Math.Abs(num);
            wholePart = (int)tempNum; // Возврщаем целую часть
            sign = num > eps ? 0 : 1; // возвращаем знак.

            int fractionalDigitCount = 0; // количество цифр в дробной части.
            while (tempNum % 10 > eps)
            {
                tempNum *= 10;
                fractionalDigitCount++;
            }
            fractionalDigitCount--;

            // Получаем числитель в виде целого числа 
            int nP = (int)(Math.Round(Math.Abs(num) - wholePart, fractionalDigitCount) * (int)Math.Pow(10, fractionalDigitCount));
            numeratorPart = nP!=0 ? nP : 1;
            // Знаменатель в виде целого числа
            denominatorPart = (int)Math.Pow(10, fractionalDigitCount);
            
        }
        /// <summary>
        /// Раскладывает  дробное число  на:знак(1- минус или 0 - плюс), целую часть, числитель(целое число) ,знаменатель(целое число)
        /// методом парсинга строки.
        /// </summary>
        protected static void DecomposeDoubleParse(double num, out int sign, out int wholePart, out int numeratorPart, out int denominatorPart)
        {
            string tempNum = num.ToString("G", CultureInfo.InvariantCulture);
            string[] intAndFracPart; // Массив с целой и дробной частью

            //Возвращаем знак и удаляем его.
            if (tempNum.Contains('-') && tempNum.IndexOf('-') == 0)
            {
                sign = 1;
                tempNum = tempNum.Remove(0,1);
            }
            else
            {
                sign = 0;
            }
            //Удяляем нули в конце дробной части
            while (tempNum.EndsWith("0"))
            {
                tempNum = tempNum.Remove(tempNum.Length - 1);
            }

            intAndFracPart = tempNum.Split('.');

            wholePart = Convert.ToInt32(intAndFracPart[0]); // Возврщаем целую часть
            numeratorPart = intAndFracPart.Length >1? Convert.ToInt32(intAndFracPart[1]):1; // Получаем числитель в виде целого числа 
            denominatorPart = (int)Math.Pow(10, intAndFracPart.Length > 1 ? intAndFracPart[1].Length: 1); // Знаменатель в виде целого числа
        }

        public Rational Noramlize()
        {
            int nod = this.GetNOD();
            numerator /= nod;
            denominator /= nod;
            return this;
        }

        public static implicit operator Rational(int num)
        {
            return new Rational(num, 1);
        }
        public static explicit operator int (Rational num)
        {
            return num.Numerator / num.Denominator;
        }
        public static implicit operator Rational(double num)
        {
            int sign, wholePart, numeratorPart, denominatorPart;
            try
            {
                Rational.DecomposeDouble(num, out sign, out wholePart, out numeratorPart, out denominatorPart);
            }
            catch (Exception)
            {
                Rational.DecomposeDoubleParse(num, out sign, out wholePart, out numeratorPart, out denominatorPart);
            }

            if (wholePart != 0)
            {
                numeratorPart = wholePart * denominatorPart + numeratorPart;
            }
            if (sign == 1)
            {
                numeratorPart *= -1;
            }
            //Console.WriteLine($"sing {sign}, whole {wholePart}, numerator {numeratorPart}, denomonator {denominatorPart}");
            Rational obj = new Rational(numeratorPart, denominatorPart);
            return obj;
        }
        public static explicit operator double(Rational num)
        {
            return (double) num.numerator /(double)num.denominator;
        }
        public static Rational operator + (Rational num1, Rational num2)
        {
            int newDenominator = Rational.GetNOKTwoNumber(num1.Denominator, num2.Denominator);
            int newNumerator = ((newDenominator / num1.Denominator) * num1.Numerator) + ((newDenominator / num2.Denominator) * num2.Numerator);
            return new Rational(newNumerator, newDenominator);
        }
        public static Rational operator + (Rational num1, int num2)
        {
            Rational tempNum2 = num2;
            return num1 + tempNum2;
        }
        public static Rational operator + (Rational num1, double num2)
        {
            Rational tempNum2 = num2;
            return tempNum2 + num1;
        }
        public static Rational operator - (Rational num1, Rational num2)
        {
            int newDenominator = Rational.GetNOKTwoNumber(num1.Denominator, num2.Denominator);
            int newNumerator = ((newDenominator / num1.Denominator) * num1.Numerator) - ((newDenominator / num2.Denominator) * num2.Numerator);
            return new Rational(newNumerator, newDenominator);
        }
        public static Rational operator - (Rational num1, int num2)
        {
            Rational tempNum2 = num2;
            return num1 - tempNum2;
        }
        public static Rational operator - (Rational num1, double num2)
        {
            Rational tempNum2 = num2;
            return num1 - tempNum2;
        }
        public static Rational operator * (Rational num1, Rational num2)
        {
            int newNumerator = num1.Numerator * num2.Numerator;
            int newDenominator = num1.Denominator * num2.Denominator;
            return new Rational(newNumerator, newDenominator);
        }
        public static Rational operator *(Rational num1, int num2)
        {
            return new Rational(num1.Numerator * num2, num1.Denominator * 1);
        }
        public static Rational operator *(Rational num1, double num2) 
        {
            Rational tempNum2 = num2;
            return num1 * tempNum2; 
        }
        public static Rational operator / (Rational num1, Rational num2)
        {
            Rational tempNum2;
            int tempNum2NewNumerator = Math.Abs(num2.Denominator);
            int tempNum2NewDenominator = Math.Abs(num2.Numerator);
            if (num2.Numerator < 0)
            {
                tempNum2NewNumerator *= -1;

            }
            tempNum2 = new Rational(tempNum2NewNumerator, tempNum2NewDenominator);
            return num1 * tempNum2;
        }
        public static Rational operator / (Rational num1, int num2)
        {
            return num1/ (Rational)num2;
        }
        public static Rational operator /(Rational num1, double num2)
        {
            return num1 / (Rational)num2;
        }
        public static bool operator true (Rational num1)
        {
            return num1.Numerator != 0;
        }
        public static bool operator false(Rational num1)
        {
            return num1.Numerator != 0;
        }
        public int this[char ind]
        {
            get
            {
                if (ind == 'N' || ind == 'D')
                {
                    if (ind == 'N')
                    {
                        return Numerator;
                    }
                    else
                    {
                        return Denominator;
                    }

                }
                else
                {
                    throw new IndexOutOfRangeException("N - значение числителя, D- знаменятеля.");
                }
            }
            set
            {
                if (ind == 'N' || ind == 'D')
                {
                    if(ind == 'N')
                    {
                        Numerator = value;
                    }
                    else
                    {
                        Denominator = value;
                    }
                    
                }
                else
                {
                    throw new IndexOutOfRangeException("N - значение числителя, D- знаменятеля.");
                }
            }
        }
        public static bool operator == (Rational num1, Rational num2)
        {
            return (num1.Numerator == num2.Numerator) && (num1.Denominator==num2.Denominator);
        }
        public static bool operator != (Rational num1, Rational num2)
        {
            return (num1.Numerator == num2.Numerator) && (num1.Denominator == num2.Denominator);
        }
        public static Rational operator ++ (Rational num1)
        {
            // Увеличивает число на 1%.         
            Rational addValue = (num1 / 100) * 1;

            return num1 + addValue;
        }
        public static Rational operator -- (Rational num1)
        {
            // Уменшяет число на 1%.         
            Rational addValue = (num1 / 100) * 1;

            return num1 - addValue;
        }
        public static bool operator > (Rational num1, Rational num2)
        {
            if (num1.Denominator == num2.Denominator)
            {
                return num1.Numerator > num2.Numerator;
            }
            else
            {
                int commonDenomenator = GetNOKTwoNumber(num1.Denominator, num2.Denominator);
                Rational tempNum1 = new Rational(commonDenomenator / num1.Denominator * num1.Numerator, commonDenomenator);
                Rational tempNum2 = new Rational(commonDenomenator / num2.Denominator * num2.Numerator, commonDenomenator);
                return tempNum1 > tempNum2;
            }
        }
        public static bool operator < (Rational num1, Rational num2)
        {
            if (num1.Denominator == num2.Denominator)
            {
                return num1.Numerator < num2.Numerator;
            }
            else
            {
                int commonDenomenator = GetNOKTwoNumber(num1.Denominator, num2.Denominator);
                Rational tempNum1 = new Rational(commonDenomenator / num1.Denominator * num1.Numerator, commonDenomenator);
                Rational tempNum2 = new Rational(commonDenomenator / num2.Denominator * num2.Numerator, commonDenomenator);
                return tempNum1 < tempNum2;
            }
        }
        public override bool Equals(Object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Rational num = (Rational)obj;
                return this == num;
            }
        }
        public override int GetHashCode()
        {
            int tempNumerator = Numerator << 2;
            int tempDenumerator = Denominator << 2;

            return (int)(Math.Pow(tempNumerator, tempDenumerator)) ^ tempDenumerator ;
        }
        public override string ToString()
        {
            return $"{numerator}/{denominator}";
        }
    }   
}   
