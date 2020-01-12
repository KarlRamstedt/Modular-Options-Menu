#if HDRP_PRESENT
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Screenspace Reflection Dropdown")]
	public sealed class ScreenSpaceReflectionDropdown : DropdownOption {

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public ScalableSettingLevelParameter.Level[] qualityOptions = {
			ScalableSettingLevelParameter.Level.Low,
			ScalableSettingLevelParameter.Level.Medium,
			ScalableSettingLevelParameter.Level.High
		};
		[Tooltip("Reference to global baseline profile.")]
		public UnityEngine.Rendering.VolumeProfile postProcessingProfile;

		ScreenSpaceReflection ssr;

		protected override void Awake(){
			if (!postProcessingProfile.TryGet<ScreenSpaceReflection>(out ssr)) //Try to get the setting override
				ssr = postProcessingProfile.Add<ScreenSpaceReflection>(true); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(int _value){
			if (_value == 0){
				ssr.active = false;
			} else {
				_value = Mathf.Min(_value, qualityOptions.Length)-1; //Convert to 0-based index and limit max value to handle invalid saved values
				ssr.active = true;
				ssr.quality.value = (int)qualityOptions[_value];
			}
		}
    }
}
#endif
