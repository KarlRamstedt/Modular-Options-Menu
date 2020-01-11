using UnityEngine;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/Windowmode Dropdown")]
	public sealed class WindowmodeDropdown : DropdownOption {

		[Tooltip("Setting for the corresponding dropdown index.")]
		public FullScreenMode[] options = {
			FullScreenMode.ExclusiveFullScreen,
			FullScreenMode.FullScreenWindow, //FullScreenWindow = BorderlessFullscreen. MaximizedWindow is a MacOS-only thing.
			FullScreenMode.Windowed
		};

		protected override void ApplySetting(int _value){
			_value = Mathf.Min(_value, options.Length-1); //Limit max value to avoid invalid saved values
			Screen.fullScreenMode = options[_value];
		}
    }
}
