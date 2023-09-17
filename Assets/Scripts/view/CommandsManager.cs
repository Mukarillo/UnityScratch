using System.Collections;
using System.Collections.Generic;
using domain.commands.executables;
using UnityEngine;
using view.executable;

namespace view
{
    public class CommandsManager : MonoBehaviour
    {
        private static CommandsManager instance;
        public static CommandsManager Instance => instance;
        
        public RectTransform Root => root;
        public ExecutableContext Context => context;

        [SerializeField] private BlockView[] blocks;
        [SerializeField] private GameObject editModeUI;
        [SerializeField] private GameObject gameModeUI;
        [SerializeField] private RectTransform root;
        [SerializeField] private GameObject target;

        private ExecutableContext context;

        private Vector3 initialPosition;
        private Quaternion initialRotation;
        private Vector3 initialScale;
        
        private void Awake()
        {
            instance = this;
            context = new ExecutableContext(target);

            initialPosition = target.transform.position;
            initialRotation = target.transform.rotation;
            initialScale = target.transform.localScale;

            editModeUI.SetActive(true); 
            gameModeUI.SetActive(false);
            
            foreach (var block in blocks)
            {
                block.IsCreator = true;
                block.Setup(root, context);
            }
        }

        public void Execute()
        {
            editModeUI.SetActive(false);
            gameModeUI.SetActive(true);
            
            var rootExecutables = new List<ExecutableCommand>();
            foreach (Transform child in root)
            {
                if (child.TryGetComponent<ExecutableView>(out var executable))
                {
                    rootExecutables.Add(executable.Command);
                }
            }
            
            context.SetRootCommands(rootExecutables);
            
            context.Execute();
        }

        public void Stop()
        {
            context.Stop();
            
            editModeUI.SetActive(true);
            gameModeUI.SetActive(false);

            StartCoroutine(WaitAndResetTarget());
        }

        private IEnumerator WaitAndResetTarget()
        {
            yield return new WaitForSeconds(0.1f);
            
            target.transform.position = initialPosition;
            target.transform.rotation = initialRotation;
            target.transform.localScale = initialScale;
        }
    }
}