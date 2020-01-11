#if (URP_PRESENT || HDRP_PRESENT)
using UnityEngine;
#if URP_PRESENT
using UnityEngine.Rendering.Universal;
#elif HDRP_PRESENT
using UnityEngine.Rendering.HighDefinition;
#endif
namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Motion Blur Toggle")]
	public sealed class MotionBlurToggle : ToggleOption {

		[Tooltip("Reference to global baseline profile.")]
		public UnityEngine.Rendering.VolumeProfile postProcessingProfile;

		MotionBlur mb;

		protected override void Awake(){
			if (!postProcessingProfile.TryGet<MotionBlur>(out mb)) //Try to get the setting override
				mb = (MotionBlur)postProcessingProfile.Add<MotionBlur>(true); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(bool _value){
			mb.active = _value;
		}
	}
}
#endif
