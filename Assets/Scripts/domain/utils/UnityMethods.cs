using UnityEngine;
using UnityEngine.Events;

namespace domain.utils
{
    public class UnityMethods : MonoBehaviour
    {
        public static UnityMethods Instance { get; private set; }

        public UnityEvent UpdateEvent = new UnityEvent();
        
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            UpdateEvent.Invoke(); 
        }
    }
}