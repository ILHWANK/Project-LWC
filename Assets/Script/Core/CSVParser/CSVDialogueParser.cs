using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVDialogueParser
{
    public static CSVTable LoadDialogueTable(string filePath)
    {
        // 파일 경로에서 데이터를 읽어옴
        var csvReader = new CSVReader(filePath);
        return new CSVTable(csvReader);
    }
}