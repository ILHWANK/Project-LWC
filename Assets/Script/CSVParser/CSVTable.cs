using System.Collections.Generic;

public class CSVTable
{
    private List<Dictionary<string, string>> rows;

    public CSVTable(CSVReader csvReader)
    {
        rows = csvReader.GetData();
    }

    public List<Dictionary<string, string>> GetByMultipleColumnsGroup((string column, string value)[] groupMap)
    {
        var filteredRows = new List<Dictionary<string, string>>(rows);

        foreach (var (column, value) in groupMap)
        {
            filteredRows = filteredRows.FindAll(row => row[column] == value);
        }

        return filteredRows;
    }

    public List<Dictionary<string, string>> GetByColumnGroup(string column, string value)
    {
        return rows.FindAll(row => row[column] == value);
    }
}