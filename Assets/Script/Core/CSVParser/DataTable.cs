using System.Collections.Generic;

public class TableData
{
    private Dictionary<string, string> data; // 컬럼 이름과 값을 매핑한 사전

    // 생성자: 컬럼과 데이터 사전을 받아 초기화
    public TableData(Dictionary<string, string> data)
    {
        this.data = data;
    }

    // 인덱서를 사용해 컬럼 이름으로 데이터에 접근 가능
    public string this[string columnName]
    {
        get
        {
            data.TryGetValue(columnName, out string value); // 컬럼 이름으로 값 찾기
            return value; // 찾은 값 반환 (없으면 null)
        }
    }
}