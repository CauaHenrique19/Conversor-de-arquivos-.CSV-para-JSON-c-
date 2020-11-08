using System;
using System.IO;
using System.Linq;

namespace LeitorCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader(@"C:\Users\cauah\OneDrive\Área de Trabalho\CSV\Example-Spreadsheet.CSV");
            
            string[] headers = null;
            string jsonComplete = "";

            int countHeaders = 0;

            while (true)
            {
                string line = reader.ReadLine();
                headers = countHeaders == 0 ? line.Split(";") : headers;

                if (line == null) break;

                int countLines = 1;
                string json = "";

                var lines = line.Split(";").ToArray();

                json += countHeaders != 0 ? "\t{" : "";

                for(int i = 0; i < lines.Length; i++)
                {
                    if (!headers.Contains(lines[i]))
                    {
                        if(countLines != headers.Length)
                        {
                            json += double.TryParse(lines[i], out double itemParsed) ? 
                                $"\"{headers[countLines - 1].Replace(" ", "")}\": {itemParsed.ToString().Replace(",", ".")}, " : 
                                $"\"{headers[countLines - 1].Replace(" ", "")}\": \"{lines[i]}\", ";
                        }
                        else
                        {
                            json += double.TryParse(lines[i], out double itemParsed) ?
                                $"\"{headers[countLines - 1].Replace(" ", "")}\": {itemParsed.ToString().Replace(",", ".")}" :
                                $"\"{headers[countLines - 1].Replace(" ", "")}\": \"{lines[i]}\"";
                        }
                    }
                    countLines++;
                }

                json += countHeaders != 0 ? "},\n" : "";
                jsonComplete += json;
                
                countHeaders++;
            }

            jsonComplete = "[\n" + jsonComplete.Substring(0, jsonComplete.Length - 2) + "\n]";

            using(StreamWriter sw = File.CreateText(@"C:\Users\cauah\source\repos\LeitorCSV\LeitorCSV\JSONGerados\JSON.json"))
            {
                sw.Write(jsonComplete);
            }

            Console.WriteLine("ARQUIVO CRIADO!");
            Console.ReadKey();
        }
    }
}
