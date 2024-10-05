using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CsvTable
{
    private List<Dictionary<string, string>> table = new List<Dictionary<string, string>>();
    
    public void ReadCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var headers = lines[0].Split(',');

        for (var i = 1; i < lines.Length; i++)
        {
            var fields = lines[i].Split(',');
            var row = new Dictionary<string, string>();

            for (var j = 0; j < headers.Length; j++)
            {
                row[headers[j]] = fields[j];
            }

            table.Add(row);
        }
    }

    public List<TableData> GetByColumnGroup(string columnName, string value)
    {
        List<TableData> results = new List<TableData>();

        foreach (var row in table)
        {
            if (row.ContainsKey(columnName) && row[columnName] == value)
            {
                results.Add(new TableData(row));
            }
        }

        return results;
    }

    public List<TableData> GetByMultipleColumnsGroup(Dictionary<string, string> columnCriteria)
    {
        return (from row in table 
            let match = !(from criteria 
                in columnCriteria 
                let columnName = criteria.Key 
                let expectedValue = criteria.Value 
                where !row.ContainsKey(columnName) || row[columnName] != expectedValue 
                select columnName).Any() where match select new TableData(row)).ToList();
    }
}
