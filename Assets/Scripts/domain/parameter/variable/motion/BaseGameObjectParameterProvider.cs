using domain.commands;

namespace domain.parameter.variable.motion
{
    public abstract class BaseGameObjectParameterProvider : VariableParameterProvider<float>
    {
        protected Context context;
        
        public BaseGameObjectParameterProvider(Context context)
        {
            this.context = context;
        }
        
        public abstract float GetValue();
    }
}