using UnityEngine;
using UnityEngine.UI;

namespace ModularOptions {
	/// <summary>
	/// Acts as an interface between a UI Dropdown on the same GameObject, a saving system and your code.
	/// Inherit from this and override ApplySetting to make an option class.
	/// </summary>
#if TMP_PRESENT
	[RequireComponent(typeof(TMPro.TMP_Dropdown))]
#else
	[RequireComponent(typeof(Dropdown))]
#endif
	public abstract class DropdownOption : OptionBase<int, IntDropdown> {
#if TMP_PRESENT
		protected TMPro.TMP_Dropdown dropdown;
#else
		protected Dropdown dropdown;
#endif
		public override int Value {
			get { return dropdown.value; }
			set {
				if (dropdown.value == value)
					OnValueChange(value); //Ensure setting is applied when value is unchanged. OnValueChange event is only invoked when value is actually changed
				else {
					dropdown.value = value;
					dropdown.RefreshShownValue();
				}
			}
		}

		/// <summary>
		/// Initializes values and subscribes listeners to events.
		/// </summary>
		protected virtual void Awake(){
#if TMP_PRESENT
			dropdown = GetComponent<TMPro.TMP_Dropdown>();
#else
			dropdown = GetComponent<Dropdown>();
#endif
			dropdown.onValueChanged.AddListener((int _) => OnValueChange(_)); //UI classes use Unity events, requiring delegates (delegate() { OnValueChange(); }) or lambda expressions (() => OnValueChange()). Listeners are not persistent, so no need to unsub
			Value = OptionSaveSystem.LoadInt(optionName, defaultSetting.value); //Saved value if there is one, else default. After subscribing so OnValueChange applies setting
		}

		protected void OnValueChange(int _value){
			OptionSaveSystem.SaveInt(optionName, _value);
			ApplySetting(_value);
			if (allowPresetCallback && preset != null)
				preset.SetCustom();
		}
	}
}
