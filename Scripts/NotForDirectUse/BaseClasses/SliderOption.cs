using UnityEngine;
using UnityEngine.UI;

namespace ModularOptions {
	/// <summary>
	/// Acts as an interface between a UI Slider on the same GameObject, a saving system and your code.
	/// Inherit from this and override ApplySetting to make an option class.
	/// </summary>
	[RequireComponent(typeof(Slider))]
	public abstract class SliderOption : OptionBase<float, FloatSlider> {

		protected Slider slider;
		public override float Value {
			get { return slider.value; }
			set {
				if (slider.value == value)
					OnValueChange(value); //Ensure setting is applied when value is unchanged. OnValueChange event is only invoked when value is actually changed)
				else
					slider.value = value;
			}
		}

		/// <summary>
		/// Initializes values and subscribes listeners to events.
		/// </summary>
		protected virtual void Awake(){
			slider = GetComponent<Slider>();
			slider.onValueChanged.AddListener((float _) => OnValueChange(_)); //UI classes use Unity events, requiring delegates (delegate() { OnValueChange(); }) or lambda expressions (() => OnValueChange()). Listeners are not persistent, so no need to unsub
			Value = PlayerPrefs.GetFloat(optionName, defaultSetting.value); //Saved value if there is one, else default. After subscribing so OnValueChange applies setting
		}

		protected void OnValueChange(float _value){
			PlayerPrefs.SetFloat(optionName, _value);
			ApplySetting(_value);
			if (allowPresetCallback && preset != null)
				preset.SetCustom();
		}
		
#if UNITY_EDITOR
		/// <summary>
		/// Add a display script by default, because it's good form to show the user the value of a slider.
		/// Can easily be removed if undesired; it's optional, but recommended.
		/// </summary>
		protected override void Reset(){
			if (GetComponent<DisplaySliderValue>() == null)
				UnityEditor.Undo.AddComponent<DisplaySliderValue>(gameObject);
			base.Reset();
		}
#endif
	}
}
