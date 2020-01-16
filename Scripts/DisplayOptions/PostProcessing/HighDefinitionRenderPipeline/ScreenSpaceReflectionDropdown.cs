#if HDRP_PRESENT
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Screenspace Reflection Dropdown")]
	public sealed class ScreenSpaceReflectionDropdown : PostProcessingDropdown<ScreenSpaceReflection> {

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public ScalableSettingLevelParameter.Level[] qualityOptions = {
			ScalableSettingLevelParameter.Level.Low,
			ScalableSettingLevelParameter.Level.Medium,
			ScalableSettingLevelParameter.Level.High
		};

		protected override void ApplySetting(int _value){
			if (_value == 0){
				setting.active = false;
			} else {
				_value = Mathf.Min(_value, qualityOptions.Length)-1; //Convert to 0-based index and limit max value to handle invalid saved values
				setting.active = true;
				setting.quality.value = (int)qualityOptions[_value];
			}
		}
    }
}
#endif
