using UnityEngine;
using UnityEngine.EventSystems;

namespace ModularOptions {
	/// <summary>
	/// Put on whatever UI element you want to show a tooltip for when hovering over,
	/// then create a tooltip object (for example: text+background) and drag the reference to this.
	/// </summary>
	[AddComponentMenu("Modular Options/Tooltip")]
	[RequireComponent(typeof(UnityEngine.UI.Selectable))]
	public class UITooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {

		public GameObject tooltip;
		[Tooltip("Direction relative to this object in which to show Tooltip.")]
		public RelativePosition relativePosition = RelativePosition.right;
		public enum RelativePosition { left, right, bottom, top }
		[Tooltip("Optional offset for Tooltip position.")]
		public Vector3 offset = Vector3.zero;

		RectTransform ttTrans;
		RectTransform rTrans;

		void Awake(){
			ttTrans = tooltip.GetComponent<RectTransform>();
			rTrans = GetComponent<RectTransform>();
		}

		public void OnPointerEnter(PointerEventData _eventData){ //For mouse use
			EnterOrSelect();
		}
		public void OnPointerExit(PointerEventData _eventData){
			tooltip.SetActive(false);
		}
		public void OnSelect(BaseEventData _eventData){ //For non-mouse use (for example controllers)
			EnterOrSelect();
		}
		public void OnDeselect(BaseEventData _eventData){
			tooltip.SetActive(false);
		}

		//Constant values (pivot direction is for the tooltip, which is opposite of the RelativePosition and center on the other axis)
		readonly Vector2 middleLeftPivot = new Vector2(0f, 0.5f);
		readonly Vector2 middleRightPivot = new Vector2(1f, 0.5f);
		readonly Vector2 bottomCenterPivot = new Vector2(0.5f, 0f);
		readonly Vector2 topCenterPivot = new Vector2(0.5f, 1f);

		void EnterOrSelect(){
			var pos = rTrans.position;

			switch (relativePosition){
				case RelativePosition.left:
					ttTrans.pivot = middleRightPivot;
					ttTrans.position = new Vector3(pos.x+rTrans.rect.xMin*rTrans.lossyScale.x, pos.y, pos.z) + offset;
					break;
				case RelativePosition.right:
					ttTrans.pivot = middleLeftPivot;
					ttTrans.position = new Vector3(pos.x+rTrans.rect.xMax*rTrans.lossyScale.x, pos.y, pos.z) + offset;
					break;
				case RelativePosition.bottom:
					ttTrans.pivot = topCenterPivot;
					ttTrans.position = new Vector3(pos.x, pos.y+rTrans.rect.yMin*rTrans.lossyScale.y, pos.z) + offset;
					break;
				case RelativePosition.top:
					ttTrans.pivot = bottomCenterPivot;
					ttTrans.position = new Vector3(pos.x, pos.y+rTrans.rect.yMax*rTrans.lossyScale.y, pos.z) + offset;
					break;
				default:
					Debug.LogError("Invalid direction.", this);
					break;
			}
			tooltip.SetActive(true);
		}
	}
}
