using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModularOptions {
	/// <summary>
	/// Scrolls a scrollable view to the currently selected item if it is out of view.
	/// It is recommended to use Explicit Navigation if you have enough elements to make the scrollbar activate.
	/// Unity's Automatic navigation can be a bit wonky with scrollable areas.
	/// </summary>
	[AddComponentMenu("Modular Options/Scroll View On Select")]
	[RequireComponent(typeof(Selectable))]
	public class ScrollViewOnSelect : MonoBehaviour, ISelectHandler {

		[Tooltip("When selected the view will scroll to show this rect. Defaults to itself, but can be changed to include things like labels.")]
		public RectTransform rectToScrollTo;
		
		[Tooltip("ScrollRect viewport that will be scrolled.")]
		public ScrollRect scrollRect;

#if UNITY_EDITOR
		/// <summary>
		/// Auto-assign editor references, if suitable components can be found.
		/// </summary>
		void Reset(){
			rectToScrollTo = GetComponent<RectTransform>();
			scrollRect = GetComponentInParent<ScrollRect>();
		}
#endif

		public void OnSelect(BaseEventData _eventData){
			var contentRect = scrollRect.content.rect;
			var viewport = scrollRect.viewport;

			var scrollbar = scrollRect.verticalScrollbar;
			if (scrollbar != null && contentRect.height > viewport.rect.height){ //No scroll if content fits in viewport
				float selectableTop = rectToScrollTo.position.y + rectToScrollTo.rect.yMax * rectToScrollTo.lossyScale.y;
				float viewportTop = viewport.position.y + viewport.rect.yMax * viewport.lossyScale.y;

				if (selectableTop > viewportTop){
					float delta = selectableTop - viewportTop;
					scrollbar.value += delta * 1/(1-scrollbar.size)/viewport.lossyScale.y / contentRect.height;
				} else {
					float selectableBottom = rectToScrollTo.position.y + rectToScrollTo.rect.yMin * rectToScrollTo.lossyScale.y;
					float viewportBottom = viewport.position.y + viewport.rect.yMin * viewport.lossyScale.y;

					if (selectableBottom < viewportBottom){
						float delta = selectableBottom - viewportBottom;
						scrollbar.value += delta * 1/(1-scrollbar.size)/viewport.lossyScale.y / contentRect.height;
					}
				}
			}

			scrollbar = scrollRect.horizontalScrollbar;
			if (scrollbar != null && contentRect.width > viewport.rect.width){
				float selectableRight = rectToScrollTo.position.x + rectToScrollTo.rect.xMax * rectToScrollTo.lossyScale.x;
				float viewportRight = viewport.position.x + viewport.rect.xMax * viewport.lossyScale.x;

				if (selectableRight > viewportRight){
					float delta = selectableRight - viewportRight;
					scrollbar.value += delta * 1/(1-scrollbar.size)/viewport.lossyScale.x / contentRect.width;
				} else {
					float selectableLeft = rectToScrollTo.position.x + rectToScrollTo.rect.xMin * rectToScrollTo.lossyScale.x;
					float viewportLeft = viewport.position.x + viewport.rect.xMin * viewport.lossyScale.x;

					if (selectableLeft < viewportLeft){
						float delta = selectableLeft - viewportLeft;
						scrollbar.value += delta * 1/(1-scrollbar.size)/viewport.lossyScale.x / contentRect.width;
					}
				}
			}
		}
	}
}
