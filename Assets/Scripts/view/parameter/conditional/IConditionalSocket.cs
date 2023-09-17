using domain.parameter;

namespace view.parameter.conditional
{
    public interface IConditionalSocket : ISocketBlock
    {
        ConditionalParameter Parameter { get; }
    }
}