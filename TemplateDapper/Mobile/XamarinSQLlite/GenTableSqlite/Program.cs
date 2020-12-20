using System;
using System.IO;

namespace GenTableSqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LcXamainSqlLite.db");
            Console.WriteLine("Hello World!");
        }
    }
}
