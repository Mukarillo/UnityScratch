using domain.parameter;
using UnityEngine;
using UnityEngine.UI;

namespace view.parameter.conditional
{
    public class ConditionalSocket : BlockView, IConditionalSocket
    {
        [SerializeField] private GameObject highlight;

        private ConditionalParameter parameter;
        public ConditionalParameter Parameter => parameter;

        private ConditionalView attachedBlock;

        protected override bool IsDraggable => false;

        protected override void Awake()
        {
            base.Awake();
            
            parameter = new ConditionalParameter();
            
            HideHighlight();
        }

        public void ShowHighlight()
        {
            highlight.SetActive(true);
        }

        public void HideHighlight()
        {
            highlight.SetActive(false);
        }

        public void Attach(ConditionalView view, Vector2 initialDragPosition)
        {
            FixLayout();

            if (view == null)
                return;

            view.FixLayout();
            
            attachedBlock?.DetachAndMoveTo(initialDragPosition);
            
            attachedBlock = view;
            var provider = view.Provider;
            
            Parameter.SetProvider(provider);
        }
    }
}