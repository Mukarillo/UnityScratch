using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using domain.utils;
using UnityEngine;

namespace domain.commands.executables.events.keypressed
{
    public class KeyPressedEventCommand : EventCommand
    {
        private readonly ValidKeysDropdownParameter parameter;
        private KeyPressedEventListener listener;
        private UniTaskCompletionSource<bool> keyPressedTask;

        public KeyPressedEventCommand(ExecutableContext context, ValidKeysDropdownParameter parameter) : base(context)
        {
            this.parameter = parameter;
        }

        public override Connection Connection => Connection.Bottom;

        protected override void Initialize()
        {
            
        }
        
        public override async UniTask OnEnterAsync()
        {
            if(listener == null)
                listener = Context.GameObject.AddComponent<KeyPressedEventListener>();
        
            keyPressedTask = new UniTaskCompletionSource<bool>();
            listener.SetParameter(parameter);
            listener.OnKeyPressed.AddListener(OnKeyPressed);

            ExecutableContext.CancellationToken.Token.Register(() =>
            {
                keyPressedTask.TrySetCanceled();
            });
            await keyPressedTask.Task;

            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
            OnEnterAsync().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync()
        {
            listener.OnKeyPressed.RemoveListener(OnKeyPressed);
        }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new KeyPressedEventCommand(context, parameter);
        }

        private void OnKeyPressed()
        {
            keyPressedTask.TrySetResult(true);
        }

        public override void Destroy()
        {
            GameObject.Destroy(listener);
        }
    }
}