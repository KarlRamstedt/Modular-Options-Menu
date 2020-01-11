using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Changes the sensitivity of a camera control script.
	/// A simple reference FPP (First Person Perspective) camera rotation script is included.
	/// Just replace it with your own if it doesn't fit your use-case.
	/// </summary>
	[AddComponentMenu("Modular Options/Controls/Sensitivity Slider")]
	public class SensitivitySlider : SliderOption, ISliderDisplayFormatter {

		public FirstPersonCameraRotation cameraController;

		protected override void ApplySetting(float _value){
			if (cameraController != null) //Allows options screen to exist in scenes without a camera controller
				cameraController.Sensitivity = _value / 10f; //Whole numbers + 90 as max sensitivity = 0.1-9 with 1 decimal.
        }

		public string OverrideFormatting(float _value){
			return (_value/10f).ToString();
		}
    }
}