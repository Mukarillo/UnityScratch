using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace domain.commands.executables.motion.move
{
    public abstract class BaseMoveCommand : ExecutableCommand
    {
        public BaseMoveCommand(ExecutableContext context) : base(context)
        {
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;

        protected virtual UniTask MoveTo(Vector3 position, float duration)
        {
            return Context.GameObject.transform.DOMove(position, duration).SetEase(Ease.Linear).ToUniTask().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        protected virtual UniTask MoveBy(Vector3 value, float duration)
        {
            var position = Context.GameObject.transform.position;
            position += value;

            return MoveTo(position, duration);
        }

        protected virtual UniTask MoveTo(int vectorIndex, float value, float duration)
        {
            var position = Context.GameObject.transform.position;
            position[vectorIndex] = value;

            return MoveTo(position, duration);
        }

        protected virtual UniTask MoveBy(int vectorIndex, float value, float duration)
        {
            var position = Context.GameObject.transform.position;
            position[vectorIndex] += value;

            return MoveTo(position, duration);
        }
    }
}