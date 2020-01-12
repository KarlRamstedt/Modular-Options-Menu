#if (URP_PRESENT || HDRP_PRESENT)
using UnityEngine;
#if URP_PRESENT
using UnityEngine.Rendering.Universal;
#elif HDRP_PRESENT
using UnityEngine.Rendering.HighDefinition;
#endif
namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Vignette Toggle")]
	public sealed class VignetteToggle : ToggleOption {

		[Tooltip("Reference to global baseline profile.")]
		public UnityEngine.Rendering.VolumeProfile postProcessingProfile;

		Vignette vig;

		protected override void Awake(){
			if (!postProcessingProfile.TryGet<Vignette>(out vig)) //Try to get the setting override
				vig = postProcessingProfile.Add<Vignette>(true); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(bool _value){
			vig.active = _value;
		}
	}
}
#endif
