using domain.parameter.variable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace view.parameter.variable
{
    public class VariableSocket : BlockView, IVariableSocket
    {
        private VariableParameter parameter;
        public VariableParameter Parameter => parameter;
        
        protected override bool IsDraggable => false;
        
        [SerializeField] private GameObject highlight;
        [SerializeField] private TMP_InputField inputField;
        
        private DefaultVariableParameterProvider<float> defaultProvider;

        private VariableView attachedBlock;

        protected override void Awake()
        {
            base.Awake();
            
            defaultProvider = new DefaultVariableParameterProvider<float>(0);
            parameter = new VariableParameter();
            parameter.SetProvider(defaultProvider);
            
            HideHighlight();
        }
        
        private void OnEnable()
        {
            inputField.onValueChanged.AddListener(ChangeConstProviderValue);
        }

        private void OnDisable()
        {
            inputField.onValueChanged.RemoveListener(ChangeConstProviderValue);
        }

        private void ChangeConstProviderValue(string value)
        {
            if (!int.TryParse(value, out var intValue))
                intValue = 0;
            
            defaultProvider.SetValue(intValue);
        }

        public void ShowHighlight() 
        {
            highlight.SetActive(true);
        }

        public void HideHighlight()
        {
            highlight.SetActive(false);
        }

        public void RemoveParameter(Vector2 initialDragPosition)
        {
            attachedBlock.DetachAndMoveTo(initialDragPosition);
            
            attachedBlock.FixLayout();
            FixLayout();
        }

        public bool IsFilled()
        {
            return attachedBlock != null;
        }

        public void Attach(VariableView view, Vector2 initialDragPosition)
        {
            FixLayout();
            
            if(view != null)
                view.FixLayout();
            
            attachedBlock?.DetachAndMoveTo(initialDragPosition);
            
            attachedBlock = view;
            
            var provider = view?.Provider;
            inputField.gameObject.SetActive(provider == null);
            Parameter.SetProvider(provider ?? defaultProvider);
        }

        public void Detach()
        {
            FixLayout();
            attachedBlock = null;
            
            inputField.gameObject.SetActive(true);
            Parameter.SetProvider(defaultProvider);
        }
    }
}