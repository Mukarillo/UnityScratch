using UnityEngine;

namespace view
{
    public class CreateMessageButton : MonoBehaviour
    {
        [SerializeField] private GameObject messageCreatorPanel;

        public void OpenPanel()
        {
            messageCreatorPanel.SetActive(true);
        }
    }
}
