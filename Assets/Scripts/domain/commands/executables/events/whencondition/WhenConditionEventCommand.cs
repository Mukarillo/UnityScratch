using Cysharp.Threading.Tasks;
using domain.parameter;
using domain.utils;

namespace domain.commands.executables.events.whencondition
{
    public class WhenConditionEventCommand : EventCommand
    {
        private readonly ConditionalParameter parameter;
        private UniTaskCompletionSource<bool> validateParameterTask;

        public WhenConditionEventCommand(ExecutableContext context, ConditionalParameter parameter) : base(context)
        {
            this.parameter = parameter;
        }

        public override Connection Connection => Connection.Bottom;
        protected override void Initialize() { }

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
            OnEnterAsync().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
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
            return new WhenConditionEventCommand(context, parameter);
        }

        public override void Destroy()
        {
            UnityMethods.Instance.UpdateEvent.RemoveListener(ValidateParameter);
        }
    }
}