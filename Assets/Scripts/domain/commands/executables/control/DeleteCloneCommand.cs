using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using domain.utils;

namespace domain.commands.executables.control
{
    public class DeleteCloneCommand : ExecutableCommand
    {
        public DeleteCloneCommand(ExecutableContext context) : base(context) { }

        public override async UniTask OnEnterAsync()
        {
            ExecutableContext.Dispose();
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new DeleteCloneCommand(context);
        }

        public override Connection Connection => Connection.Top;
    }
}