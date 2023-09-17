using Cysharp.Threading.Tasks;
using UnityEngine;

namespace domain.commands.executables.events.message
{
    public class ReceiveMessageEventCommand : EventCommand
    {
        private readonly MessagesDropdownParameter parameter;
        private UniTaskCompletionSource<bool> messageReceivedTask;
        
        public ReceiveMessageEventCommand(ExecutableContext context, MessagesDropdownParameter parameter) : base(context)
        {
            this.parameter = parameter;
        }
        
        public override async UniTask OnEnterAsync()
        {
            messageReceivedTask = new UniTaskCompletionSource<bool>();
            MessageHandler.EvtHandler += OnReceiveMessage;

            ExecutableContext.CancellationToken.Token.Register(() =>
            {
                messageReceivedTask.TrySetCanceled();
            });
            await messageReceivedTask.Task;
            
            Debug.LogWarning($"RECEIVED MESSAGE: {parameter.GetValue()}");

            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
            OnEnterAsync().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }
        
        private void OnReceiveMessage(MessageEventArgs args)
        {
            if (!args.message.Value.Equals(parameter.GetValue().Value))
                return;

            messageReceivedTask.TrySetResult(true);
        }

        public override async UniTask OnExitAsync()
        {
            MessageHandler.EvtHandler -= OnReceiveMessage;
        }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new ReceiveMessageEventCommand(context, parameter);
        }

        public override Connection Connection => Connection.Bottom;

        public override void Destroy()
        {
            MessageHandler.EvtHandler -= OnReceiveMessage;
        }
    }
}