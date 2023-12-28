/*
    Studnet: Beatrice Björn
    Asssignment: Moment 3 - DT071G -  Programmering i C#.NET
    Last updated: 2022-12-19
*/

//Including namespaces 
using System;
using System.Collections.Generic;
using System.IO;

using System.Text.Json;

//Creating a namespace called "guests"
namespace guests
{
    // Creating a public class called "GuestBook"
    // Storing the filename for the JSON file in a variable called "filename" and initializing list to store guestbook entries
    // An if-statement checks if the file "guestbook.json" exists, if so the content gets turned into a JSON-string
    // The JSON file is then deserialize into a list of objects
    public class GuestBook{

        private string filename = @"guestbook.json";
        private List<Guest> guests = new List<Guest>();

        public GuestBook(){ 
            if(File.Exists(@"guestbook.json")==true){
                string jsonString = File.ReadAllText(filename);
                guests = JsonSerializer.Deserialize<List<Guest>>(jsonString);
            }
        }

        // The public function "addGuest" is used to add a guest to the list
        // The JSON list is updated with the new guest added and the added guest gets returned
        public Guest addGuest(Guest guest){
            guests.Add(guest);
            marshal();         
            return guest;
        }

        // The public function delGuest takes an argument of index which is an integer
        // The guest with the given index is then removed 
        // The JSON list is updated and Index gets returned
        public int delGuest(int index){
            guests.RemoveAt(index);
            marshal();
            return index;
        }

        // The function "getGuests" returns the list of guests
        public List<Guest> getGuests(){
            return guests;
        }

        // Private void marchal is used to serialize the list of guests to a JSON string and write it to the file
        private void marshal()
        {
            var jsonString = JsonSerializer.Serialize(guests);
            File.WriteAllText(filename, jsonString);
        }
    }

    // Creating a public class called "Guest"
    // Creating a variable named "name" where the name is stored and a variable named "post" where the post is stored
    public class Guest {
        private string name;        
        public string Name
        {
            set {this.name = value;}
            get {return this.name;}
        }

        private string post;        
        public string Post
        {
            set {this.post = value;}
            get {return this.post;}
        }
    }

    // The "Program" class holds the main function for the application
    // The class Guestbook is initiated
    // A while loop where Console.WriteLine is used to type out instructions for the guestbook
    // A foreach loop is used to loop through all guests in the guestbook and print them out
    // A new instance of the class "Guest is created and name and post is added to the guestbook
    // An if statement checks that neither name or post has been submitted empty, if so, name and post will not be stored. 
    // The different functions created gets run depending on what key the user is pressing
    class Program {
        static void Main(string[] args)
        {
 
            GuestBook guestbook = new GuestBook();
            int i=0;

            while(true){
                Console.Clear();Console.CursorVisible = false;
                Console.WriteLine("Beas Gästbok\n\n");

                Console.WriteLine("1. Tryck 1 för att skapa ett inlägg i gästboken\n");
                Console.WriteLine("2. Tryck 2 för att ta bort ett inlägg från gästboken\n");
                Console.WriteLine("x. Tryck x för att avsluta\n");

                i=0;
                foreach(Guest guest in guestbook.getGuests()){
                    Console.WriteLine("[" + i++ + "] " + guest.Name + " - " + guest.Post);
                }

                // Console.ReadLine is used to save the input for both name and post
                int inp = (int) Console.ReadKey(true).Key;
                switch (inp) {
                case '1':
                    Console.CursorVisible = true; 
                    Console.Write("Skriv in ditt namn: ");
                    string name = Console.ReadLine();

                    Console.Write("Skriv ditt gästbokinlägg: ");
                    string post = Console.ReadLine();

                    Guest obj = new Guest();
                    obj.Name = name;
                    obj.Post = post;
                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(post)){
                        Console.WriteLine("Fyll i både namn och inlägg");                   
                    }else{
                        guestbook.addGuest(obj);
                    }
                    break;

                    // Deletes a guest with given index
                    case '2': 
                        Console.CursorVisible = true;
                        Console.Write("Ange index för att radera: ");
                        string index = Console.ReadLine();
                        if(!String.IsNullOrEmpty(index)){
                            guestbook.delGuest(Convert.ToInt32(index));
                        }
                     break;
                    // Exists the application
                    case 88: 
                        Environment.Exit(0);
                    break;
                }
            }
        }
    }
}
