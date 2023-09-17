using domain.commands;

namespace domain.parameter.variable.motion
{
    public class RotationXParameterProvider : BaseGameObjectParameterProvider
    {
        public RotationXParameterProvider(Context context) : base(context) { }
        
        public override float GetValue()
        {
            return context.GameObject.transform.rotation.eulerAngles.x;
        }
    }
}