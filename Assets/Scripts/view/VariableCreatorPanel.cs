using domain.variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using view.parameter.variable;

namespace view
{
    public class VariableCreatorPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField variableName;
        [SerializeField] private TMP_InputField variableDefaultValue;

        [SerializeField] private CustomVariableView variableViewPrefab;
        [SerializeField] private RectTransform variableViewParent;

        private void Start()
        {
            foreach (var variable in VariablesManager.Instance.GetAllVariables())
            {
                CreateVariable(variable);
            }
            
            ClosePanel();
        }

        public void CreateVariable()
        {
            if (string.IsNullOrEmpty(variableName.text) || string.IsNullOrEmpty(variableDefaultValue.text))
                return;
            
            var variable = VariablesManager.Instance.AddVariable(variableName.text, int.Parse(variableDefaultValue.text));
            CreateVariable(variable);

            ClosePanel();
        }

        private void CreateVariable(Variable variable)
        {
            if (variable == null)
                return;
            
            var newVariable = Instantiate(variableViewPrefab, variableViewParent);
            newVariable.SetAsVariable(variable);
            newVariable.Setup(CommandsManager.Instance.Root, CommandsManager.Instance.Context);
            newVariable.IsCreator = true;
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(variableViewParent);
        }

        public void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}