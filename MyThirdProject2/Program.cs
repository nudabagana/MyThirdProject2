using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//(localdb)\MSSQLLocalDB
namespace MyThirdProject2
{
    class Program
    {
        static void Main(string[] args)
        {
            int control = 0;
            while (true)
            {
                Console.WriteLine("Hello, this is Score DB manager. Please choose an action:");
                Console.WriteLine("1. View table contents.");
                Console.WriteLine("2. Insert into table.");
                Console.WriteLine("3. Delete from table.");
                Console.WriteLine("4. Update in table.");
                Console.WriteLine("5. Custom Insert.");
                Console.WriteLine("6. Exit.");
                //control = 0;
                switch (control)
                {
                    case 1:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
