using System.ComponentModel;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace domain.commands.executables.motion.rotate
{
    public abstract class BaseRotateCommand : ExecutableCommand
    {
        public BaseRotateCommand(ExecutableContext context) : base(context)
        {
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;

        protected virtual UniTask RotateTo(Vector3 value, float duration)
        {
            return Context.GameObject.transform.DORotate(value, duration).SetEase(Ease.Linear).ToUniTask().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }
        
        protected virtual UniTask RotateTo(int axisIndex, float value, float duration)
        {
            var rotation = Context.GameObject.transform.rotation.eulerAngles;
            rotation[axisIndex] = value;

            return RotateTo(rotation, duration);
        }

        protected virtual UniTask RotateBy(Vector3 value, float duration)
        {
            var rotation = Context.GameObject.transform.rotation.eulerAngles;
            rotation += value;

            return RotateTo(rotation, duration);
        }
        
        protected virtual UniTask RotateBy(int axisIndex, float value, float duration)
        {
            var rotation = Context.GameObject.transform.rotation.eulerAngles;
            rotation[axisIndex] += value;

            return RotateTo(rotation, duration);
        }
    }
}