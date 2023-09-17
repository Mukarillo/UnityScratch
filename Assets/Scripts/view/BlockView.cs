using System.Collections;
using System.Collections.Generic;
using System.Linq;
using domain.commands.executables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace view
{
    public abstract class BlockView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool IsCreator;

        public RectTransform RectTransform { get; private set; }
        protected virtual bool IsDraggable => true;
        
        protected RectTransform root;
        protected ExecutableContext context;
        
        private Vector2 initialOffset;
        protected BlockView currentOverlappingBlock;

        private BlockView createdView;

        private Trash trash;
        protected bool isDestroyed;

        public virtual void Setup(RectTransform root, ExecutableContext context)
        {
            this.root = root;
            this.context = context;

            trash = FindObjectOfType<Trash>();
        }

        protected virtual void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (IsCreator)
            {
                var newObject = Instantiate(gameObject);
                newObject.transform.SetParent(root, true);
                newObject.transform.position = transform.position;
                newObject.transform.localScale = Vector3.one;
                createdView = newObject.GetComponent<BlockView>();
                createdView.IsCreator = false;
                createdView.Setup(root, context);
                createdView.OnBeginDrag(eventData);

                createdView.RectTransform.anchorMin = createdView.RectTransform.anchorMax =
                    createdView.RectTransform.pivot = new Vector2(0, 0.5f);  
                
                createdView.OnObjectCloned(this);

                return;
            }
            
            FixLayout();
            if (!IsDraggable)
                return;
            
            initialOffset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
        }

        protected virtual void OnObjectCloned(BlockView fromBlockView) { }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if(createdView != null)
                createdView.OnDrag(eventData);
            
            if (!IsDraggable || IsCreator)
                return;
            
            transform.position = eventData.position + initialOffset;
        }
        
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if (createdView != null)
            {
                createdView.OnEndDrag(eventData);
                createdView = null;
            }
            
            FixLayout();
            
            if (IsOverlapping(RectTransform, trash.RectTransform))
            {
                isDestroyed = true;
                transform.SetParent(null);
                transform.position = Vector3.one * 1000;
            }
        }

        protected List<BlockView> GetOverlappingBlockView()
        {
            var overlappingBlocks = new List<BlockView>();
            foreach (var blockView in FindObjectsOfType<BlockView>())
            {
                if (IgnoredBlocksToAttach().Contains(blockView) || blockView.IsCreator) continue;

                if (IsOverlapping(RectTransform, blockView.RectTransform))
                    overlappingBlocks.Add(blockView);
            }

            return overlappingBlocks;
        }

        private bool IsOverlapping(RectTransform rt1, RectTransform rt2)
        {
            var rect1 = GetWorldRect(rt1);
            var rect2 = GetWorldRect(rt2);

            return rect1.Overlaps(rect2);
        }

        private Rect GetWorldRect(RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            var min = corners[0];
            var max = corners[2];

            return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
        }

        protected virtual List<BlockView> IgnoredBlocksToAttach()
        {
            var result = new List<BlockView> { this };
            
            result.AddRange(GetComponentsInChildren<BlockView>());
            
            return result;
        }

        protected BlockView GetClosest(IEnumerable<BlockView> blocks)
        {
            if (blocks.Count() <= 1)
                return blocks.FirstOrDefault();
            
            var minDist = float.MaxValue;
            BlockView closest = null;
            
            var myRect = GetWorldRect(RectTransform);
            foreach (var block in blocks)
            {
                var d = GetDistance(block);
                if (!(d < minDist)) 
                    continue;
                closest = block;
                minDist = d;
            }

            return closest;
            
            float GetDistance(BlockView block)
            {
                var rect = GetWorldRect(block.RectTransform);
                return Vector2.Distance(rect.center, myRect.center);
            }
        }

        public void FixLayout()
        {
            StartCoroutine(InternalFixLayout());
        }

        protected virtual IEnumerator InternalFixLayout()
        {
            yield return null;
            
            var rectTransforms = GetComponentsInParent<RectTransform>().ToList();
            rectTransforms.Add(RectTransform);
            foreach (var rt in rectTransforms)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rt);    
            }
        }
    }
}