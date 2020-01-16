#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Bloom Slider")]
	public sealed class BloomSlider : PostProcessingStackSlider<Bloom> {
		
		[Tooltip("Slider value is multiplied by this for final intensity value. Default 0.01 is for use with 0 to 100% slider.")]
		public float intensityFactor = 0.01f;
		
		protected override void ApplySetting(float _value){
			if (_value <= slider.minValue){
				setting.enabled.value = false;
			} else {
				setting.enabled.value = true;
				setting.intensity.value = _value * intensityFactor;
			}
		}
	}
}
#endif
