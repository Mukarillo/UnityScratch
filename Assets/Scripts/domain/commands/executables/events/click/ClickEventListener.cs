using UnityEngine;
using UnityEngine.Events;

namespace domain.commands.executables.events.click
{
    public class ClickEventListener : MonoBehaviour
    {
        public UnityEvent OnClick = new UnityEvent();
        
        private void OnMouseDown()
        {
            OnClick?.Invoke();    
        }
    }
}