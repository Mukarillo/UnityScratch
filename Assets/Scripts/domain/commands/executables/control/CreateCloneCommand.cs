using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using domain.utils;
using UnityEngine;

namespace domain.commands.executables.control
{
    public class CreateCloneCommand : ExecutableCommand
    {
        public CreateCloneCommand(ExecutableContext context) : base(context) { }

        public override async UniTask OnEnterAsync()
        {
            MainThreadDispatcher.Instance.EnqueueAction(ExecuteClone);

            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        private void ExecuteClone()
        {
            var target = GameObject.Instantiate(Context.GameObject);
            var newContext = new ExecutableContext(target, true)
            {
                id = Context.id + 1
            };
            newContext.SetRootCommands(ExecutableContext.CloneRootCommands(newContext));
            newContext.Execute();
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new CreateCloneCommand(context);
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;
    }
}