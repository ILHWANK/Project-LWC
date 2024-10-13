using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader
{
    private List<Dictionary<string, string>> data;
    private string[] headers;

    public CSVReader(string filePath)
    {
        data = new List<Dictionary<string, string>>();
        LoadCSV(filePath);
    }

    private void LoadCSV(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found at {filePath}");
            return;
        }

        var lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Debug.LogError("CSV file is empty");
            return;
        }

        headers = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            var lineData = lines[i].Split(',');
            var entry = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length; j++)
            {
                entry[headers[j]] = lineData[j];
            }

            data.Add(entry);
        }
    }

    public List<Dictionary<string, string>> GetData()
    {
        return data;
    }

    public string[] GetHeaders()
    {
        return headers;
    }
}