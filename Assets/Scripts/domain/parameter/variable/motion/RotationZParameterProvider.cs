using domain.commands;

namespace domain.parameter.variable.motion
{
    public class RotationZParameterProvider : BaseGameObjectParameterProvider
    {
        public RotationZParameterProvider(Context context) : base(context) { }
        
        public override float GetValue()
        {
            return context.GameObject.transform.rotation.eulerAngles.z;
        }
    }
}