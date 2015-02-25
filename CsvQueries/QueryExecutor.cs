
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvQueries
{
    public class QueryExecutor
    {
        private readonly Dictionary<int, IDictionary<string, string>> csvFileHash;
        public List<string> ColumnNames { private set; get; }

        public QueryExecutor(Dictionary<int, IDictionary<string, string>> csvFileHash, List<string> columnNames)
        {
            this.csvFileHash = csvFileHash;
            this.ColumnNames = columnNames;
        }

        public string Select(List<string> columns, int limit = Int32.MaxValue)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var rowNumber in csvFileHash.Keys.Where(p=>p <= limit))
            {
                var currentRow = csvFileHash[rowNumber];
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
            int sum = 0;
            foreach (var rowNumber in csvFileHash.Keys)
            {
                int value = 0;
                Int32.TryParse(csvFileHash[rowNumber][column], out value);
                sum += value;
            }
            return sum.ToString();
        }

        public string Show()
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var columnName in ColumnNames)
            {
                strBuilder.AppendFormat("{0},", columnName);
            }
            return strBuilder.ToString(0, strBuilder.Length - 2); // lenght - 2 cuts the last comma
        }

        public string Find(string x)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var rowNumber in csvFileHash.Keys)
            {
                var currentRow = csvFileHash[rowNumber];
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
            if (strBuilder.Length == 0)
            {
                strBuilder.Append("No result found");
            }
            return strBuilder.ToString();
        }
    }
}
