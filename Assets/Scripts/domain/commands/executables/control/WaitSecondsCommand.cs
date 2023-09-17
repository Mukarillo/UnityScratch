using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using domain.parameter.variable;

namespace domain.commands.executables.control
{
    public class WaitSecondsCommand : ExecutableCommand
    {
        private VariableParameter parameter;

        public WaitSecondsCommand(ExecutableContext context, VariableParameter parameter) : base(context)
        {
            this.parameter = parameter;
        }

        public override async UniTask OnEnterAsync()
        {
            await UniTask.Delay((int)parameter.GetValue() * 1000);
            await ExecuteNextCommand();
        }

        public override async UniTask OnExitAsync()
        {
        }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new WaitSecondsCommand(context, parameter);
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;
    }
}