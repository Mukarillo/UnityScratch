using UnityEngine;

namespace view
{
    public class Trash : MonoBehaviour
    {
        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}