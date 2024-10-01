using System.Collections.Generic;

public class TableData
{
    // 모든 컬럼 데이터를 저장하는 Dictionary
    private Dictionary<string, string> rowData;

    // 생성자에서 데이터를 설정
    public TableData(Dictionary<string, string> row)
    {
        rowData = new Dictionary<string, string>(row); // 모든 컬럼을 동적으로 저장
    }

    // 특정 컬럼의 값을 가져오기 위한 인덱서 (동적 컬럼 접근)
    public string this[string columnName]
    {
        get
        {
            if (rowData.ContainsKey(columnName))
            {
                return rowData[columnName];
            }
            return null; // 컬럼이 없을 경우 null 반환
        }
    }

    // 데이터가 어떤 컬럼을 가지고 있는지 확인하기 위해 컬럼 이름을 반환하는 메소드
    public IEnumerable<string> GetColumnNames()
    {
        return rowData.Keys; // 동적 컬럼 이름 목록 반환
    }
}