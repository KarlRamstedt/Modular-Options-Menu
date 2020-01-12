#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Film Grain Slider")]
	public sealed class FilmGrainSlider : SliderOption {
		
		[Tooltip("Slider value is multiplied by this for final intensity value. Default 0.01 is for use with 0 to 100% slider.")]
		public float intensityFactor = 0.01f;
		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		Grain fg;
		
		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<Grain>(out fg)) //Try to get the setting override
				fg = postProcessingProfile.AddSettings<Grain>(); //Create one if it can't be found
			base.Awake();
		}
		
		protected override void ApplySetting(float _value){
			if (_value <= slider.minValue){
				fg.enabled.value = false;
			} else {
				fg.enabled.value = true;
				fg.intensity.value = _value * intensityFactor;
			}
		}
	}
}
#endif
