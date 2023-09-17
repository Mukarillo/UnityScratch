using domain.commands;

namespace domain.parameter.variable.motion
{
    public class PositionXParameterProvider : BaseGameObjectParameterProvider
    {
        public PositionXParameterProvider(Context context) : base(context) { }
        
        public override float GetValue()
        {
            return context.GameObject.transform.position.x;
        }
    }
}