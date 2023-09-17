using domain.commands.executables.events.message;
using TMPro;
using UnityEngine;

namespace view
{
    public class MessageCreatorPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField messageName;

        private void Start()
        {
            ClosePanel();
        }

        public void CreateMessage()
        {
            if (string.IsNullOrEmpty(messageName.text))
                return;
            
            MessagesManager.Instance.AddMessage(messageName.text);

            ClosePanel();
        }

        public void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}