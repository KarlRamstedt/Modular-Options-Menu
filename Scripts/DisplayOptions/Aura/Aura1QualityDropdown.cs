#if AURA_PRESENT
using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// A volumetric lighting asset available on GitHub: https://github.com/raphael-ernaelsten/Aura
	/// Requires Aura to be enabled in the scene, because Aura doesn't support runtime initialization.
	/// Aura can then be disabled from this script, just not enabled again (requires scene reload).
	/// I strongly recommended showing a tooltip mentioning that. Something like:
	/// "Enabling this option when it is off will require a reload to enable the effect."
	/// Runtime initialization is a feature in Aura 2, a paid asset: https://assetstore.unity.com/packages/tools/particles-effects/aura-2-volumetric-lighting-fog-137148
	/// </summary>
	[AddComponentMenu("Modular Options/Display/Volumetric Lighting/Aura Quality Dropdown")]
	public sealed class Aura1QualityDropdown : DropdownOption {

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public Vector3Int[] resolutionOptions = {
			new Vector3Int(160, 88, 64), //Default, low-ish
			new Vector3Int(320, 176, 128)
		};

		public AuraAPI.Aura aura;

#if UNITY_EDITOR
		/// <summary>
		/// Auto-assign reference.
		/// </summary>
		protected override void Reset(){
			aura = Camera.main.GetComponent<AuraAPI.Aura>();
			base.Reset();
		}
#endif

		protected override void ApplySetting(int _value){
			if (_value == 0){
				aura.enabled = false;
			} else {
				_value = Mathf.Min(_value, resolutionOptions.Length)-1; //Convert to 0-based index and limit max value to handle invalid saved values
				aura.enabled = true;
				aura.frustum.SetResolution(resolutionOptions[_value]);
			}
		}
    }
}
#endif
