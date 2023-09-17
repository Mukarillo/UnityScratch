using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace view.executable
{
    public class SocketUpdateEvent : UnityEvent<ExecutableView> {}
    public class ExecutableSocket : BlockView
    {
        protected override bool IsDraggable => false;

        public bool IsFilled => ExecutableView != null;
        public ExecutableView ExecutableView { get; private set; }

        public UnityEvent<ExecutableView> OnSocketUpdate = new SocketUpdateEvent();

        public BlockView Owner { get; private set; }

        public void SetOwner(BlockView blockView)
        {
            Owner = blockView;
        }

        public void Attach(ExecutableView executableView)
        {
            executableView.transform.SetParent(transform);
            SetExecutableViewPosition(executableView);
            ExecutableView = executableView;
            
            OnSocketUpdate.Invoke(executableView);
            
            FixLayout();
            executableView.FixLayout();
        }

        private void SetExecutableViewPosition(ExecutableView executableView)
        {
            if (executableView == null)
                return;
            
            var totalHeight = GetHeight(executableView) / 2f;
            var height = executableView.RectTransform.sizeDelta.y / 2f;
            var offset = -2f * GetChainSize(executableView);
            executableView.RectTransform.anchoredPosition = new Vector2(0, totalHeight - height + offset);
        }

        private int GetChainSize(ExecutableView executableView)
        {
            var view = executableView;
            var amount = 0;
            while (view.AttachedBottom)
            {
                view = view.AttachedBottom;
                amount++;
            }

            return amount;
        }
        
        private float GetHeight(ExecutableView executableView)
        {
            return executableView.RectTransform.sizeDelta.y + 
                   (executableView.AttachedBottom ? GetHeight(executableView.AttachedBottom) : 0f);
        }

        public void Detach()
        {
            FixLayout();
            ExecutableView.FixLayout();
            
            ExecutableView = null;
            OnSocketUpdate.Invoke(null);
        }

        protected override IEnumerator InternalFixLayout()
        {
            yield return base.InternalFixLayout();
            Owner.FixLayout();

            SetExecutableViewPosition(ExecutableView);
        }
    }
}