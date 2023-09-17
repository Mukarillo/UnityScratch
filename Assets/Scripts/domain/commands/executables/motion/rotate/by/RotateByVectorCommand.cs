using Cysharp.Threading.Tasks;
using domain.parameter.variable;
using UnityEngine;

namespace domain.commands.executables.motion.rotate.by
{
    public class RotateByVectorCommand : BaseRotateCommand
    {
        private VariableParameter xRot;
        private VariableParameter yRot;
        private VariableParameter zRot;
        private VariableParameter duration;
        
        public RotateByVectorCommand(ExecutableContext context, VariableParameter xRot, VariableParameter yRot, VariableParameter zRot, VariableParameter duration) : base(context)
        {
            this.xRot = xRot;
            this.yRot = yRot;
            this.zRot = zRot;
            this.duration = duration;
        }

        public override async UniTask OnEnterAsync()
        {
            await RotateBy(new Vector3(xRot.GetValue(), yRot.GetValue(), zRot.GetValue()), duration.GetValue());
            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new RotateByVectorCommand(context, xRot, yRot, zRot, duration);
        }
    }
}