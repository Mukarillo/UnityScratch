using UnityEngine;

namespace domain.commands
{
    public class Context
    {
        public int id = 0;
        public GameObject GameObject { get; }

        public Context(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}