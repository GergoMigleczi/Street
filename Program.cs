using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Street
{
    /*
        s w  f
        0 10 P   if side=0 even house numbers else odd house numbers
        1 8 K    if fence = # fence = fence is built but not painted yet
        1 10 :   if fence = : fence = no fence    
        1 9 S    if fence = A-Z fence = painted (colour starts with that letter)
        0 10 P 
     */
    struct Street
    {
        public int sideOftheStreet;
        public int widthOfTheProperty;
        public string fence;

        public Street(int sideOftheStreet, int widthOfTheProperty, string fence)
        {
            this.sideOftheStreet = sideOftheStreet;
            this.widthOfTheProperty = widthOfTheProperty;
            this.fence = fence;
        }
    }
    class StreetClass
    {
        static List<Street> properties = new List<Street>();

        //Task 1: read and store the data of fence.txt
        static void Task1()
        {
            StreamReader sr = new StreamReader("kerites.txt");

            while (!sr.EndOfStream)
            {
                string[] adatok = sr.ReadLine().Split();
                int sideOftheStreet = int.Parse(adatok[0]);
                int widthOfTheProperty = int.Parse(adatok[1]);
                string fence = adatok[2];

                Street item = new Street(sideOftheStreet, widthOfTheProperty, fence);
                properties.Add(item);
            }
            sr.Close();
        }

        //Task 2: Count the properties in the street
        static void Task2()
        {
            Console.WriteLine("Task 2");
            Console.WriteLine($"\tNumber of properties: {properties.Count} ");
        }

        //Task 3: Print which side was the last property in the txt on, and what house number it got
        static void Task3()
        {
            Console.WriteLine("Task 3");
            int sideOftheStreet = 0;
            int houseNumber = 0;
            foreach (Street item in properties)
            {
                sideOftheStreet = item.sideOftheStreet;
            }
            if (sideOftheStreet == 0)
            {

                foreach (Street item in properties)
                {
                    if (item.sideOftheStreet % 2 == 0) //even
                    {
                        houseNumber += 2;
                    }
                }
                Console.WriteLine("\tOn the side with even house numbers.");
                Console.WriteLine($"\tHouse number: {houseNumber}");
            }
            else
            {
                houseNumber = -1;
                foreach (Street item in properties)
                {
                    if (item.sideOftheStreet % 2 != 0) //odd
                    {
                        houseNumber += 2;
                    }
                }
                Console.WriteLine("\tOn the side with odd house numbers.");
                Console.WriteLine($"\tHouse number: {houseNumber}");
            }

        }

        //Task 4: print a house number from the odd side, which's neighbour's fence is the same colour
        static void Task4()
        {
            Console.WriteLine("Task 4");
            string neighbourFence = "";
            string fence = "";
            int houseNumber = -1;
            foreach (Street item in properties)
            {
                if (item.sideOftheStreet == 1)
                {
                    houseNumber += 2;
                    if (item.fence != "#" || item.fence != ":")
                    {
                        fence = item.fence;
                        if (fence == neighbourFence)
                        {
                            Console.WriteLine($"\tHouse number {houseNumber} has teh same colour fence as its neighbour");
                            break;
                        }
                    }
                }
                neighbourFence = fence;
            }
        }

        //Task 5: Ask the user for a house number
        //        Print that house's fence's colour or # or :
        //        the owner wants to paint the fence to a colour which is different than the neighbour's and the previous colour
        static void Task5()
        {
            Console.WriteLine("Task 5");
            Console.Write("House number  ");
            int houseNumber = int.Parse(Console.ReadLine());
            int counter = 0;
            string colourOfTheFence = "";
            if (houseNumber % 2 == 0)
            {
                foreach (Street item in properties)
                {
                    if (item.sideOftheStreet == 0)
                    {
                        counter += 2;
                        if (counter == houseNumber)
                            Console.WriteLine($"\tFence: {item.fence}");
                        colourOfTheFence = item.fence;
                    }

                }
            }
            else
            {
                counter = -1;
                foreach (Street item in properties)
                {
                    if (item.sideOftheStreet == 1)
                    {
                        counter += 2;
                        if (counter == houseNumber)
                            Console.WriteLine($"\tFence: {item.fence}");
                        colourOfTheFence = item.fence;
                    }

                }
            }
            //---------------------------------------------
            string neighbour1 = "";
            string neighbour2 = "";
            if (houseNumber % 2 == 0)
            {
                foreach (Street item in properties)
                {
                    if (item.sideOftheStreet == 0)
                    {
                        counter += 2;
                        if (counter == houseNumber - 2)
                            neighbour1 = item.fence;
                        if (counter == houseNumber + 2)
                            neighbour2 = item.fence;
                    }

                }
            }
            else
            {
                counter = -1;
                foreach (Street item in properties)
                {
                    if (item.sideOftheStreet == 1)
                    {
                        counter += 2;
                        if (counter == houseNumber - 2)
                            neighbour1 = item.fence;
                        if (counter == houseNumber + 2)
                            neighbour2 = item.fence;
                    }

                }
            }
            List<string> abc = new List<string>();
            string availabelColour = "";
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                string l = $"{letter}";
                abc.Add(l);
            }
            foreach (string letter in abc)
            {
                if (letter != neighbour1 && letter != neighbour2 && letter != colourOfTheFence)
                {
                    availabelColour = letter;
                }
            }
            Console.WriteLine($"\tAn available colour: {availabelColour}");

        }

        //Task 6: in the OddSide.txt file display the properties like this:
        /*
         KKKKKKKK::::::::::SSSSSSSSSBBBBBBBBFFFFFFFFFKKKKKKKKKKIIIIIIII   //fence*width
         1       3         5        7       9        11        13         //house number
         */
        static void Task6()
        {
            StreamWriter sw = new StreamWriter("OddSide.txt");
            int houseNumber = -1;
            foreach (Street item in properties)
            {
                if (item.sideOftheStreet == 1)
                {
                    for (int i = 0; i < item.widthOfTheProperty; i++)
                    {
                        sw.Write(item.fence);
                    }
                }
            }
            sw.WriteLine();
            string space = " ";
            foreach (Street item in properties)
            {
                if (item.sideOftheStreet == 1)
                {
                    houseNumber += 2;

                    sw.Write(houseNumber);
                    for (int i = 0; i < item.widthOfTheProperty - houseNumber.ToString().Length; i++)
                    {
                        sw.Write(space);

                    }


                }
            }
            sw.Flush();
            sw.Close();
        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Task5();
            Task6();
            Console.ReadKey();
        }
    }
}
