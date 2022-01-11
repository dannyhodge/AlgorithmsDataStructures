using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace algorithmsassignment
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.SetWindowPosition(0, 0);   // sets window position to upper left    
            System.Console.SetBufferSize(200, 300);   // make sure buffer is bigger than window          
            System.Console.SetWindowSize(122, 54);   //set window size to almost full screen
            Console.SetBufferSize(Console.BufferWidth, 4000);     
            //Allows all data to be displayed, instead of deleting old lines after reaching 300 (which would be too low for this project) 

            string[] monthsText = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}; 
            //Allows months to be represented by their names, and not just their number (e.g. January instead of 1)

            List<double> months = new List<double>();  
            List<double> years = new List<double>();
            List<double> ws1af = new List<double>();   //int values stored in double lists to make functions like quicksort and binarysearch simpler
            List<double> ws1rain = new List<double>(); //(all in same datatype)
            List<double> ws1sun = new List<double>();
            List<double> ws1tmin = new List<double>();
            List<double> ws1tmax = new List<double>();
            List<double> ws2af = new List<double>();
            List<double> ws2rain = new List<double>();
            List<double> ws2sun = new List<double>();
            List<double> ws2tmin = new List<double>();
            List<double> ws2tmax = new List<double>();

            //A list to contain each of the sets of data

            bool ascending = true;  //Whether data will be displayed in ascending or descending order
            bool monthsTrue = true; //Whether it is months being ordered or not
            bool menuLoop = true; //A bool to allow the program to loop (not close after one task has been completed)



            while (menuLoop) { //Loop for the menu (to allow the program to not close after one task has been completed)

                AddFiles(out months, monthsText, out years, out ws1af, out ws1rain, out ws1sun, out ws1tmax, out ws1tmin, out ws2af, out ws2rain, out ws2sun, out ws2tmax, out ws2tmin);
                //Function to add all the .txt files to their corresponding list
            

                Console.WriteLine("Do you want to do a search(1), display a catagory(2), find a maximum(3)/minimum(4)/median(5) value, or display every value in its current order(6)?");


                int choice = 6;
                choice = Convert.ToInt32(Console.ReadLine()); //Which decision the user has made

                if(choice==6)    //Not required functionality, but useful for testing that values are correct after quicksort/binary search
                {
                    for(int i = 0; i < ws1af.Count(); i++) //loop through each index from 0 to last item in list
                    {
                        Console.Write(monthsText[Convert.ToInt32(months[i])-1] + " ");  //months is double list, so needs converting to int, and monthstext is strings, based on index of months
                        Console.Write(years[i] + " ");
                        Console.Write("WS1 AF:" + ws1af[i] + " ");
                        Console.Write("WS1 Rain:" + ws1rain[i] + " ");
                        Console.Write("WS1 Sun:" + ws1sun[i] + " ");
                        Console.Write("WS1 Max Temp:" + ws1tmax[i] + " ");
                        Console.Write("WS1 Min Temp:" + ws1tmin[i] + " ");
                        Console.Write("WS2 AF:" + ws2af[i] + " ");
                        Console.Write("WS2 Rain:" + ws2rain[i] + " ");
                        Console.Write("WS2 Sun:" + ws2sun[i] + " ");
                        Console.Write("WS2 Max Temp:" + ws2tmax[i] + " ");
                        Console.WriteLine("WS2 Min Temp:" + ws2tmin[i]);
                    } //End for loop
                } //End if choice==6


                if (choice==1)
            {
                Console.WriteLine("Search by month(1)");
                Console.WriteLine("Search by year(2)");

                int option = Convert.ToInt32(Console.ReadLine());  //decision of whether to search for month or year


                if (option == 1)  //Searching for month
                {
                    Console.WriteLine("Which month (1 - 12) do you want to search for?");
                    int monthChoice = Convert.ToInt32(Console.ReadLine());  //decision of which month to search for

                    List<double> Months = Quick_Sort(months, 0, months.Count() - 1);  //Quicksort to order months, so binary search can be used

                        if (monthChoice > (months[months.Count() - 1]) || monthChoice < months[0])  //If month choice is less than 1 or greater than 12, it isn't a real month, so throw an error
                        {
                            Console.WriteLine("This month doesn't exist");  //Error is thrown, and program will return to main menu loop
                        }
                        else //If month choice is between 1 and 12
                        {
                            List<int> myIndexes = new List<int>();
                            List<int> indexes = Binary_Search(Months, 0, Months.Count - 1, monthChoice, myIndexes); //Run a binarysearch on the now ordered data, to find the given month (monthChoice)

                         
                                if (monthChoice == 12)
                                {
                                    monthChoice = 0; //First instance of december is 0, not 12 (its the first value in the list)
                                }

                                foreach (int ind in indexes)
                                { //Loop cycles through each catagory for month choice, then adds 12 to month choice until it reaches the end (catagories are organized in order of months)
                            
                                        Console.Write(monthsText[Convert.ToInt32(months[ind] - 1)] + " ");
                                        Console.Write(years[monthChoice] + " ");
                                        Console.Write("WS1 AF:" + ws1af[monthChoice] + " ");
                                        Console.Write("WS1 Rain:" + ws1rain[monthChoice] + " ");
                                        Console.Write("WS1 Sun:" + ws1sun[monthChoice] + " ");
                                        Console.Write("WS1 Max Temp:" + ws1tmax[monthChoice] + " ");
                                        Console.Write("WS1 Min Temp:" + ws1tmin[monthChoice] + " ");
                                        Console.Write("WS2 AF:" + ws2af[monthChoice] + " ");
                                        Console.Write("WS2 Rain:" + ws2rain[monthChoice] + " ");
                                        Console.Write("WS2 Sun:" + ws2sun[monthChoice] + " ");
                                        Console.Write("WS2 Max Temp:" + ws2tmax[monthChoice] + " ");
                                        Console.WriteLine("WS2 Min Temp:" + ws2tmin[monthChoice]);

                                    if(monthChoice == 0)
                                    {
                                        monthChoice = 12; //The follow calculation would break if monthChoice==0, as it would remain at 0
                                    } //end if monthchoice == 0
                                    else if (ind  + 12 < (indexes.Count() * monthChoice) - 1) //Finds out whether next addition of 12 would be out of range of the loop (which would cause error)
                                    {
                                        monthChoice += 12;
                                    } //end elseif next rotation out of range
                                }//end foreach for month search
                            
                        } //end else (if months is between 1 and 12)
                   
                } //end if option==1 (months)

                else if(option==2) //user chose to search for years
                {
                       
                    Console.WriteLine("Which year do you want to search for?");
                    int yearChoice = Convert.ToInt32(Console.ReadLine()); //Store choice of year

                        
                        if (yearChoice > (years[0] + ((years.Count() - 2) / 12) + 1) || yearChoice < years[0])  //If year choice is less than 1930 or greater than 2016, it isn't a year in our set, so throw an error
                        {
                            Console.WriteLine("This year doesn't exist");  //Error is thrown, and program will return to main menu loop
                        }
                        else //If year choice is between 1930 and 2016
                        {

                            List<int> myIndexes = new List<int>();
                            List<int> indexes = Binary_Search(years, 0, years.Count - 1, yearChoice, myIndexes); //binarysearch to find given year(yearchoice)
                                                                                                                 //Quicksort wasn't needed, as years are already in ascending order
                            foreach (int ind in indexes) //For each value the binarysearch found for the year, display all corresponding details based on index of the order
                        {                               
                            Console.Write(years[ind] + " ");
                            Console.Write(monthsText[Convert.ToInt32(months[ind] - 1)] + " ");
                            Console.Write("WS1 AF:" + ws1af[ind] + " ");
                            Console.Write("WS1 Rain:" + ws1rain[ind] + " ");
                            Console.Write("WS1 Sun:" + ws1sun[ind] + " ");
                            Console.Write("WS1 Max Temp:" + ws1tmax[ind] + " ");
                            Console.Write("WS1 Min Temp:" + ws1tmin[ind] + " ");
                            Console.Write("WS2 AF:" + ws2af[ind] + " ");
                            Console.Write("WS2 Rain:" + ws2rain[ind] + " ");
                            Console.Write("WS2 Sun:" + ws2sun[ind] + " ");
                            Console.Write("WS2 Max Temp:" + ws2tmax[ind] + " ");
                            Console.WriteLine("WS2 Min Temp:" + ws2tmin[ind]);
                        } //End foreach indexes found in binary search
                    } //End else (if value is a year in the list)

                } //End if user chose to search for years
              

            } //End if user chose to do a search


                if (choice == 2)  //If user chose to display all of a given catagory in ascending/descending order
                {
                    Console.WriteLine("Show months(1)");
                    Console.WriteLine("Show years(2)");
                    Console.WriteLine("Show Lerwick Air Frost(3)");
                    Console.WriteLine("Show Lerwick Rainfall(4)");
                    Console.WriteLine("Show Lerwick Sunshine(5)");
                    Console.WriteLine("Show Lerwick Minimum Temperature(6)");
                    Console.WriteLine("Show Lerwick Maximum Temperature(7)");
                    Console.WriteLine("Show Ross on Wye Air Frost(8)");
                    Console.WriteLine("Show Ross on Wye Rainfall(9)");
                    Console.WriteLine("Show Ross on Wye Sunshine(10)");
                    Console.WriteLine("Show Ross on Wye Minimum Temperature(11)");
                    Console.WriteLine("Show Ross on Wye Maximum Temperature(12)");

                    int option = 0;
                    option = Convert.ToInt32(Console.ReadLine()); //Catagory to display, chosen by user input

                    Console.WriteLine("Ascending(1) or Descending(2) order?");
                    int AorD = Convert.ToInt32(Console.ReadLine()); //Whether to display data in ascending or descending order 

                    if (AorD == 1)
                    {
                        ascending = true;
                    }  else if (AorD == 2)
                    {
                        ascending = false;
                    }
                    monthsTrue = false;

                    if (option == 1) //If user chose to display by month
                    {
                        monthsTrue = true;
                        SortAorD(months, ascending, monthsTrue, monthsText); 
                    }
                    if (option == 2)  //if user chose to display by year
                    {
                        SortAorD(years, ascending, monthsTrue, monthsText);
                    }
                    if (option == 3) //if user chose to display by ws1af
                    {
                        SortAorD(ws1af, ascending, monthsTrue, monthsText);
                     }
                    if (option == 4) //if user chose to display by ws1rain
                    {
                        SortAorD(ws1rain, ascending, monthsTrue, monthsText);
                    }
                    if (option == 5) //if user chose to display by ws1sun
                    {
                        SortAorD(ws1sun, ascending, monthsTrue, monthsText);
                    }
                    if (option == 6) //if user chose to display by ws1tmin
                    {
                        SortAorD(ws1tmin, ascending, monthsTrue, monthsText);
                    }
                    if (option == 7) //if user chose to display by ws1tmax
                    {
                     SortAorD(ws1tmax, ascending, monthsTrue, monthsText); 
                    }
                    if (option == 8) //if user chose to display by ws2af
                    {
                      SortAorD(ws2af, ascending, monthsTrue, monthsText);
                    }
                    if (option == 9) //if user chose to display by ws2rain
                    {
                       SortAorD(ws2rain, ascending, monthsTrue, monthsText);
                    }
                    if (option == 10) //if user chose to display by ws2sun
                    {
                        SortAorD(ws2sun, ascending, monthsTrue, monthsText);
                    }
                    if (option == 11) //if user chose to display by ws2tmin
                    {
                        SortAorD(ws2tmin, ascending, monthsTrue, monthsText);
                    }
                    if (option == 12) //if user chose to display by ws2tmax
                    {
                        SortAorD(ws2tmax, ascending, monthsTrue, monthsText);
                    }
                } //End if choice == 2 (display by catagory)
                else if (choice == 3) //If user chose to find the max value of a catagory
                {
                    Console.WriteLine("What would you like the maximum value of? Lerwick Air Frost(1), Lerwick Rainfall(2), Lerwick Sunshine(3), Lerwick Minimum Temperature(4), Lerwick Maximum Temperature(5), Ross on Wye Air Frost(6), Ross on Wye Rainfall(7), Ross on Wye Sunshine(8), Ross on Wye Minimum Temperature(9) or Ross on Wye Maximum Temperature(10)");
                    int option = 0;
                    option = Convert.ToInt32(Console.ReadLine()); //users choice of what catagory to find max of

                    List<int> unorderdedIndexes = new List<int>(); //the indexes of the max values of the catagory, in their original order

                    if (option == 1) //if user selected ws1af
                    {           
                        unorderdedIndexes = Linear_Search(monthsText, "ws1af", "max");
                    }
                    if (option == 2) //if user selected ws1rain
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1rain", "max");
                    }
                    if (option == 3) //if user selected ws1sun
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1sun", "max");
                    }
                    if (option == 4) //if user selected ws1tmin
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1tmin", "max");
                    }
                    if (option == 5) //if user selected ws1tmax
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1tmax", "max");
                    }
                    if (option == 6) //if user selected ws2af
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2af", "max");
                    }
                    if (option == 7) //if user selected ws2rain
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2rain", "max");
                    }
                    if (option == 8) //if user selected ws2sun
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2sun", "max");
                    }
                    if (option == 9) //if user selected ws2tmin
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2tmin", "max");
                    }
                    if (option == 10) //if user selected ws2tmax
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2tmax", "max");
                    }

                    AddFiles(out months, monthsText, out years, out ws1af, out ws1rain, out ws1sun, out ws1tmax, out ws1tmin, out ws2af, out ws2rain, out ws2sun, out ws2tmax, out ws2tmin);
                    //Rerun the function to add data to lists from source files, to make sure they are in their original order again (not ascending)
                   
                        foreach (int ind in unorderdedIndexes) //each index of the max data in its original order
                        {
              
                        int indTemp = 0;

                        if (months[ind] == 0)
                        {
                            months[indTemp] = 11; //make sure decembers index is 11 not 0
                        }
                        else
                        {
                            months[indTemp] = months[ind] - 1; //Shift everything down by 1 to keep data within range of months, and make it work as an index for monthsText
                        } 
                            Console.Write("Year: " + years[ind] + " "); //Display every corresponding value to the max
                            Console.Write("Month: " + monthsText[Convert.ToInt32(months[indTemp])] + " ");
                            Console.Write("WS1 AF: " + ws1af[ind] + " ");
                            Console.Write("WS1 Rain: " + ws1rain[ind] + " ");
                            Console.Write("WS1 Sun: " + ws1sun[ind] + " ");
                            Console.Write("WS1 Max Temp: " + ws1tmax[ind] + " ");
                            Console.Write("WS1 Min Temp: " + ws1tmin[ind] + " ");
                            Console.Write("WS2 AF " + ws2af[ind] + " ");
                            Console.Write("WS2 Rain: " + ws2rain[ind] + " ");
                            Console.Write("WS2 Sun: " + ws2sun[ind] + " ");
                            Console.Write("WS2 Max Temp: " + ws2tmax[ind] + " ");
                            Console.WriteLine("WS2 Min Temp: " + ws2tmin[ind] + " ");
                        } //end of foreach ind of max data in order
                        
                                           
                } //End of if choice == 3 (max of catagory)


                else if (choice == 4)
                { //If user chose to find the min value of a catagory
                    Console.WriteLine("What would you like the minimum value of? Lerwick Air Frost(1), Lerwick Rainfall(2), Lerwick Sunshine(3), Lerwick Minimum Temperature(4), Lerwick Maximum Temperature(5), Ross on Wye Air Frost(6), Ross on Wye Rainfall(7), Ross on Wye Sunshine(8), Ross on Wye Minimum Temperature(9) or Ross on Wye Maximum Temperature(10)");
                   
                    int option = 0;
                    option = Convert.ToInt32(Console.ReadLine()); //catagory that the user has decided to show minimum value of

                    List<int> unorderdedIndexes = new List<int>();  //the indexes of the min values of the catagory, in their original order

                    if (option == 1) //if user selected ws1af
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1af", "min");
                    }
                    if (option == 2) //if user selected ws1rain
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1rain", "min");
                    }
                    if (option == 3) //if user selected ws1sun
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1sun", "min");
                    }
                    if (option == 4) //if user selected ws1tmin
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1tmin", "min");
                    }
                    if (option == 5) //if user selected ws1tmax
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1tmax", "min");
                    }
                    if (option == 6) //if user selected ws2af
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2af", "min");
                    }
                    if (option == 7) //if user selected ws2rain
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2rain", "min");
                    }
                    if (option == 8) //if user selected ws2sun
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2sun", "min");
                    }
                    if (option == 9) //if user selected ws2tmin
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2tmin", "min");
                    }
                    if (option == 10) //if user selected ws2tmax
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2tmax", "min");
                    }
                   
                    AddFiles(out months, monthsText, out years, out ws1af, out ws1rain, out ws1sun, out ws1tmax, out ws1tmin, out ws2af, out ws2rain, out ws2sun, out ws2tmax, out ws2tmin);
                    //Rerun the function to add data to lists from source files, to make sure they are in their original order again (not ascending)

                    foreach (int ind in unorderdedIndexes) //each index of the min data in its original order
                    {
                        int indTemp = 0;

                        if (months[ind] == 0)
                        {
                            months[indTemp] = 11; //make sure decembers index is 11 not 0
                        }
                        else
                        {
                            months[indTemp] = months[ind] - 1; //Shift everything down by 1 to keep data within range of months, and make it work as an index for monthsText
                        }

                        Console.Write("Year: " + years[ind] + " "); //Display every corresponding value to the min
                        Console.Write("Month: " + monthsText[Convert.ToInt32(months[indTemp])] + " ");
                        Console.Write("WS1 AF: " + ws1af[ind] + " ");
                        Console.Write("WS1 Rain: " + ws1rain[ind] + " ");
                        Console.Write("WS1 Sun: " + ws1sun[ind] + " ");
                        Console.Write("WS1 Max Temp: " + ws1tmax[ind] + " ");
                        Console.Write("WS1 Min Temp: " + ws1tmin[ind] + " ");
                        Console.Write("WS2 AF " + ws2af[ind] + " ");
                        Console.Write("WS2 Rain: " + ws2rain[ind] + " ");
                        Console.Write("WS2 Sun: " + ws2sun[ind] + " ");
                        Console.Write("WS2 Max Temp: " + ws2tmax[ind] + " ");
                        Console.WriteLine("WS2 Min Temp: " + ws2tmin[ind] + " ");
                    } //End of foreach all min values in original order
                } //end of if user chose to find min value (choice == 4)

                else if (choice == 5)
                { //if user chose to show median value
                    Console.WriteLine("What would you like the median value of? Lerwick Air Frost(1), Lerwick Rainfall(2), Lerwick Sunshine(3), Lerwick Minimum Temperature(4), Lerwick Maximum Temperature(5), Ross on Wye Air Frost(6), Ross on Wye Rainfall(7), Ross on Wye Sunshine(8), Ross on Wye Minimum Temperature(9) or Ross on Wye Maximum Temperature(10)");
                    int option = 0;
                    option = Convert.ToInt32(Console.ReadLine()); //catagory that the user has decided to show median value of

                    List<int> unorderdedIndexes = new List<int>();  //the indexes of the median values of the catagory, in their original order

                    if (option == 1) //if user selected ws1af
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1af", "med");
                    }
                    if (option == 2) //if user selected ws1rain
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1rain", "med");
                    }
                    if (option == 3) //if user selected ws1sun
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1sun", "med");
                    }
                    if (option == 4) //if user selected ws1tmin
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1tmin", "med");
                    }
                    if (option == 5) //if user selected ws1tmax
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws1tmax", "med");
                    }
                    if (option == 6) //if user selected ws2af
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2af", "med");
                    }
                    if (option == 7) //if user selected ws2rain
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2rain", "med");
                    }
                    if (option == 8) //if user selected ws2sun
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2sun", "med");
                    }
                    if (option == 9) //if user selected ws2tmin
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2tmin", "med");
                    }
                    if (option == 10) //if user selected ws2tmax
                    {
                        unorderdedIndexes = Linear_Search(monthsText, "ws2tmax", "med");
                    }

                    AddFiles(out months, monthsText, out years, out ws1af, out ws1rain, out ws1sun, out ws1tmax, out ws1tmin, out ws2af, out ws2rain, out ws2sun, out ws2tmax, out ws2tmin);
                    //Rerun the function to add data to lists from source files, to make sure they are in their original order again (not ascending)

                    foreach (int ind in unorderdedIndexes) //each index of the median data in its original order
                    {
                        int indTemp = 0;

                        if (months[ind] == 0)
                        {
                            months[indTemp] = 11; //make sure decembers index is 11 not 0
                        }
                        else
                        {
                            months[indTemp] = months[ind] - 1; //Shift everything down by 1 to keep data within range of months, and make it work as an index for monthsText
                        }

                        Console.Write("Year: " + years[ind] + " "); //Display every corresponding value to the median
                        Console.Write("Month: " + monthsText[Convert.ToInt32(months[indTemp])] + " ");
                        Console.Write("WS1 AF: " + ws1af[ind] + " ");
                        Console.Write("WS1 Rain: " + ws1rain[ind] + " ");
                        Console.Write("WS1 Sun: " + ws1sun[ind] + " ");
                        Console.Write("WS1 Max Temp: " + ws1tmax[ind] + " ");
                        Console.Write("WS1 Min Temp: " + ws1tmin[ind] + " ");
                        Console.Write("WS2 AF " + ws2af[ind] + " ");
                        Console.Write("WS2 Rain: " + ws2rain[ind] + " ");
                        Console.Write("WS2 Sun: " + ws2sun[ind] + " ");
                        Console.Write("WS2 Max Temp: " + ws2tmax[ind] + " ");
                        Console.WriteLine("WS2 Min Temp: " + ws2tmin[ind] + " ");
                    } //End of foreach all median values in original order
                } //end else if choice = median
            } ///End while loop that returns user to main menu after performing operating
            }  //End of Main


        static void AddFiles(out List<double> months, string[] monthsText, out List<double> years, out List<double> ws1af, out List<double> ws1rain, out List<double> ws1sun, out List<double> ws1tmax, out List<double> ws1tmin, out List<double> ws2af, out List<double> ws2rain, out List<double> ws2sun, out List<double> ws2tmax, out List<double> ws2tmin)
        { //Function to add all data files (.txt files) into their own respective lists
            months = new List<double>();
            years = new List<double>();
            ws1af = new List<double>();
            ws1rain = new List<double>();
            ws1sun = new List<double>();
            ws1tmin = new List<double>();
            ws1tmax = new List<double>();
            ws2af = new List<double>();
            ws2rain = new List<double>();
            ws2sun = new List<double>();
            ws2tmin = new List<double>();
            ws2tmax = new List<double>();


            foreach (string line in File.ReadLines("wdata\\Month.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                foreach (string month in monthsText)  //Uses data in monthsText to turn text in the .txt file into a numerical representation of the month
                {
                    if(line == month)
                    {
                        months.Add(Array.IndexOf(monthsText, month) + 1); //Finds the month in monthsText, finds its index, and adds that index to the list
                    }
                }          
            }

            foreach (string line in File.ReadLines("wdata\\Year.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                years.Add(Convert.ToDouble(line));
            }

            foreach (string line in File.ReadLines("wdata\\WS1_AF.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws1af.Add(Convert.ToDouble(line));
            }
            foreach (string line in File.ReadLines("wdata\\WS1_Rain.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws1rain.Add(Convert.ToDouble(line));
            }
            foreach (string line in File.ReadLines("wdata\\WS1_Sun.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws1sun.Add(Convert.ToDouble(line));
            }
            foreach (string line in File.ReadLines("wdata\\WS1_TMax.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws1tmax.Add(Convert.ToDouble(line));
            }
            foreach (string line in File.ReadLines("wdata\\WS1_TMin.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws1tmin.Add(Convert.ToDouble(line));
            }
            foreach (string line in File.ReadLines("wdata\\WS2_AF.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws2af.Add(Convert.ToDouble(line));
            }
            foreach (string line in File.ReadLines("wdata\\WS2_Rain.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws2rain.Add(Convert.ToDouble(line));
            }
            foreach (string line in File.ReadLines("wdata\\WS2_Sun.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws2sun.Add(Convert.ToDouble(line));
            }
            foreach (string line in File.ReadLines("wdata\\WS2_TMax.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws2tmax.Add(Convert.ToDouble(line));
            }
            foreach (string line in File.ReadLines("wdata\\WS2_TMin.txt")) //adds value of .txt file to the corresponding list, each line becoming a single element in the list
            {
                ws2tmin.Add(Convert.ToDouble(line));
            }
        }


        static void SortAorD(List<double> collection, bool ascending, bool monthsTrue, string[] monthsText)
        { //Function to sort and output all values of a given catagory, in ascending or descending order

            if (ascending) //If user chose to order in ascending order
            {
                Console.WriteLine("Would you like to  use a Quick Sort(1) or a Bubble Sort(2)?");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    Quick_Sort(collection, 0, collection.Count() - 1); //Quicksort to put values in order
                }
                else if (choice==2)
                {
                    BubbleSort(collection, 0, collection.Count() - 1); //Bubble sort to put values in order
                }
                else
                {
                    Console.WriteLine("This was an incorrect choice. Please try again");
                    SortAorD(collection, ascending, monthsTrue, monthsText);
                }
                for (int i = 0; i < collection.Count(); i++) //for loop to go through each value in the collection from start to end
                {
                    if (monthsTrue == true) //If sorting months
                    {
                        string monthItem = monthsText[Convert.ToInt32(collection[i]) - 1]; //output values in ascending order, 1 by 1
                        Console.WriteLine(monthItem);
                    }
                    else
                    {
                        Console.WriteLine(collection[i]); //If months isn't true (any other collection), output values in this way
                    }

                }
            }

            else  //If user chose descending order
            {
                Quick_Sort(collection, 0, collection.Count() - 1); //Quicksort to put values in order
                for (int i = collection.Count() - 1; i >= 0; i--) //for loop to go through each value in the collection from end to start
                {
                    if (monthsTrue == true) //If sorting months
                    {
                        string monthItem = monthsText[Convert.ToInt32(collection[i]) - 1];  //output values in descending order, 1 by 1
                        Console.WriteLine(monthItem);
                    }
                    else
                    {
                        Console.WriteLine(collection[i]); //If months isn't true (any other collection), output values in this way
                    }
                } //end for, looping through each value in collection
            } //end else (if user chose descending order)
        
                   
        } //End SortAorD (ascending or descending)


        public static List<int> Linear_Search(string[] monthsText, string searchCat, string type)

        {  //Function to search values when they aren't in order (otherwise, binary search will be used)
            List<int> unorderedIndexes = new List<int>(); //List of indexes that will be returned at end of function
            List<int> indexes = new List<int>(); //List of ordered indexes of the value being searched for
            List<double> collection = new List<double>(); //List of the catagory being used
            List<double> months = new List<double>();
            List<double> years = new List<double>();
            List<double> ws1af = new List<double>();
            List<double> ws1rain = new List<double>();
            List<double> ws1sun = new List<double>();
            List<double> ws1tmin = new List<double>();
            List<double> ws1tmax = new List<double>();
            List<double> ws2af = new List<double>();
            List<double> ws2rain = new List<double>();
            List<double> ws2sun = new List<double>();
            List<double> ws2tmin = new List<double>();
            List<double> ws2tmax = new List<double>();

            AddFiles(out months, monthsText, out years, out ws1af, out ws1rain, out ws1sun, out ws1tmax, out ws1tmin, out ws2af, out ws2rain, out ws2sun, out ws2tmax, out ws2tmin);
            //Run addfiles function to populate the lists with the .txt data

            if (searchCat == "ws1af") //If user is searching inside ws1af
            {
                collection = ws1af;
            }
            if (searchCat == "ws1rain") //If user is searching inside ws1rain
            {
                collection = ws1rain;
            }
            if (searchCat == "ws1sun") //If user is searching inside ws1sun
            {
                collection = ws1sun;
            }
            if (searchCat == "ws1tmin") //If user is searching inside ws1tmin
            {
                collection = ws1tmin;
            }
            if (searchCat == "ws1tmax") //If user is searching inside ws1tmax
            {
                collection = ws1tmax;
            }
            if (searchCat == "ws2af") //If user is searching inside ws2af
            {
                collection = ws2af;
            }
            if (searchCat == "ws2rain") //If user is searching inside ws2rain
            {
                collection = ws2rain;
            }
            if (searchCat == "ws2sun") //If user is searching inside ws2sun
            {
                collection = ws2sun;
            }
            if (searchCat == "ws2tmin") //If user is searching inside ws2tmin
            {
                collection = ws2tmin;
            }
            if (searchCat == "ws2tmax") //If user is searching inside ws2tmax
            {
                collection = ws2tmax;
            }  //Set collection to be equal to whatever catagory is being worked with

                List<double> organizedCollection = new List<double>();

                organizedCollection = Quick_Sort(collection, 0, collection.Count() - 1); //Use quicksort to sort data in ascending order

            if (type == "max") //If user chose to find the maximum value of the data
            {
                double temp = organizedCollection[organizedCollection.Count() - 1]; //Variable to store an instance of the correct value

                for (int i = organizedCollection.Count() - 1; i > 0; i--)   //Loop from end of the list to the beginning
                {
                    if (organizedCollection[i] == temp)
                    {
                        indexes.Add(i); //Loop to go through the list to find the value in temp, and add every other instance of it to the indexes list
                    }
                }  //end for
            } //end if max
               
            else if (type == "min")  //If user chose to find the minimum value of the data
            {
                double temp = organizedCollection[0]; //Variable to store an instance of the correct value

                for (int i = 0; i < organizedCollection.Count(); i++) //Loop from beginning of list, to the end
                {
                    if (organizedCollection[i] == temp)
                    {
                        indexes.Add(i);  //Loop to go through the list to find the value in temp, and add every other instance of it to the indexes list
                    }
                } //end for
            } //end if min
            else if (type=="med")
            {
                double temp = organizedCollection[(organizedCollection.Count() / 2) + 1]; //Variable to store an instance of the correct value

                for (int i =(organizedCollection.Count() / 2) + 1; i < (organizedCollection.Count() / 2) + 2; i++) //Loop from beginning of list, to the end
                {

                    if (organizedCollection[i] == temp)
                    {
                        indexes.Add(i);  //Loop to go through the list to find the value in temp, and add every other instance of it to the indexes list
                       
                    }
                   
                } //end for
            } //end if median

            AddFiles(out months, monthsText, out years, out ws1af, out ws1rain, out ws1sun, out ws1tmax, out ws1tmin, out ws2af, out ws2rain, out ws2sun, out ws2tmax, out ws2tmin);
            //Run addfiles function to populate the lists with the .txt data

            if (searchCat == "ws1af") //If user is searching inside ws1af
            {
                collection = ws1af;
            }
            if (searchCat == "ws1rain") //If user is searching inside ws1rain
            {
                collection = ws1rain;
            }
            if (searchCat == "ws1sun") //If user is searching inside ws1sun
            {
                collection = ws1sun;
            }
            if (searchCat == "ws1tmin") //If user is searching inside ws1tmin
            {
                collection = ws1tmin;
            }
            if (searchCat == "ws1tmax") //If user is searching inside ws1tmax
            {
                collection = ws1tmax;
            }
            if (searchCat == "ws2af") //If user is searching inside ws2af
            {
                collection = ws2af;
            }
            if (searchCat == "ws2rain") //If user is searching inside ws2rain
            {
                collection = ws2rain;
            }
            if (searchCat == "ws2sun") //If user is searching inside ws2sun
            {
                collection = ws2sun;
            }
            if (searchCat == "ws2tmin") //If user is searching inside ws2tmin
            {
                collection = ws2tmin;
            }
            if (searchCat == "ws2tmax") //If user is searching inside ws2tmax
            {
                collection = ws2tmax;
            }

            for (int i = 0; i < collection.Count() - 1; i++) //Loop from beginning to end of list to check values found against unordered list, and add the unordered indexes
                {

                    if (collection[i] == organizedCollection[indexes[0]])
                    {
                        unorderedIndexes.Add(i); //Add unordered index to list
                    }

                } //end for
                 return unorderedIndexes;
        } //End Linear Search



        public static List<double> Quick_Sort(List<double> data, int left, int right)
        { //Function to sort data into ascending order
            int i, j; 
            double pivot, temp;
            i = left; //i is the leftmost value being checked
            j = right; //j is the rightmost value being checked
            pivot = data[(left + right) / 2]; //Pivot is the middle value in the list, than i and j will be checked against

            do
            {
                while ((data[i] < pivot) && (i < right))
                {
                    i++; //If i is in right place compared to pivot, move onto next value to the right
                }

                while ((pivot < data[j]) && (j > left))
                {
                    j--; //If j is in right place compared to pivot, move onto next value to the left
                }

                if (i <= j)
                { //if prior whileloops ended, i and j are either equal, or in the wrong place, so if they are, swap them
                    temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    i++;
                    j--;
                }

            } while (i <= j); //Do the above until every value has been checked against pivot


            if (left < j) //if every value hasn't been checked, run function recursively until they have, reducing set of values being checked in the process
            {
                Quick_Sort(data, left, j);
            }

            if (i < right) //if every value hasn't been checked, run function recursively until they have, reducing set of values being checked in the process

            {
                Quick_Sort(data, i, right);
            }
           
            return data; //If each value has been checked against every other value, return the correctly sorted list
        } //End Quicksort


        public static List<double> BubbleSort(List<double> data, int start, int end) //Bubble sort to sort data. Less efficient alternative to the Quick Sort
        {
            int counter = 0;

            while (start + 1 <= end) //To avoid going outside lists range
            {
                if(data[start] <= data[start + 1]) //If the value is less than or equal to the next value, correct position
                {
                    
                }
                else //Else, they need to be swapped around using a temporary double
                {
                    counter++; //Counter measures how many changes occured this iteration. If it is none, it is in order, and can be returned successfully
                   double temp = data[start];
                   data[start] = data[start + 1];
                   data[start + 1] = temp;
                }

                start++;

            } //End while 

        
                if(counter == 0)
                {
                
                return data; //Return the correct data set
              
            }
                else
                {
       
                    BubbleSort(data, 0, data.Count() - 1); //Recursively run bubble sort again, until it is correct
                }

            return data;
        } //End bubblesort

        public static List<int> Binary_Search(List<double> data, int start, int end, double valueToFind, List<int> Indexes)
        { //Function that searches for an item (or multiple instances of an item) in a list

            int index = ((end + start) / 2); //Middle number in the list, which will be checked against value to find

            if (data[index] == valueToFind)
            {
                int tempIndex = index;

                while(data[index]==valueToFind)
                {
                    Indexes.Add(index); //If the index is the value being searched for, add to list of indexes

                    if (index != data.Count() - 1) //If it is last value, adding 1 will cause exception, so only do following code if it isn't
                    {
                        if (data[index + 1] != valueToFind) //If next value isn't another instance of the value being searched for, stop
                        {
                            
                            break;
                        }
                        else //If next value is another instance of value being searched for, move to it so it can be added to list
                        {
                           
                            index++;
                           
                        }
                    } //end if value isn't last value

                    else //If index is the last value, stop
                    {
                        break;
                    }
   
                } //End while data = value being searched for

                if (index > 0) //Only do code if index isn't 0, as next part of code would cause exception
                {
                    index = tempIndex - 1; //Cycle backwards through 

                    while (data[index] == valueToFind)
                    {
                        Indexes.Add(index); //If previous value is another instance of value being searched for, move to it so it can be added to list


                        if (index != 0) //Preventing exception in next line
                        {
                            if (data[index - 1] != valueToFind) //if next value back in list isn't value being searched for, do nothing, end loop
                            {

                                break;

                            }
                            else //If next value back in loop is value being searched for, go through loop again with that value
                            {
                                index--;

                            } //end if index isn't 0
                        }
                        else
                        {
                            break;
                        }
                    } //End while index = value being searched for 
                } //end if index is great than 0 
                               
            } //end if index = value being searched for
             
            else if (data[index] < valueToFind)
            {   //If the middle value is smaller than the value being searched for, the first half of the data set can be discounted, as it is ordered
                start = index + 1;
                Binary_Search(data, start, end, valueToFind, Indexes); //The function is run recursively, using the new dataset, until the item has been found, or proven that it doesn't exist
              
            }
            else if (data[index] > valueToFind)
            {  //If the middle value is larger than the value being searched for, the latter half of the data set can be discounted, as it is ordered
                end = index - 1;
                Binary_Search(data, start, end, valueToFind, Indexes);
                
            }

            return Indexes; //Once all instances of value being searched have been found, return the list containing them all

        } //End Binary Search

    } //End class Program
} //End namespace algorithmassignment
