using UnityEngine;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/Field Of View Slider")]
	public sealed class FieldOfViewSlider : SliderOption {

		public Camera cam;

		protected override void ApplySetting(float _value){
			cam.fieldOfView = _value;
        }

		//public string OverrideFormatting(float _value){ //Implement ISliderDisplayFormatter interface if you want this
		//	float hFOV = 2 * Mathf.Atan(Mathf.Tan(Mathf.Deg2Rad * _value / 2) * cam.aspect) * Mathf.Rad2Deg;
		//	return _value.ToString() + " (" + hFOV.ToString("0") + ")"; //Show vertical and horizontal FOV
		//}
    }
}
