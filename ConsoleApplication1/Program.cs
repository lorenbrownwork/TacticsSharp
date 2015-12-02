﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Armor WoodenSheild = new Armor ("Wooden Sheild", 10, "Normal", 10, "Fire");
            Weapon RustySword = new Weapon ("Rusty Sword", 30, "Normal", 40, "Fire");

            Character Joe = new Character ("Joe", 100, 20, 2.0, 2.0, 1.0, RustySword, WoodenSheild);
            Character Bob = new Character ("Bob", 100, 20, 1.0, 2.0, 1.0, RustySword, WoodenSheild);

            Console.WriteLine(Joe.getHP());
            Joe.hurt(Bob);
            Console.WriteLine(Joe.getHP());

            string path = serializer(Joe);
            Character Joe2 = deserializer(path);

            //Wait
            Console.ReadKey();

        }

        static string serializer(Character character)
        {
            //Path to Desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = path + "\\" + character.getName() + ".bin";
            Console.WriteLine(path);

            //Serialize
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, character);
            stream.Close();

            return path;
        }

        static Character deserializer(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Character obj = (Character)formatter.Deserialize(stream);
            stream.Close();

            // Here's the proof.
            Console.WriteLine("Name: {0}", obj.getName());

            return obj;
        }
    }
}

