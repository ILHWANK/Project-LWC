using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WHDle.Util
{
    using BackEnd;
    using Define;
    using System;
    using System.Reflection;
    using WHDle.Database.Dto;

    public static class SerializationUtil
    {
        public static T JsonToObject<T>(JsonData json, DeserializeType deserializeType) where T : class
        {
            T data = (T)Activator.CreateInstance(typeof(T));
            var fields = typeof(T).GetFields();

            foreach(var field in fields)
            {
                var identifier = field.Name;

                var isExists = json.Keys.Contains(identifier);

                if(!isExists)
                {
                    GameManager.ErrorLog($"### {identifier} Field of {typeof(T).Name} is Not Exist ###");
                    continue;
                }

                try
                {
                    string subKey = GetSubKey(field.FieldType, deserializeType);

                    var fieldData = subKey != null ?
                        json[identifier][subKey] : json[identifier];

                    TryParse(data, field, fieldData);
                }
                catch(Exception ex)
                {
                    GameManager.ErrorLog($"### Access a Key in {field.Name} Field in {typeof(T).Name} Failed ###\n {ex}");
                }
            }

            return data;
        }

        public static Param DtoToParam<T>(T dtoData) where T : DtoBase
        {
            var param = new Param();

            var fields = typeof(T).GetFields();

            foreach(var field in fields)
            {
                var fieldType = field.GetType();

                object value;
                if (fieldType.IsEnum)
                    value = field.GetValue(dtoData).ToString();
                else
                    value = field.GetValue(dtoData);

                param.Add(field.Name, value);
            }

            return param;
        }

        private static void TryParse<T>(T data, FieldInfo field, JsonData fieldData) where T : class
        {
            var type = field.FieldType;

            try
            {
                var stringData = fieldData.ToString();

                if (type == typeof(int))
                    field.SetValue(data, int.Parse(stringData));
                else if (type == typeof(string))
                    field.SetValue(data, stringData);
                else if (type == typeof(float))
                    field.SetValue(data, float.Parse(stringData));
                else if (type == typeof(double))
                    field.SetValue(data, double.Parse(stringData));
                else if (type == typeof(bool))
                    field.SetValue(data, bool.Parse(stringData));
                else if (type == typeof(DateTime))
                    field.SetValue(data, DateTime.Parse(stringData));
                else if (type.IsEnum)
                    field.SetValue(data, Enum.Parse(type, stringData));
                else if (type.IsArray)
                {
                    var arrayData = stringData.Split(',');
                    var elementType = type.GetElementType();

                    if (elementType == typeof(int))
                        field.SetValue(data, Array.ConvertAll(arrayData, i => int.Parse(i)));
                    else if (elementType == typeof(float))
                        field.SetValue(data, Array.ConvertAll(arrayData, f => float.Parse(f)));
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Field Set Faild = {type.Name}");
            }
        }

        private static string GetSubKey(Type fieldType, DeserializeType deserializeType)
        {
            switch (deserializeType)
            {
                case DeserializeType.SD:
                    return "S";
                case DeserializeType.DTO:
                    if (fieldType == typeof(string))
                        return "S";
                    else if (fieldType.IsGenericType)
                        return "L";
                    else if (fieldType == typeof(bool))
                        return "BOOL";
                    else if (fieldType == typeof(int))
                        return "N";
                    else
                        return "N";
                case DeserializeType.DefineDtoByBackend:
                    return null;
            }

            return null;
        }
    }
}
