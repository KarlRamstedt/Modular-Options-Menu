using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Simple utility script for changing cursor attributes after loading scenes.
	/// </summary>
	[AddComponentMenu("Modular Options/Misc/Set Cursor Attributes On Awake")]
	public class SetCursorAttributesOnAwake : MonoBehaviour {

		public bool visible = true;
		public CursorLockMode lockState = CursorLockMode.Confined;

		void Awake(){
			Cursor.visible = visible;
			Cursor.lockState = lockState;
		}

		/// <summary>
		/// Public function for potential reference in UI events.
		/// </summary>
		public void SetCursorVisibility(bool _visible){
			Cursor.visible = _visible;
		}
	}
}
