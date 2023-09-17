using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using domain.parameter;
using domain.utils;

namespace domain.commands.executables.control
{
    public class WaitUntilCommand : ExecutableCommand
    {
        private readonly ConditionalParameter parameter;
        private UniTaskCompletionSource<bool> validateParameterTask;

        public WaitUntilCommand(ExecutableContext context, ConditionalParameter parameter) : base(context)
        {
            this.parameter = parameter;
        }

        public override async UniTask OnEnterAsync()
        {
            validateParameterTask = new UniTaskCompletionSource<bool>();

            UnityMethods.Instance.UpdateEvent.AddListener(ValidateParameter);

            ExecutableContext.CancellationToken.Token.Register(() =>
            {
                validateParameterTask.TrySetCanceled();
            });
            await validateParameterTask.Task;
            

            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        private void ValidateParameter()
        {
            if (parameter.GetValue())
                validateParameterTask.TrySetResult(true);
        }

        public override async UniTask OnExitAsync()
        {
            UnityMethods.Instance.UpdateEvent.RemoveListener(ValidateParameter);
        }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new WaitUntilCommand(context, parameter);
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;

        public override void Destroy()
        {
            UnityMethods.Instance.UpdateEvent.RemoveListener(ValidateParameter);
        }
    }
}