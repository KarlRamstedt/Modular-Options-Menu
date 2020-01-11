#if (URP_PRESENT || HDRP_PRESENT)
using UnityEngine;
#if URP_PRESENT
using UnityEngine.Rendering.Universal;
#elif HDRP_PRESENT
using UnityEngine.Rendering.HighDefinition;
#endif
namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Depth Of Field Toggle")]
	public sealed class DepthOfFieldToggle : ToggleOption {

		[Tooltip("Reference to global baseline profile.")]
		public UnityEngine.Rendering.VolumeProfile postProcessingProfile;

		DepthOfField dof;

		protected override void Awake(){
			if (!postProcessingProfile.TryGet<DepthOfField>(out dof)) //Try to get the setting override
				dof = (DepthOfField)postProcessingProfile.Add<DepthOfField>(true); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(bool _value){
			dof.active = _value;
		}
	}
}
#endif
