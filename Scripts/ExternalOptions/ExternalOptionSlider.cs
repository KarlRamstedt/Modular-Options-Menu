using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// A class that acts as a generic link between the option system and an external system.
	/// Useful if you for example already have code (a public function) for setting an option in your class.
	/// </summary>
	[AddComponentMenu("Modular Options/External/Slider")]
	public class ExternalOptionSlider : SliderOption {

		public UnityEngine.UI.Slider.SliderEvent onValueChange; //A simple wrapper class because Unity can't serialize Generics

		protected override void ApplySetting(float _value){
			onValueChange.Invoke(_value);
		}
	}
}
