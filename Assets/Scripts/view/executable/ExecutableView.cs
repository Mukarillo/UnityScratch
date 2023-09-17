using System.Collections;
using System.Collections.Generic;
using System.Linq;
using domain.commands;
using domain.commands.executables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace view.executable
{
    public abstract class ExecutableView : BlockView
    {
        public abstract ExecutableCommand Command { get; }
        public ExecutableView AttachedTop { get; private set; }
        public ExecutableView AttachedBottom { get; private set; }
        public ExecutableSocket AttachedSocket { get; private set; }

        private ExecutableSocket lastOverlapSocket;
        
        [SerializeField] protected RectTransform shadow;

        protected override void Awake()
        {
            base.Awake();

            shadow.gameObject.SetActive(false);
        }

        private void AttachOnTop(ExecutableView view)
        {
            AttachedTop = view;
            
            view.Command.SetNextCommand(Command);
        }

        private void AttachOnBottom(ExecutableView view)
        {
            AttachedBottom = view;
        }

        private void DetachTop()
        {
            if (AttachedTop == null)
                return;

            var refTop = AttachedTop;
            AttachedTop.DetachBottom();
            AttachedTop.Command.SetNextCommand(null);
            AttachedTop = null;

            transform.SetParent(root);
            
            refTop.FixLayout();
        }

        private void DetachBottom()
        {
            AttachedBottom = null;
        }

        private void DetachSocket()
        {
            if (AttachedSocket == null)
                return;
            
            transform.SetParent(root);
            
            foreach (Transform child in AttachedSocket.transform)
            {
                child.gameObject.SetActive(false);
            }
            
            AttachedSocket.Detach();
            AttachedSocket = null;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            if (IsCreator)
                return;
            
            DetachTop();
            DetachSocket();
            RecursiveHideShadow();
            
            transform.SetAsLastSibling();
        }

        private void RecursiveHideShadow()
        {
            shadow.gameObject.SetActive(false);
            if(AttachedBottom)
                AttachedBottom.RecursiveHideShadow();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            
            if (IsCreator)
                return;
            
            PositionAttachShadow(GetOverlappingBlockView());
        }
        
        protected virtual void PositionAttachShadow(List<BlockView> overlapping)
        {
            if (!GetAttachedBlockAndPosition(overlapping, out var otherBlockView, out var pos, out var isSocket))
            {
                currentOverlappingBlock = null;
                FixLayout();
                return;
            }

            currentOverlappingBlock = otherBlockView;

            shadow.GetComponent<LayoutElement>().ignoreLayout = !isSocket;
            FixShadowSize();
            shadow.gameObject.SetActive(true);
            shadow.SetParent(otherBlockView.RectTransform);
            shadow.anchoredPosition = pos;

            if (isSocket)
            {
                RecursiveShowShadow(otherBlockView.RectTransform);
                lastOverlapSocket = ((ExecutableSocket)otherBlockView);
                lastOverlapSocket.Owner.FixLayout();
            }

            FixLayout();
        }

        private void RecursiveShowShadow(RectTransform parent)
        {
            if (AttachedBottom == null)
                return;
            
            AttachedBottom.shadow.GetComponent<LayoutElement>().ignoreLayout = false;
            AttachedBottom.FixShadowSize();
            AttachedBottom.shadow.SetParent(parent);
            AttachedBottom.shadow.gameObject.SetActive(true);
            AttachedBottom.shadow.SetAsLastSibling();
            
            AttachedBottom.RecursiveShowShadow(parent);
        }

        private void FixShadowSize()
        {
            shadow.pivot = shadow.anchorMin = shadow.anchorMax = new Vector2(0, 0.5f);
            shadow.sizeDelta = RectTransform.sizeDelta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            
            if (IsCreator || isDestroyed)
                return;
            
            AttachCommands(currentOverlappingBlock);
            
            if (AttachedSocket == null)
            {
                RecursiveHideShadow();
                if(lastOverlapSocket != null)
                    lastOverlapSocket.Owner.FixLayout();
            }
            
            FixLayout();
        }

        private void AttachCommands(BlockView otherView)
        {
            if (otherView == null) return;

            if (otherView is ExecutableSocket executableSocket)
            {
                AttachedSocket = executableSocket;
                AttachedSocket.Attach(this);

                return;
            }

            var otherExecutableView = otherView as ExecutableView;
            var snapTop = IsOnTop(otherView);
            var pos = GetSnapPosition(snapTop, this, otherView);
            
            if (snapTop)
            {
                var pp = otherView.RectTransform.anchoredPosition + pos;
                otherView.transform.SetParent(transform);
                otherView.RectTransform.anchoredPosition = -pos;
                RectTransform.anchoredPosition = pp;

                AttachOnBottom(otherExecutableView);
                otherExecutableView.AttachOnTop(this);
            }
            else
            {
                transform.SetParent(otherView.transform);
                RectTransform.anchoredPosition = pos;

                AttachOnTop(otherExecutableView);
                otherExecutableView.AttachOnBottom(this);
            }
        }

        private bool IsOnTop(BlockView otherBlockView)
        {
            return otherBlockView.transform.position.y < transform.position.y;
        }

        private bool GetAttachedBlockAndPosition(List<BlockView> overlappingBlocks,
            out BlockView blockView,
            out Vector2 pos,
            out bool isSocket)
        {
            pos = Vector2.zero;
            blockView = null;
            isSocket = false;
            
            blockView = overlappingBlocks.FirstOrDefault(x => x is ExecutableSocket exec && !exec.Owner.IsCreator && !exec.IsFilled);
            if (blockView != null && Command.Connection.HasFlag(Connection.Top))
            {
                isSocket = true;
                return true;
            }
            
            blockView = overlappingBlocks.FirstOrDefault(ValidExecutableView);

            if (blockView == null)
            {
                RecursiveHideShadow();
                if(currentOverlappingBlock != null)
                    currentOverlappingBlock.FixLayout();
                return false;
            }

            var otherExecutableBlockView = blockView as ExecutableView;
            var snapTop = IsOnTop(otherExecutableBlockView);
            if (snapTop)
            {
                if (!Command.Connection.HasFlag(Connection.Bottom) ||
                    !otherExecutableBlockView.Command.Connection.HasFlag(Connection.Top))
                    return false;
            }
            else
            {
                if (!Command.Connection.HasFlag(Connection.Top) ||
                    !otherExecutableBlockView.Command.Connection.HasFlag(Connection.Bottom))
                    return false;
            }

            pos = GetSnapPosition(snapTop, this, otherExecutableBlockView);
            
            if(currentOverlappingBlock != null)
                currentOverlappingBlock.FixLayout();
            
            return true;
        }

        private bool ValidExecutableView(BlockView arg)
        {
            return arg is ExecutableView exec && NotAttachedToSocket(exec) && CanAttach(exec);
        }

        private bool CanAttach(ExecutableView exec)
        {
            var isTop = IsOnTop(exec);
            if (isTop && exec.AttachedTop)
                return false;
            
            return isTop || !exec.AttachedBottom;
        }

        private bool NotAttachedToSocket(ExecutableView exec)
        {
            if (exec.AttachedSocket)
                return false;

            return !exec.AttachedTop || exec.AttachedTop.NotAttachedToSocket(exec.AttachedTop);
        }
        
        private Vector2 GetSnapPosition(bool top, BlockView c1, BlockView c2)
        {
            var posY = (((c1.RectTransform.sizeDelta.y / 2f) + (c2.RectTransform.sizeDelta.y / 2f)) - 5f) *
                       (top ? 1 : -1);
            return new Vector2(0, posY);
        }

        protected override List<BlockView> IgnoredBlocksToAttach()
        {
            var result = base.IgnoredBlocksToAttach();
            if (AttachedTop != null) result.Add(AttachedTop);
            if (AttachedBottom != null) result.Add(AttachedBottom);

            return result;
        }

        protected override IEnumerator InternalFixLayout()
        {
            yield return base.InternalFixLayout();
            yield return null;

            var current = this;

            while (true)
            {
                if (current.AttachedTop == null)
                    break;
                
                current = current.AttachedTop;
            }

            while (true)
            {
                if (current.AttachedBottom == null)
                    break;

                var newPos = GetSnapPosition(false, current, current.AttachedBottom);
                
                current.AttachedBottom.RectTransform.anchoredPosition = newPos;
                current = current.AttachedBottom;
            }
            
            if (AttachedSocket)
            {
                shadow.sizeDelta = RectTransform.sizeDelta;
                AttachedSocket.FixLayout();
            }
        }
    }
}