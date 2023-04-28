using KIT206Assignment2.Entity;
using System;
using System.Reflection;

namespace Program
{
    class Program
    {
        static void Main()
        {
            Researcher r = new Researcher {id = 1, GivenName = "Jane", FamilyName = "Green", Title = "teacher", School = "University", Campus = "highway", Email = "111@gmail.com", Photo = "hello.com"};

            Console.WriteLine(r);
        }
    }
}