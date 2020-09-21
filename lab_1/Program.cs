using System;
using System.Collections.Generic;

class StringArray
{

    // Хранилище наших строк
    private List<string> list = new List<string>();

    // Получаем и присваиваем элементы по индексу
    public string Get(int index)
    {
        return list[index];
    }

    // Получаем элементы по "начальной комбинации символов"
    public string Get(string key)
    {
        foreach (var element in list)
        {
            if (element.ToLower().StartsWith(key.ToLower()))
            {
                return element;
            }
        }
        return null;
    }

    // Свойство, возвращает кол-во строк в массиве
    public int Count
    {
        get => list.Count;
    }

    // Выводим элемент под индексом, проверяем границы массива
    public void PrintElementAt(int index)
    {
        if (index >= list.Count)
        {
            Console.WriteLine("Некорректный индекс массива!");
            return;
        }
        Console.WriteLine(list[index]);
    }

    // Выводим весь массив
    public void Print()
    {
        foreach (var element in list)
        {
            Console.WriteLine(element);
        }
    }

    public void Add(string element)
    {
        list.Add(element);
    }

    // Слияние двух массивов поэлементно
    // А так же с исключением повторяющихся
    public StringArray MergeWith(StringArray array, bool unique = false)
    {
        var result = new StringArray();
        foreach (var element in list)
        {
            result.Add(element);
        }
        for (int i = 0; i < array.Count; i++)
        {
            var element = array.Get(i);
            if (unique && result.Get(element) != null) { continue; }
            result.Add(element);
        }
        return result;
    }

    // Пересечение 2 массивов
    public StringArray Intersect(StringArray array)
    {
        var result = new StringArray();
        for (int i = 0; i < array.Count; i++)
        {
            var element = array.Get(i);
            if (Get(element) != null)
            {
                result.Add(element);
            }
        }
        return result;
    }

}

enum MainMenuItem
{
    CREATE_ARR_1 = 1,
    CREATE_ARR_2,
    PRINT_AT_INDEX,
    PRINT_ARR,
    MERGE,
    MERGE_UNIQUE,
    INTERSECT,
    EXIT
}

class MainClass
{

    public static string Description(MainMenuItem item)
    {
        switch (item)
        {
            case MainMenuItem.CREATE_ARR_1:
                return "Создать массив 1";
            case MainMenuItem.CREATE_ARR_2:
                return "Создать массив 2";
            case MainMenuItem.PRINT_AT_INDEX:
                return "Вывести строку под индексом";
            case MainMenuItem.PRINT_ARR:
                return "Вывести все элементы массива";
            case MainMenuItem.MERGE:
                return "Слить 2 массива в 1";
            case MainMenuItem.MERGE_UNIQUE:
                return "Слить 2 массива в 1 без повторяющихся значений";
            case MainMenuItem.INTERSECT:
                return "Получить пересечение 2 массивов";
            case MainMenuItem.EXIT:
                return "Выход";
            default:
                return "";
        }
    }

    public static void ShowMenu()
    {
        Console.WriteLine("");
        for (var item = MainMenuItem.CREATE_ARR_1; item <= MainMenuItem.EXIT; item++)
        {
            Console.WriteLine($"{(int)item}. {Description(item)}");
        }
    }

    public static void FulfillArray(StringArray array)
    {
        Console.WriteLine("Введите строки через запятую");
        char[] separator = { ',' };
        var elements = Console.ReadLine().Split(separator, 256, StringSplitOptions.None);
        foreach (var element in elements)
        {
            array.Add(element);
        }
    }

    public static int PickAnArray()
    {
        Console.WriteLine("Выберите массив 1 или 2");
        return Int32.Parse(Console.ReadLine());
    }

    public static void Run()
    {
        var arr1 = new StringArray();
        var arr2 = new StringArray();
        while (true)
        {
            ShowMenu();
            Console.WriteLine("Выберите опцию: ");
            var option = (MainMenuItem)Int32.Parse(Console.ReadLine());
            Console.WriteLine("");
            switch (option)
            {
                case MainMenuItem.CREATE_ARR_1:
                    FulfillArray(arr1);
                    break;
                case MainMenuItem.CREATE_ARR_2:
                    FulfillArray(arr2);
                    break;
                case MainMenuItem.PRINT_AT_INDEX:
                    {
                        int arrPosition = PickAnArray();
                        Console.WriteLine("Введите индекс строки");
                        var index = Int32.Parse(Console.ReadLine());
                        var arr = arrPosition == 1 ? arr1 : arr2;
                        arr.PrintElementAt(index);
                        break;
                    }
                case MainMenuItem.PRINT_ARR:
                    {
                        int arrPosition = PickAnArray();
                        var arr = arrPosition == 1 ? arr1 : arr2;
                        arr.Print();
                        break;
                    }
                case MainMenuItem.MERGE:
                    {
                        var arr = arr1.MergeWith(arr2);
                        arr.Print();
                        break;
                    }
                case MainMenuItem.MERGE_UNIQUE:
                    {
                        var arr = arr1.MergeWith(arr2, unique: true);
                        arr.Print();
                        break;
                    }
                case MainMenuItem.INTERSECT:
                    {
                        var arr = arr1.Intersect(arr2);
                        if (arr.Count != 0)
                        {
                            arr.Print();
                        }
                        else
                        {
                            Console.WriteLine("Нет пересечений");
                        }
                        break;
                    }
                case MainMenuItem.EXIT:
                    Console.WriteLine("Пока!");
                    return;
                default:
                    Console.WriteLine("Нет такой опции");
                    break;
            }
        }
    }

    public static void Main(string[] args)
    {
        Run();
    }

}