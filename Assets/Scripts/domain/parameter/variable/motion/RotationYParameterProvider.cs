using domain.commands;

namespace domain.parameter.variable.motion
{
    public class RotationYParameterProvider : BaseGameObjectParameterProvider
    {
        public RotationYParameterProvider(Context context) : base(context) { }
        
        public override float GetValue()
        {
            return context.GameObject.transform.rotation.eulerAngles.y;
        }
    }
}