#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Chromatic Aberration Toggle")]
	public sealed class ChromaticAberrationToggle : ToggleOption {

		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		ChromaticAberration ca;

		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<ChromaticAberration>(out ca)) //Try to get the setting override
				ca = postProcessingProfile.AddSettings<ChromaticAberration>(); //Create one if it can't be found
			base.Awake();
		}

		protected override void ApplySetting(bool _value){
			ca.enabled.value = _value;
		}
	}
}
#endif
