using System;

namespace Pract_Task_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Задайте булеву функцию векторно (без пробелов): ");
            string bulFunc = Console.ReadLine();
            bool inputOk;
            if (TwoStep(bulFunc.Length) == 0) inputOk = false;
            else inputOk = true;
            foreach (char x in bulFunc) if (!(x == '0' || x == '1'||x=='*')) inputOk = false;


            if (!inputOk) Console.WriteLine("Ошибка! Введенные данные не являются векторным представлением булевой функции!");
            else
            {
                Console.WriteLine("Среди возможных определений булевой функции линейными являются:");
                Console.WriteLine(AllLinDefs(bulFunc));
            }
         
        }

        static string AllLinDefs(string baseFunc)
        {
            string resMsg = "";
            char[] tekDef = baseFunc.ToCharArray();
            string replace = "";
            foreach (char x in baseFunc) if (x == '*') replace += '0';
           //Для всех комбинаций до 111..11
            do
            {
                int j = 0;
                for (int i = 0; i < tekDef.Length; i++) if (tekDef[i] == '*')
                    {
                        tekDef[i] = replace[j];
                        j++;
                    }
                string tekVect = "";
                foreach (char x in tekDef) tekVect += x;
                if (PascMethod( tekVect)) resMsg += tekVect + "; ";
                tekDef = baseFunc.ToCharArray();
                replace = PlusOne(replace);
            } while (!EveryElemIsOne(replace));
           //Для последней комбинации
            {
                int j = 0;
                for (int i = 0; i < tekDef.Length; i++) if (tekDef[i] == '*')
                    {
                        tekDef[i] = replace[j];
                        j++;
                    }
                string tekVect = "";
                foreach (char x in tekDef) tekVect += x;
                if (PascMethod(tekVect)) resMsg += tekVect + "; ";
            }

            if (resMsg == "") resMsg = "Невозможно доопределить функцию так, чтобы она была линейной";
            return resMsg;
        }

        static string PlusOne(string vect)
        {
            char[] newVect = vect.ToCharArray();
            string vec = "";
            for (int i = newVect.Length - 1; i >= 0; i--) 
                if (newVect[i] == '0') 
                {
                    newVect[i] = '1';
                    foreach (char x in newVect) vec += x;
                    return vec;
                }
                else if (i != 0) newVect[i] = '0';
            foreach (char x in newVect) vec += x;
            return vec;
        }

        static bool EveryElemIsOne(string str)
        {
            foreach (char x in str) if (x != '1') return false;
            return true;
        }

        static int TwoStep(int x)
        {
            if (x == 2) return 1;
            if (x % 2 > 0) return 0;
            else return TwoStep(x/2);
        }

        static bool PascMethod(string function)
        {
            int[,] mainArr = FormArr(function);
            int i = mainArr.GetLength(0) - 1;
            for (int j=0; j<mainArr.GetLength(1); j++)
            {
                if (mainArr[i, j] != 0 && j != 0 && TwoStep(j) == 0) return false;
            }
            return true;
        }

        static int[,] FormArr(string function)
        {
            char[] allValues = function.ToCharArray();
            int[,] resultArr = new int[ (int) Math.Log2(allValues.Length) + 1, allValues.Length];
            for (int i = 0; i < allValues.Length; i++) resultArr[0, i] =int.Parse(allValues[i].ToString());
            int blockCounter;
            bool leftBlock;
            for (int i = 0; i < resultArr.GetLength(0) - 1; i++)
            {
                blockCounter = 0;
                leftBlock = true;
                for (int j = 0; j < resultArr.GetLength(1); j++)
                {
                    if (blockCounter == Math.Pow(2, i)) { blockCounter = 0; leftBlock = !leftBlock; }
                    if (leftBlock) resultArr[i + 1, j] = resultArr[i, j];
                    else resultArr[i + 1, j] = ModTwoSum(resultArr[i, j - int.Parse(Math.Pow(2, i).ToString())], resultArr[i, j]);
                    blockCounter++;
                }
            }
            return resultArr;
        }

        static int ModTwoSum(int a, int b)
        {
            if (a == b) return 0;
            else return 1;
        }
    }
}
