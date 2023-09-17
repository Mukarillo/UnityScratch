using UnityEngine;
using UnityEngine.Events;

namespace domain.commands.executables.events.keypressed
{
    public class KeyPressedEventListener : MonoBehaviour
    {
        public UnityEvent OnKeyPressed = new UnityEvent();
        
        private ValidKeysDropdownParameter parameter;

        public void SetParameter(ValidKeysDropdownParameter parameter)
        {
            this.parameter = parameter;
        }

        private void Update()
        {
            if (parameter == null || OnKeyPressed == null)
                return;

            var expectedKey = parameter.GetValue();

            if (expectedKey == ValidKeys.Any)
            {
                if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0)
                                          || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
                {
                    OnKeyPressed.Invoke();
                }
            }
            else
            {
                if (Input.GetKeyDown(expectedKey.ToKeyCode()))
                {
                    OnKeyPressed.Invoke();
                }
            }
        }
    }
}