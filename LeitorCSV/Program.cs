using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace LeitorCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader(@"C:\Users\cauah\OneDrive\Área de Trabalho\Pasta1.CSV");
            
            string[] headers = null;
            string line = "";
            string jsonComplete = "";

            int countHeaders = 0;

            while (true)
            {
                line = reader.ReadLine();

                if(countHeaders == 0)
                {
                    headers = line.Split(";");
                }

                if (line == null) break;

                int countLines = 1;

                foreach(var item in line.Split(";"))
                {
                    string json = "";

                    if (!headers.Contains(item))
                    {
                        if(countLines == 1)
                        {
                            json += "\t{";
                        }

                        if(countLines % headers.Length != 0)
                        {

                            if (double.TryParse(item, out double itemNumero))
                            {
                                json += $"\"{headers[countLines - 1]}\": {itemNumero.ToString().Replace(",", ".")}, ";
                            }
                            else
                            {
                                json += $"\"{headers[countLines - 1]}\": \"{item}\", ";
                            }
                        }
                        else
                        {
                            if (double.TryParse(item, out double itemNumero))
                            {
                                json += $"\"{headers[countLines - 1]}\": {itemNumero.ToString().Replace(",", ".")} ";
                            }
                            else
                            {
                                json += $"\"{headers[countLines - 1]}\": \"{item}\"";
                            }

                            json += "},\n";
                        }
                    }
                    else if(headers.Contains(item))
                    {
                        continue;
                    }

                    jsonComplete += json;
                    countLines++;
                }

                countHeaders++;
            }

            jsonComplete = "[\n" + jsonComplete.Substring(0, jsonComplete.Length-2) + "\n]";

            using(StreamWriter sw = File.CreateText(@"C:\Users\cauah\source\repos\LeitorCSV\LeitorCSV\JSONGerados\JSON.json"))
            {
                sw.Write(jsonComplete);
            }

            Console.WriteLine("ARQUIVO CRIADO!");
            Console.ReadKey();
        }
    }
}
