using System.Collections.Generic;
using System.Linq;

public class CsvTable
{
        private List<TableData> rows; // 파싱된 CSV 데이터의 각 행을 저장
    private List<string> headers;  // CSV 파일의 헤더(컬럼 이름)를 저장

    public CsvTable(string filePath)
    {
        rows = new List<TableData>(); // 각 행을 저장할 리스트 초기화
        var reader = new CSVReader(); // CsvReader 인스턴스 생성 (이전 CsvParser)
        var csvData = reader.Parse(filePath); // CSV 데이터를 파싱

        headers = csvData[0]; // 첫 번째 줄을 헤더로 저장

        // 나머지 줄을 데이터로 저장
        for (int i = 1; i < csvData.Count; i++)
        {
            var row = csvData[i]; // 현재 행 데이터
            var dataDictionary = new Dictionary<string, string>(); // 컬럼 이름과 값을 매핑할 사전

            // 각 컬럼을 헤더와 매핑
            for (int j = 0; j < headers.Count; j++)
            {
                dataDictionary[headers[j]] = row[j]; // 헤더를 키로 데이터를 저장
            }

            rows.Add(new TableData(dataDictionary)); // 각 행을 TableData로 변환하여 리스트에 추가
        }
    }

    // 특정 컬럼으로 데이터를 검색
    public List<TableData> GetByColumnSingle(string columnName, string value)
    {
        return rows.Where(row => row[columnName]?.Equals(value, System.StringComparison.OrdinalIgnoreCase) == true).ToList();
    }

    // 특정 컬럼으로 그룹화하여 데이터를 검색
    public List<List<TableData>> GetByColumnGroup(string columnName, string value)
    {
        return rows.Where(row => row[columnName]?.Equals(value, System.StringComparison.OrdinalIgnoreCase) == true)
                   .GroupBy(row => row[columnName]) // 그룹화
                   .Select(group => group.ToList()) // 그룹을 리스트로 변환
                   .ToList();
    }

    // 여러 컬럼으로 데이터를 그룹화하여 검색
    public List<List<TableData>> GetByMultipleColumnsGroup(params (string columnName, string value)[] columnValues)
    {
        return rows.Where(row => columnValues.All(cv => row[cv.columnName]?.Equals(cv.value, System.StringComparison.OrdinalIgnoreCase) == true))
                   .GroupBy(row => string.Join("|", columnValues.Select(cv => row[cv.columnName]))) // 각 그룹을 문자열로 묶음
                   .Select(group => group.ToList())
                   .ToList();
    }
}
