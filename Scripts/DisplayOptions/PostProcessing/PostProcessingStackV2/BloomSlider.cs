#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Bloom Slider")]
	public sealed class BloomSlider : SliderOption {
		
		[Tooltip("Slider value is multiplied by this for final bloom intensity value. Default 0.01 is for use with 0 to 100% slider.")]
		public float intensityFactor = 0.01f;
		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		Bloom bloom;
		
		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<Bloom>(out bloom)) //Try to get the setting override
				bloom = (Bloom)postProcessingProfile.AddSettings<Bloom>(); //Create one if it can't be found
			base.Awake();
		}
		
		protected override void ApplySetting(float _value){
			if (_value <= slider.minValue){
				bloom.enabled.value = false;
			} else {
				bloom.enabled.value = true;
				bloom.intensity.value = _value * intensityFactor;
			}
		}
	}
}
#endif
