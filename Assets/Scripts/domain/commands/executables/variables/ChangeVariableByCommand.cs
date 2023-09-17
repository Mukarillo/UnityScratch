using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using domain.parameter.variable;
using domain.utils;
using UnityEngine;

namespace domain.commands.executables.variables
{
    public class ChangeVariableByCommand : ExecutableCommand
    {
        private readonly VariablesDropdownParameter parameter;
        private readonly VariableParameter variable;

        public ChangeVariableByCommand(ExecutableContext context, VariablesDropdownParameter parameter, VariableParameter variable) : base(context)
        {
            this.parameter = parameter;
            this.variable = variable;
        }

        public override async UniTask OnEnterAsync()
        {
            var variable = parameter.GetValue();
            variable.ChangeValue(variable.Provider.GetValue() + this.variable.GetValue());

            Debug.LogWarning($"CHANGED VARIABLE {variable.Key}: {variable.Provider.GetValue()}");

            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new ChangeVariableByCommand(context, parameter, variable);
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;
    }
}