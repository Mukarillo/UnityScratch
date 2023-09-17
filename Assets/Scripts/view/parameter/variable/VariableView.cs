using System.Collections.Generic;
using System.Linq;
using domain.parameter.variable;
using UnityEngine;

namespace view.parameter.variable
{
    public abstract class VariableView : ParameterView
    {
        public abstract VariableParameterProvider<float> Provider { get; }

        protected override ISocketBlock GetSocket(List<BlockView> overlapping)
        {
            if (overlapping.Count == 0)
                return null;
            
            return (ISocketBlock)GetClosest(overlapping.Where(x => x is IVariableSocket));
        }

        protected override void AttachToSocket(BlockView socketBlock, Vector2 initialPosition)
        {
            var socket = socketBlock as VariableSocket;
            socket.Attach(this, initialPosition);
        }
    
        protected override void DetachFromSocket(ISocketBlock socketBlock)
        {
            var socket = socketBlock as VariableSocket;
            socket.Detach();
        }
    }
}
