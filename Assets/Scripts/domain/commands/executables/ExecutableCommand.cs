using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace domain.commands.executables
{
    public abstract class ExecutableCommand : Command
    {
        public ExecutableContext ExecutableContext => (ExecutableContext)Context;
        public ExecutableCommand nextCommand;
        
        public abstract UniTask OnEnterAsync();
        public abstract UniTask OnExitAsync();

        protected ExecutableCommand(ExecutableContext context) : base(context)
        {
        }
        
        public void SetNextCommand(ExecutableCommand command)
        {
            nextCommand = command;
        }

        public async UniTask Execute()
        {
            await OnEnterAsync();
        }
        
        protected async UniTask ExecuteNextCommand()
        {
            if (ExecutableContext.IsDisposed)
                return;
            
            await OnExitAsync();
            
            if(nextCommand != null)
                await nextCommand.Execute();
        }

        public ExecutableCommand Clone(ExecutableContext context)
        {
            var clone = InternalClone(context);
            if(nextCommand != null)
                clone.SetNextCommand(nextCommand.Clone(context));
            return clone;
        }

        protected abstract ExecutableCommand InternalClone(ExecutableContext context);
    }
}