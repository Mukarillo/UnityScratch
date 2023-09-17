using System.Collections.Generic;
using System.Linq;
using domain.parameter;
using UnityEngine;

namespace view.parameter.conditional
{
    public abstract class ConditionalView : ParameterView
    {
        public abstract ContidionalParameterProvider Provider { get; }

        protected override ISocketBlock GetSocket(List<BlockView> overlapping)
        {
            if (overlapping.Count == 0)
                return null;
            
            return (IConditionalSocket)GetClosest(overlapping.Where(x => x is IConditionalSocket));
        }

        protected override void AttachToSocket(BlockView socketBlock, Vector2 initialPosition)
        {
            var socket = socketBlock as ConditionalSocket;
            socket.Attach(this, initialPosition);
        }
    
        protected override void DetachFromSocket(ISocketBlock socketBlock)
        {
            var socket = socketBlock as ConditionalSocket;
            socket.Attach(null, Vector2.zero);
        }
    }
}