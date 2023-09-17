using System;
using System.Linq;
using domain.commands.executables;
using domain.commands.executables.variables;
using domain.parameter;
using domain.variables;
using UnityEngine;
using view.parameter;
using view.parameter.variable;
using Random = UnityEngine.Random;

namespace view.executable.commands.variables
{
    public class ChangeVariableByCommandView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        private VariablesDropdownParameter parameter;

        [SerializeField] private DropdownView dropdown;
        [SerializeField] private VariableSocket variable;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);
            
            DropdownParameterProvider<Variable> provider = new VariablesDropdownParameterProvider();
            parameter = new VariablesDropdownParameter(provider);
            
            dropdown.SetupDropdown(parameter.GetOptions().Select(x => x.Key).ToArray());
            dropdown.OnSelectOption.AddListener(i => parameter.SelectIndex(i));
            
            VariablesManager.Instance.OnVariablesUpdated += OnOnVariablesUpdated;
            
            command = new ChangeVariableByCommand(context, parameter, variable.Parameter);
        }

        private void OnOnVariablesUpdated(object sender, EventArgs e)
        {
            dropdown.SetupDropdown(parameter.GetOptions().Select(x => x.Key).ToArray());
        }
    }
}