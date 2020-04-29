using System;
using System.Linq;

namespace mysql.EF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            LibraryContext libraryContext = new LibraryContext();
            var data = libraryContext.category.ToList();
        }
    }
}
