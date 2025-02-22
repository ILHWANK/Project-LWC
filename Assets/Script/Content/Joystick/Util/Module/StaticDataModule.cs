/*using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WHDle.Database.SD
{
    using Util;
    using Util.Define;

    [Serializable]
    public class StaticDataModule
    {
        public List<SDMake> sdMakes = new();
        public List<SDPlace> sdPlaces = new();
        public List<SDPlaceItem> sdPlaceItems = new();

        public void Initialize()
        {
            var loader = new StaticDataLoader(this);

            GameManager.Instance.StartCoroutine(WaitForLoadComplete());
            loader.LoadAllData();

            IEnumerator WaitForLoadComplete()
            {
                yield return new WaitUntil(() => loader.allLoaded);

                GameManager.Instance.TitleController.LoadComplete = true;
            }
        }

        private class StaticDataLoader
        {
            public bool allLoaded;
            public int currentLoadedCount;
            public const int maxLoadedCount = (int)SDType.End;

            public StaticDataModule module;

            public StaticDataLoader(StaticDataModule module)
            {
                this.module = module;
            }

            public void LoadAllData()
            {
                LoadDataIds();
            }

            private void LoadDataIds()
            {
                Backend.Chart.GetChartListV2(callback =>
                {
                    if (callback.IsSuccess())
                    {
                        var rows = callback.Rows();
                        for (int i = 0; i < rows.Count; i++)
                        {
                            if (!rows[i]["selectedChartFileId"].ContainsKey("N"))
                                continue;

                            var dataId = Convert.ToString(rows[i]["selectedChartFileId"]["N"]);

                            var dataName = Convert.ToString(rows[i]["chartName"]["S"]);

                            MatchAndLoad(dataId, dataName);
                        }
                    }
                    else
                        GameManager.Log($"### Load All Table Id Failed ###\n{callback}");
                });
            }

            private void MatchAndLoad(string dataId, string dataName)
            {
                SDType type = (SDType)Enum.Parse(typeof(SDType), dataName);

                switch (type)
                {
                    case SDType.Make:
                        LoadData(dataId, module.sdMakes);
                        break;
                    case SDType.Place:
                        LoadData(dataId, module.sdPlaces);
                        break;
                    case SDType.PlaceItem:
                        LoadData(dataId, module.sdPlaceItems);
                        break;
                    case SDType.End:
                        break;
                    default:
                        break;
                }
            }

            private void LoadData<T>(string charId, List<T> data) where T : StaticBase
            {
                Backend.Chart.GetChartContents(charId, callback =>
                {
                    if (callback.IsSuccess())
                    {
                        var rows = callback.Rows();

                        for (int i = 0; i < rows.Count; ++i)
                        {
                            data.Add(SerializationUtil.JsonToObject<T>(rows[i], DeserializeType.SD));
                        }

                        CheckLoadedCount();
                    }
                    else
                    {
                        GameManager.Log($"### Load {typeof(T).Name} Table Failed ###\n{callback}");
                    }
                });
            }

            private void CheckLoadedCount()
            {
                ++currentLoadedCount;

                if (currentLoadedCount >= maxLoadedCount)
                    allLoaded = true;
            }
        }
    }
}*/