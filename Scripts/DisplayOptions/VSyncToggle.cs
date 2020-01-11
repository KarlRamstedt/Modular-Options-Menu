using UnityEngine;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/V-Sync Toggle")]
	public sealed class VSyncToggle : ToggleOption {

		protected override void ApplySetting(bool _value){
			QualitySettings.vSyncCount = _value ? 1 : 0; //Can be set higher than 1, but doesn't make much sense for an option. Higher than 1 is mainly useful for mobile where it can reduce power draw by limiting framerate below refreshrate.
		}
	}
}
