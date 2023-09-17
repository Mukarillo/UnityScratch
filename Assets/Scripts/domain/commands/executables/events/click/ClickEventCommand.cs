using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using domain.utils;
using UnityEngine;

namespace domain.commands.executables.events.click
{
    public class ClickEventCommand : EventCommand
    {
        private ClickEventListener listener;
        private UniTaskCompletionSource<bool> clickTask;

        public ClickEventCommand(ExecutableContext context) : base(context) { }

        public override Connection Connection => Connection.Bottom;

        protected override void Initialize() { }
        
        public override async UniTask OnEnterAsync()
        {
            if (listener == null)
                listener = Context.GameObject.AddComponent<ClickEventListener>();
            
            clickTask = new UniTaskCompletionSource<bool>();
            listener.OnClick.AddListener(OnClick);

            ExecutableContext.CancellationToken.Token.Register(() =>
            {
                clickTask.TrySetCanceled();
            });
            await clickTask.Task;

            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
            OnEnterAsync().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync()
        {
            listener.OnClick.RemoveListener(OnClick);
        }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new ClickEventCommand(context);
        }

        private void OnClick()
        {
            clickTask.TrySetResult(true);
        }

        public override void Destroy()
        {
            GameObject.Destroy(listener);
        }
    }
}