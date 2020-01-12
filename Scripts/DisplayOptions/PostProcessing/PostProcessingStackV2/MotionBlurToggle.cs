#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Motion Blur Toggle")]
	public sealed class MotionBlurToggle : ToggleOption {

		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		MotionBlur mb;

		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<MotionBlur>(out mb)) //Try to get the setting override
				mb = postProcessingProfile.AddSettings<MotionBlur>(); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(bool _value){
			mb.enabled.value = _value;
		}
	}
}
#endif
