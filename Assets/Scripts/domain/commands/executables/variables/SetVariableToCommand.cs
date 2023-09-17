using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using domain.parameter.variable;

namespace domain.commands.executables.variables
{
    public class SetVariableToCommand : ExecutableCommand
    {
        private readonly VariablesDropdownParameter parameter;
        private readonly VariableParameter variable;

        public SetVariableToCommand(ExecutableContext context, VariablesDropdownParameter parameter, VariableParameter variable) : base(context)
        {
            this.parameter = parameter;
            this.variable = variable;
        }

        public override async UniTask OnEnterAsync()
        {
            var variable = parameter.GetValue();
            variable.ChangeValue(this.variable.GetValue());
            
            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new SetVariableToCommand(context, parameter, variable);
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;
    }
}