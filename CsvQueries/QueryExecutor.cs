
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvQueries
{
    public class QueryExecutor
    {
        private readonly Dictionary<int, IDictionary<string, string>> csvFileHash;
        private readonly List<string> columnNames;

        public QueryExecutor(Dictionary<int, IDictionary<string, string>> csvFileHash, List<string> columnNames)
        {
            this.csvFileHash = csvFileHash;
            this.columnNames = columnNames;
        }

        public string Select(List<string> columns, int limit = Int32.MaxValue)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var columnNumber in csvFileHash.Keys.Where(p=>p < limit))
            {
                var currentRow = csvFileHash[columnNumber];
                foreach (var columnName in columns)
                {
                    strBuilder.AppendFormat("{0}|", currentRow[columnName]);
                }
                strBuilder.AppendLine();
            }
            return strBuilder.ToString();
        }

        public string Sum(string column)
        {
            int sum = csvFileHash.Keys.Sum(columnNumber => Convert.ToInt32(csvFileHash[columnNumber][column]));
            return sum.ToString();
        }

        public string Show()
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var columnName in columnNames)
            {
                strBuilder.AppendFormat("{0},", columnName);
            }
            return strBuilder.ToString(0, strBuilder.Length - 2); // lenght - 2 cuts the last comma
        }

        public string Find(string x)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var columnNumber in csvFileHash.Keys)
            {
                var currentRow = csvFileHash[columnNumber];
                foreach (var columnValue in currentRow.Values)
                {
                    if (columnValue.Contains(x))
                    {
                        foreach (var key in currentRow.Keys)
                        {
                            strBuilder.AppendFormat("{0}|", currentRow[key]);
                        }
                        strBuilder.AppendLine();
                        break;
                    }
                }
            }
            return strBuilder.ToString();
        }
    }
}
