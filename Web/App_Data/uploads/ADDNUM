using System.IO;
using System;

public class TestClass
{
    public static void Main()
    {
        var reader = new StreamReader("input.txt");
        var writer = new StreamWriter("output.txt");

        int[] a = new int[10];
        Console.WriteLine("a[10] = " + a[9]);
        
        writer.WriteLine(reader.ReadLine());

        reader.Close();
        writer.Close();
    }
}