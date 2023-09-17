using System;
using System.Collections.Generic;
using UnityEngine;

namespace domain.utils
{
    public class MainThreadDispatcher : MonoBehaviour
    {
        public static MainThreadDispatcher Instance { get; private set; }

        private readonly Queue<Action> actions = new Queue<Action>();

        private void Awake()
        {
            Instance = this;
        }

        public void EnqueueAction(Action action)
        {
            actions.Enqueue(action);
        }

        private void Update()
        {
            while (actions.Count > 0)
            {
                var action = actions.Dequeue();
                action.Invoke();
            }
        }
    }
}