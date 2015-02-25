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
            Dictionary<int, IDictionary<string, string>> csvFileHash = ReadFile(out columnNames);
            var queryExecutor = new QueryExecutor(csvFileHash, columnNames);

                string selectionString =
                    String.Format("1. SELECT [columns] LIMIT X \n2. SUM [column] \n3. SHOW \n4. FIND X");
                do
                {
                    while (! Console.KeyAvailable)
                    {
                        Console.WriteLine("Please choose type of query or ESC to quit:");
                        Console.WriteLine(selectionString);
                        string userInput = Console.ReadLine();

                        int choice = 0;
                        Int32.TryParse(userInput, out choice);
                        if (choice < 1 || choice > 4)
                        {
                            Console.WriteLine("Wrong input!");
                            continue;
                        }
                        Console.WriteLine("Please write your query");
                        string userQuery = Console.ReadLine();
                        if (!ExecuteQuery(userQuery, choice, queryExecutor))
                        {
                            Console.WriteLine("Wrong query!\n");
                        }
                        else
                        {
                            Console.WriteLine("\n");
                        }
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            //Console.Write(queryExecutor.Select(new List<string>() {"id", "name"}, 5));
            //Console.WriteLine();
            //Console.Write(queryExecutor.Sum("id"));
            //Console.WriteLine();
            //Console.Write(queryExecutor.Show());
            //Console.WriteLine();
            //Console.Write(queryExecutor.Find("-"));
        }

        public static bool ExecuteQuery(string query, int choice, QueryExecutor queryExecutor)
        {
            bool executionStatus = true;
            switch (choice)
            {
                case 1:
                {
                    // Select 
                    query = query.ToLower();
                    string[] queryParts = query.Split(null);
                    int limit = Int32.MaxValue;
                    bool hasLimit = false;
                    if (query.Contains("limit"))
                    {
                        hasLimit = true;
                        if (!int.TryParse(queryParts[queryParts.Length - 1], out limit))
                        {
                            return false;
                            // the limit is not an integer
                        }
                    }
                    if (!query.Contains("select"))
                    {
                        return false;
                        // wrong query
                    }
                    var columns = new List<string>();
                    int lastColumnSelectorIndex = hasLimit ? queryParts.Count() - 2 : queryParts.Count();
                    for (int i = 1; i < lastColumnSelectorIndex; i++) // get the columns we want to display
                    {
                        var columnName = queryParts[i].Trim(',');
                        if (!queryExecutor.ColumnNames.Contains(columnName)) //doesn't exist such column
                        {
                            return false;
                        }
                        columns.Add(columnName);
                    }
                    Console.Write(queryExecutor.Select(columns, limit));
                    break;
                }
                case 2:
                {
                    // Sum
                    string[] queryParts = query.Split(null);
                    if (queryParts.Length != 2
                        || queryParts[0].ToLower() != "sum"
                        || !queryExecutor.ColumnNames.Contains(queryParts[1])) //doesn't exist such column
                    {
                        executionStatus = false;
                    }
                    else
                    {
                        Console.Write(queryExecutor.Sum(queryParts[1]));
                    }
                    break;
                }
                case 3:
                {
                    //Show
                    if (query.Trim().ToLower() != "show") // wrong query
                    {
                        executionStatus = false;
                    }
                    else
                    {
                        Console.Write(queryExecutor.Show());
                    }
                    break;
                }
                case 4:
                {
                    //Find
                    string[] queryParts = query.Split(null);
                    if (queryParts.Length != 2
                        || queryParts[0].ToLower() != "find") // wrong query
                    {
                        executionStatus = false;
                    }
                    else
                    {
                        string searchText = queryParts[1].Trim('"');
                        searchText = searchText.Trim('\'');
                        Console.Write(queryExecutor.Find(searchText));
                    }
                    break;
                }
                default:
                    return false;
            }
            return executionStatus;
        }

        public static Dictionary<int, IDictionary<string, string>> ReadFile(out List<string> columnNames)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "\\fileName.txt");
            var csvFileHash = new Dictionary<int, IDictionary<string, string>>();
            // for every row, hash with column names and column values
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
