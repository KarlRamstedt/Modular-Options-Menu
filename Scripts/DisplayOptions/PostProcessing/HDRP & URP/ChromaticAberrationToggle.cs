#if (URP_PRESENT || HDRP_PRESENT)
using UnityEngine;
#if URP_PRESENT
using UnityEngine.Rendering.Universal;
#elif HDRP_PRESENT
using UnityEngine.Rendering.HighDefinition;
#endif
namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Chromatic Aberration Toggle")]
	public sealed class ChromaticAberrationToggle : ToggleOption {

		[Tooltip("Reference to global baseline profile.")]
		public UnityEngine.Rendering.VolumeProfile postProcessingProfile;

		ChromaticAberration ca;

		protected override void Awake(){
			if (!postProcessingProfile.TryGet<ChromaticAberration>(out ca)) //Try to get the setting override
				ca = postProcessingProfile.Add<ChromaticAberration>(true); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(bool _value){
			ca.active = _value;
		}
	}
}
#endif
