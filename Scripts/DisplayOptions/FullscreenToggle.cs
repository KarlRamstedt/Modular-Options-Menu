using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// A simple toggle for Fullscreen vs Windowed mode. Fullscreen mode is chosen in Edit->ProjectSettings->Player.
	/// </summary>
	/// <remarks>
	/// I recommend using WindowmodeDropdown instead of this.
	/// Unity saves screen settings by default, so this class doesn't need anything from the Option base classes.
	/// </remarks>
	[AddComponentMenu("Modular Options/Display/Fullscreen Toggle")]
	public sealed class FullscreenToggle : MonoBehaviour {

		UnityEngine.UI.Toggle toggle;

		void Awake(){
			toggle = GetComponent<UnityEngine.UI.Toggle>();
			toggle.isOn = Screen.fullScreen;
			toggle.onValueChanged.AddListener((bool _) => OnValueChange(_)); //UI classes use Unity events, requiring delegates (delegate() { OnValueChange(); }) or lambda expressions (() => OnValueChange()). Listeners are not persistent, so no need to unsub
		}

		void OnValueChange(bool _value){
			Screen.fullScreen = _value;
		}
	}
}
