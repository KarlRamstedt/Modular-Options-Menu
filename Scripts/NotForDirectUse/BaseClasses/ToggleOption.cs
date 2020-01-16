using UnityEngine;
using UnityEngine.UI;

namespace ModularOptions {
	/// <summary>
	/// Acts as an interface between a UI Toggle on the same GameObject, a saving system and your code.
	/// Inherit from this and override ApplySetting to make an option class.
	/// </summary>
	[RequireComponent(typeof(Toggle))]
	public abstract class ToggleOption : OptionBase<bool, BoolToggle> {

		protected Toggle toggle;
		public override bool Value {
			get { return toggle.isOn; }
			set {
				if (toggle.isOn == value)
					OnValueChange(value); //Ensure setting is applied when value is unchanged. OnValueChange event is only invoked when value is actually changed
				else
					toggle.isOn = value;
			}
		}

		/// <summary>
		/// Initializes values and subscribes listeners to events.
		/// </summary>
		protected virtual void Awake(){
			toggle = GetComponent<Toggle>();
			toggle.onValueChanged.AddListener((bool _) => OnValueChange(_)); //UI classes use Unity events, requiring delegates (delegate() { OnValueChange(); }) or lambda expressions (() => OnValueChange()). Listeners are not persistent, so no need to unsub
			Value = OptionSaveSystem.LoadBool(optionName, defaultSetting.value); //Saved value if there is one, else default. After subscribing so OnValueChange applies setting
		}

		protected void OnValueChange(bool _value){
			OptionSaveSystem.SaveBool(optionName, _value);
			ApplySetting(_value);
			if (allowPresetCallback && preset != null)
				preset.SetCustom();
		}
	}
}
