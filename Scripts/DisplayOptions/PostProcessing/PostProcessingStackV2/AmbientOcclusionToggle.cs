#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	/// <summary>
	/// For Multi Scale Volumetric Obscurance. Toggle because the Quality Settings don't affect it.
	/// </summary>
	[AddComponentMenu("Modular Options/Display/PostProcessing/Ambient Occlusion Dropdown")]
	public sealed class AmbientOcclusionToggle : ToggleOption {

		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		AmbientOcclusion ao;

		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<AmbientOcclusion>(out ao)) //Try to get the setting override
				ao = postProcessingProfile.AddSettings<AmbientOcclusion>(); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(bool _value){
			ao.enabled.value = _value;
		}
    }
}
#endif
