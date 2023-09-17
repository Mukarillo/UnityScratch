using domain.parameter.variable;

namespace view.parameter.variable
{
    public interface IVariableSocket : ISocketBlock
    {
        VariableParameter Parameter { get; }
    }
}