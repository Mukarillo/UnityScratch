using Cysharp.Threading.Tasks;
using domain.parameter.variable;
using UnityEngine;

namespace domain.commands.executables.motion.move.@by
{
    public class MoveByVectorCommand : BaseMoveCommand
    {
        private VariableParameter xPos;
        private VariableParameter yPos;
        private VariableParameter zPos;
        private VariableParameter duration;
        
        public MoveByVectorCommand(ExecutableContext context, VariableParameter xPos, VariableParameter yPos, VariableParameter zPos, VariableParameter duration) : base(context)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
            this.duration = duration;
        }

        public override async UniTask OnEnterAsync()
        {
            await MoveBy(new Vector3(xPos.GetValue(), yPos.GetValue(), zPos.GetValue()), duration.GetValue());
            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new MoveByVectorCommand(context, xPos, yPos, zPos, duration);
        }
    }
}