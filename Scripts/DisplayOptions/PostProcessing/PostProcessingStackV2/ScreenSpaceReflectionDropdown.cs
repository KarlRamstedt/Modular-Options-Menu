#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	/// <remarks>
	/// As of 2019 I recommend using Distance Fade 0, which can be set in the Editor.
	/// </remarks>
	[AddComponentMenu("Modular Options/Display/PostProcessing/Screenspace Reflection Dropdown")]
	public sealed class ScreenSpaceReflectionDropdown : DropdownOption {

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public ScreenSpaceReflectionPreset[] qualityOptions = {
			ScreenSpaceReflectionPreset.High, //Almost no performance difference between this and lowest setting
			ScreenSpaceReflectionPreset.Ultra,
			ScreenSpaceReflectionPreset.Overkill
		};
		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		ScreenSpaceReflections ssr;

		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<ScreenSpaceReflections>(out ssr)) //Try to get the setting override
				ssr = (ScreenSpaceReflections)postProcessingProfile.AddSettings<ScreenSpaceReflections>(); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(int _value){
			if (_value == 0){
				ssr.enabled.value = false;
			} else {
				_value = Mathf.Min(_value, qualityOptions.Length)-1; //Convert to 0-based index and limit max value to handle invalid saved values
				ssr.enabled.value = true;
				ssr.preset.value = qualityOptions[_value];
			}
		}
    }
}
#endif
