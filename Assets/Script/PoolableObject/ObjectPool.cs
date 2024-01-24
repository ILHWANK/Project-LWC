using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WHDle.Util
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolableObject
    {
        private List<T> pool = new List<T>();

        public List<T> Pool => pool;

        public Transform holder;

        public bool canRecyle => pool.Find(pool => pool.CanRecyle) != null;

        public void RegistPoolableObject(T obj)
        {
            pool.Add(obj);
        }

        public T GetPoolableObject(Func<T, bool> pred)
        {
            if (!canRecyle)
            {
                var protoObj = pool.Where(pool => pool.name == typeof(T).Name).SingleOrDefault();

                for (int i = 0; i < pool.Count; i++) { Debug.Log($"pool Name i = " + pool[i].name); }

                if (protoObj != null)
                {
                    var newObj = GameObject.Instantiate(protoObj.gameObject, holder);
                    newObj.name = protoObj.name;
                    newObj.SetActive(true);
                    RegistPoolableObject(newObj.GetComponent<T>());
                }
                else
                {
                    return null;
                }
            }

            T recyleObject = pool.Find(pool => pred(pool) && pool.CanRecyle);

            Debug.Log($"RECYLEOBJECT = {recyleObject}");

            if (recyleObject == null)
                return null;

            recyleObject.OnRecyleStart?.Invoke();

            recyleObject.CanRecyle = false;

            return recyleObject;
        }
    }
}
