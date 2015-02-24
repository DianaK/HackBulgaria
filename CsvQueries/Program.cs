using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvQueries
{
    public class Program
    {
        private static void Main(string[] args)
        {
            List<string> columnNames;
            var csvFileHash = ReadFile(out columnNames);
            var queryExecutor = new QueryExecutor(csvFileHash, columnNames);
            Console.Write(queryExecutor.Select(new List<string>() {"id", "name"}, 5));
            Console.WriteLine();
            Console.Write(queryExecutor.Sum("id"));
            Console.WriteLine();
            Console.Write(queryExecutor.Show());
            Console.WriteLine();
            Console.Write(queryExecutor.Find("-"));
        }

        public static Dictionary<int, IDictionary<string, string>> ReadFile(out List<string> columnNames)
        {
            var csvFileHash = new Dictionary<int, IDictionary<string, string>>();// for every row, hash with column names and column values
            string[] keys = {}; // column names
            int lineNumber = 0;
            using (var file =
                new StreamReader(@"C:\Test\AFTPPath\csvFile.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (lineNumber == 0)
                    {
                        keys = line.Split(',');
                        lineNumber++;
                        continue;
                    }
                    csvFileHash.Add(lineNumber, new Dictionary<string, string>());
                    string[] currentRowValues = line.Split(','); // column values
                    for (int i = 0; i < keys.Count(); i++)
                    {
                        string value;
                        if (i >= currentRowValues.Count())
                        {
                            value = String.Empty;
                            // if the column names are more than column values ( the csv file is not correct)
                        }
                        else
                        {
                            value = currentRowValues[i];
                        }
                        csvFileHash[lineNumber].Add(keys[i], value);
                    }
                    lineNumber++;
                }
            }
            columnNames = keys.ToList();
            return csvFileHash;
        }

        
    }
}
