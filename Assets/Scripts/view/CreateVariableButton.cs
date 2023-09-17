using UnityEngine;

namespace view
{
    public class CreateVariableButton : MonoBehaviour
    {
        [SerializeField] private GameObject variableCreatorPanel;

        public void OpenPanel()
        {
            variableCreatorPanel.SetActive(true);
        }
    }
}
