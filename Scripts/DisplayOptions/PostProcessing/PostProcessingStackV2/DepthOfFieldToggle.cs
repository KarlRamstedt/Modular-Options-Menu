#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Depth Of Field Toggle")]
	public sealed class DepthOfFieldToggle : ToggleOption {

		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		DepthOfField dof;

		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<DepthOfField>(out dof)) //Try to get the setting override
				dof = (DepthOfField)postProcessingProfile.AddSettings<DepthOfField>(); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(bool _value){
			dof.enabled.value = _value;
		}
	}
}
#endif
