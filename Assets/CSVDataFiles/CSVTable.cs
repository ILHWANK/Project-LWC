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
                row[headers[j]] = fields[j]; // 동적으로 컬럼을 추가
            }

            table.Add(row);
        }
    }

    // 단일 컬럼 값을 기준으로 row를 찾아 TableData로 반환
    public TableData GetByColumnSingle(string columnName, string value)
    {
        foreach (var row in table)
        {
            if (row.ContainsKey(columnName) && row[columnName] == value)
            {
                return new TableData(row); // row 데이터를 TableData로 반환
            }
        }
        return null; // 데이터가 없을 경우 null 반환
    }

    // 특정 컬럼 값으로 그룹화된 row들을 TableData 리스트로 반환
    public List<TableData> GetByColumnGroup(string columnName, string value)
    {
        List<TableData> results = new List<TableData>();

        foreach (var row in table)
        {
            if (row.ContainsKey(columnName) && row[columnName] == value)
            {
                results.Add(new TableData(row)); // 각 row를 TableData로 변환하여 리스트에 추가
            }
        }

        return results;
    }
}