using domain.commands;

namespace domain.parameter.variable.motion
{
    public class PositionZParameterProvider : BaseGameObjectParameterProvider
    {
        public PositionZParameterProvider(Context context) : base(context) { }
        
        public override float GetValue()
        {
            return context.GameObject.transform.position.z;
        }
    }
}