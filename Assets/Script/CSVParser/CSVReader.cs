using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    // CSV 파일을 파싱하는 메소드, 각 행을 문자열 리스트로 반환
    public List<List<string>> Parse(string filePath)
    {
        var data = new List<List<string>>(); // CSV 데이터를 저장할 리스트

        // CSV 파일을 한 줄씩 읽어옴
        using var reader = new StreamReader(filePath);
        while (reader.ReadLine() is { } line)
        {
            // 각 줄을 ','로 분리하여 리스트에 저장
            var fields = line.Split(','); 
            data.Add(new List<string>(fields)); // 데이터를 리스트로 변환하여 추가
        }

        return data; // 파싱한 데이터 반환
    }
}
