using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace view.parameter
{
    public class DropdownSelectEvent : UnityEvent<int> {}
    public class DropdownView : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;

        public UnityEvent<int> OnSelectOption = new DropdownSelectEvent();

        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int index)
        {
            OnSelectOption.Invoke(index);
        }

        public void SetupDropdown(string[] options)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(options.ToList());
        }
    }
}