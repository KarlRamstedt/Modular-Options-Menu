using UnityEngine;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/Builtin Render Pipeline/Shadow Quality Dropdown")]
	public sealed class ShadowQualityDropdown : DropdownOption {

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public ShadowResolution[] shadowResolutionOptions = {
			ShadowResolution.Low,
			ShadowResolution.Medium,
			ShadowResolution.High,
			ShadowResolution.VeryHigh
		};

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public float[] shadowDistanceOptions = {
			50f,
			70f,
			90f,
			120f
		};

		[Tooltip("Setting for the corresponding dropdown index plus 1, index 0 is 'Off'.")]
		public ShadowCascades[] shadowCascadeOptions = {
			ShadowCascades.Two,
			ShadowCascades.Two,
			ShadowCascades.Four,
			ShadowCascades.Four
		};
		public enum ShadowCascades { None = 1, Two = 2, Four = 4 }
		
		protected override void ApplySetting(int _value){
			if (_value == 0){
				QualitySettings.shadows = ShadowQuality.Disable;
			} else {
				_value = Mathf.Min(_value, shadowResolutionOptions.Length)-1; //Convert to 0-based index and limit max value to handle invalid saved values
				QualitySettings.shadows = ShadowQuality.All;
				QualitySettings.shadowResolution = shadowResolutionOptions[_value];
				QualitySettings.shadowDistance = shadowDistanceOptions[_value];
				QualitySettings.shadowCascades = (int)shadowCascadeOptions[_value];
			}
		}
    }
}
