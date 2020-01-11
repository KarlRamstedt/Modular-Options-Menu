#if (URP_PRESENT || HDRP_PRESENT)
using UnityEngine;
#if URP_PRESENT
using UnityEngine.Rendering.Universal;
#elif HDRP_PRESENT
using UnityEngine.Rendering.HighDefinition;
#endif
namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Film Grain Slider")]
	public sealed class FilmGrainSlider : SliderOption {
		
		[Tooltip("Slider value is multiplied by this for final intensity value. Default 0.01 is for use with 0 to 100% slider.")]
		public float intensityFactor = 0.01f;
		[Tooltip("Reference to global baseline profile.")]
		public UnityEngine.Rendering.VolumeProfile postProcessingProfile;

		FilmGrain fg;
		
		protected override void Awake(){
			if (!postProcessingProfile.TryGet<FilmGrain>(out fg)) //Try to get the setting override
				fg = (FilmGrain)postProcessingProfile.Add<FilmGrain>(true); //Create one if it can't be found
			base.Awake();
		}
		
		protected override void ApplySetting(float _value){
			if (_value <= 0){
				fg.active = false;
			} else {
				fg.active = true;
				fg.intensity.value = _value * intensityFactor;
			}
		}
	}
}
#endif
