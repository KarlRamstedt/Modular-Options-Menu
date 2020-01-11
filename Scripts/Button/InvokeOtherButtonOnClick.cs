using UnityEngine;
using UnityEngine.UI;

namespace ModularOptions {
	/// <summary>
	/// Invokes (clicks) referenced button when the button on this GameObject is clicked.
	/// This button doesn't need to know what the other one does, it just delegates responsibility.
	/// Useful for reducing listener duplication in the menu heirarchy.
	/// </summary>
	[AddComponentMenu("Modular Options/Button/Invoke Other Button")]
	[RequireComponent(typeof(Button))]
	public class InvokeOtherButtonOnClick : MonoBehaviour {

		public Button buttonToInvoke;

		void Awake(){
			if (buttonToInvoke.gameObject == gameObject){
				Debug.LogWarning("Invocation loop prevented. Don't reference a button on the same GameObject.", this);
				return;
			}
			GetComponent<Button>().onClick.AddListener(() => buttonToInvoke.onClick.Invoke());
		}
	}
}
