#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	/// <summary>
	/// For Scalable Ambient Obscurance. Quality settings have no effect on Multi Scale Volumetric Obscurance.
	/// </summary>
	[AddComponentMenu("Modular Options/Display/PostProcessing/Ambient Occlusion Dropdown")]
	public sealed class AmbientOcclusionDropdown : DropdownOption {

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public AmbientOcclusionQuality[] qualityOptions = {
			AmbientOcclusionQuality.Lowest,
			AmbientOcclusionQuality.High,
			AmbientOcclusionQuality.Ultra
		};
		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		AmbientOcclusion ao;

		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<AmbientOcclusion>(out ao)) //Try to get the setting override
				ao = (AmbientOcclusion)postProcessingProfile.AddSettings<AmbientOcclusion>(); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(int _value){
			if (_value == 0){
				ao.enabled.value = false;
			} else {
				_value = Mathf.Min(_value, qualityOptions.Length)-1; //Convert to 0-based index and limit max value to handle invalid saved values
				ao.enabled.value = true;
				ao.quality.value = qualityOptions[_value];
			}
		}
    }
}
#endif
