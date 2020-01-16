#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	/// <remarks>
	/// As of 2019 I recommend using Distance Fade 0, which can be set in the Editor.
	/// </remarks>
	[AddComponentMenu("Modular Options/Display/PostProcessing/Screenspace Reflection Dropdown")]
	public sealed class ScreenSpaceReflectionDropdown : PostProcessingStackDropdown<ScreenSpaceReflections> {

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public ScreenSpaceReflectionPreset[] qualityOptions = {
			ScreenSpaceReflectionPreset.High, //Almost no performance difference between this and lowest setting
			ScreenSpaceReflectionPreset.Ultra,
			ScreenSpaceReflectionPreset.Overkill
		};

		protected override void ApplySetting(int _value){
			if (_value == 0){
				setting.enabled.value = false;
			} else {
				_value = Mathf.Min(_value, qualityOptions.Length)-1; //Convert to 0-based index and limit max value to handle invalid saved values
				setting.enabled.value = true;
				setting.preset.value = qualityOptions[_value];
			}
		}
    }
}
#endif
