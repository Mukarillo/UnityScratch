using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace view.parameter
{
    public abstract class ParameterView : BlockView
    {
        private ISocketBlock currentSocket;

        private Vector2 initialDragPosition;

        public void DetachAndMoveTo(Vector2 position)
        {
            transform.SetParent(root);
            transform.position = position;
            currentSocket = null;
        }
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            if (IsCreator)
                return;
            
            initialDragPosition = transform.position;
            
            DetachParameterFromSocket(currentSocket);
            
            transform.SetParent(root, true);
            transform.SetAsLastSibling();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            
            if (IsCreator)
                return;
            
            currentSocket?.HideHighlight();
            currentSocket = GetSocket(GetOverlappingSockets(GetOverlappingBlockView()));
            currentSocket?.ShowHighlight();
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            
            if (IsCreator || isDestroyed)
                return;
            
            currentSocket?.HideHighlight();

            AttachParameterToSocket(currentSocket);
        }

        private void AttachParameterToSocket(ISocketBlock socketBlock)
        {
            if (socketBlock == null)
                return;

            var block = socketBlock as BlockView;
            transform.SetParent(block.transform);
            RectTransform.anchoredPosition = Vector2.zero;
            
            AttachToSocket(block, initialDragPosition);
        }
        
        protected abstract void AttachToSocket(BlockView block, Vector2 initialPosition);
        
        private void DetachParameterFromSocket(ISocketBlock socketBlock)
        {
            if (socketBlock == null && currentSocket == null)
                return;

            DetachFromSocket(socketBlock);
            currentSocket = null;
        }

        protected abstract void DetachFromSocket(ISocketBlock socketBlock);

        private List<BlockView> GetOverlappingSockets(List<BlockView> overlappingBlockView)
        {
            return overlappingBlockView.OfType<ISocketBlock>().Cast<BlockView>().ToList();
        }

        protected abstract ISocketBlock GetSocket(List<BlockView> overlapping);
    }
}