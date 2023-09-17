using domain.commands;

namespace domain.parameter.variable.motion
{
    public class PositionYParameterProvider : BaseGameObjectParameterProvider
    {
        public PositionYParameterProvider(Context context) : base(context) { }
        
        public override float GetValue()
        {
            return context.GameObject.transform.position.y;
        }
    }
}