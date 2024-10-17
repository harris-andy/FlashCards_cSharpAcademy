using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcards.harris_andy
{
    public class DisplayData
    {
        public static void MainMenu()
        {
            Console.WriteLine(
                "--------------------------------------------------\n" +
                "\n\t\tMAIN MENU\n\n" +
                "\tWhat would you like to do?\n\n" +
                "\tType 0 to Close Application\n" +
                "\tType 1 to View All Records\n" +
                "\tType 2 to View Records Summary\n" +
                "\tType 3 to Insert Record\n" +
                "\tType 4 to Delete Record\n" +
                "\tType 5 to Update Record\n" +
                "\tType 6 to Delete All Records :(\n" +
                "\tType 7 to Add 100 Rows of Fake Data\n" +
                "\tType 8 to Start a Timed Coding Session. Neat!\n" +
                "\tType 9 to Set a Coding Goal\n" +
                "\tType 10 to Get Coding Goal Progress\n" +
                "--------------------------------------------------\n");
        }
    }
}