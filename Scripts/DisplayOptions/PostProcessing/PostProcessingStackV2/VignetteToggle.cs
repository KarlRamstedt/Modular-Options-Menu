#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Vignette Toggle")]
	public sealed class VignetteToggle : ToggleOption {

		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		Vignette vig;

		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<Vignette>(out vig)) //Try to get the setting override
				vig = (Vignette)postProcessingProfile.AddSettings<Vignette>(); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(bool _value){
			vig.enabled.value = _value;
		}
	}
}
#endif
