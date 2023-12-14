using BackEnd;
using BackEnd.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WHDle.Util
{
    public class GameManager : Singleton<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();

            if(transform.parent == null)
                DontDestroyOnLoad(gameObject);

            if (gameObject != null)
                SendQueue.StartSendQueue(true);
        }

        public void Update()
        {
            
        }
    }
}