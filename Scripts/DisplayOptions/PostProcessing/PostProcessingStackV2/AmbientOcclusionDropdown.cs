#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	/// <summary>
	/// For Scalable Ambient Obscurance. Quality settings have no effect on Multi Scale Volumetric Obscurance.
	/// </summary>
	[AddComponentMenu("Modular Options/Display/PostProcessing/Ambient Occlusion Dropdown")]
	public sealed class AmbientOcclusionDropdown : PostProcessingStackDropdown<AmbientOcclusion> {

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public AmbientOcclusionQuality[] qualityOptions = {
			AmbientOcclusionQuality.Lowest,
			AmbientOcclusionQuality.High,
			AmbientOcclusionQuality.Ultra
		};

		protected override void ApplySetting(int _value){
			if (_value == 0){
				setting.enabled.value = false;
			} else {
				_value = Mathf.Min(_value, qualityOptions.Length)-1; //Convert to 0-based index and limit max value to handle invalid saved values
				setting.enabled.value = true;
				setting.quality.value = qualityOptions[_value];
			}
		}
    }
}
#endif
