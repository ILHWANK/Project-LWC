using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WHDle.UI.Inventory;
using WHDle.Util.Define;

namespace WHDle.Util
{
    public class ResourcesManager : Singleton<ResourcesManager>
    {
        private int registCount = 0;
        private int maxRegistCount = 0;

        public GameObject LoadObject(string path)
            => Resources.Load<GameObject>(path);

        public void RegistAllPoolableObject()
        {
            registCount = 0;
            maxRegistCount = 1;

            LoadPoolableObject<Slot>(PoolType.Slot, "Prefabs/Inventory/Slot", 100, CompleteRegist);
        }

        private void CompleteRegist()
        {
            ++registCount;

            if (registCount == maxRegistCount)
                GameManager.Instance.TitleController.LoadComplete = true;
        }

        public void LoadPoolableObject<T>(PoolType poolType, string path, int poolCount = 1, Action complete = null)
            where T : MonoBehaviour, IPoolableObject
        {
            var obj = LoadObject(path);
            var tComponent = obj.GetComponent<T>();

            ObjectPoolManager.Instance.RegistPool(poolType, tComponent, poolCount);

            complete?.Invoke();
        }
    }
}
