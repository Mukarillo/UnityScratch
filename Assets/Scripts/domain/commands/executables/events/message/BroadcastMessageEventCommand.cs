using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using domain.utils;
using UnityEngine;

namespace domain.commands.executables.events.message
{
    public class BroadcastMessageEventCommand : EventCommand
    {
        private readonly MessagesDropdownParameter parameter;

        public BroadcastMessageEventCommand(ExecutableContext context, MessagesDropdownParameter parameter) :
            base(context)
        {
            this.parameter = parameter;
        }

        public override async UniTask OnEnterAsync()
        {
            MessageHandler.RaiseEvent(parameter.GetValue());
            Debug.LogWarning($"RAISE MESSAGE: {parameter.GetValue()}");

            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new BroadcastMessageEventCommand(context, parameter);
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;
    }
}