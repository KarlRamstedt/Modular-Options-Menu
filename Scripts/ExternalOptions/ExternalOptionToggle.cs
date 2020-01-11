using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// A class that acts as a generic link between the option system and an external system.
	/// Useful if you for example already have code (a public function) for setting an option in your class.
	/// </summary>
	[AddComponentMenu("Modular Options/External/Toggle")]
	public class ExternalOptionToggle : ToggleOption {

		public UnityEngine.UI.Toggle.ToggleEvent onValueChange; //A simple wrapper class because Unity can't serialize Generics

		protected override void ApplySetting(bool _value){
			onValueChange.Invoke(_value);
		}
	}
}
