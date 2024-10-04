using System.Collections.Generic;
using System.IO;

public class CsvTable
{
    private List<Dictionary<string, string>> table = new List<Dictionary<string, string>>();

    // CSV 파일을 읽어 테이블로 파싱
    public void ReadCsv(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        string[] headers = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');
            Dictionary<string, string> row = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length; j++)
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
        List<TableData> results = new List<TableData>();

        foreach (var row in table)
        {
            bool match = true;

            // 모든 조건을 만족해야 함
            foreach (var criteria in columnCriteria)
            {
                string columnName = criteria.Key;
                string expectedValue = criteria.Value;

                if (!row.ContainsKey(columnName) || row[columnName] != expectedValue)
                {
                    match = false;
                    break; // 조건이 하나라도 맞지 않으면 중단
                }
            }

            // 조건이 모두 맞는 경우에만 결과 리스트에 추가
            if (match)
            {
                results.Add(new TableData(row));
            }
        }

        return results;
    }
}
