using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Base-class for option controls.
	/// UI element base classes inherit from this with their data type as type parameter.
	/// </summary>
	public abstract class OptionBase<T, U> : MonoBehaviour
		where T : struct
		where U : UIDataType<T> {

		[Tooltip("Key for saving & loading, with other possible re-use.")]
		public string optionName;

		public U defaultSetting; //A simple value-type, wrapped in a Serializable class to use PropertyDrawers

		public abstract T Value { get; set; }

		[HideInInspector] public OptionPreset preset; //Used for preset callbacks
		protected bool allowPresetCallback = true;
		/// <summary>
		/// Method wrapper for Value setter to prevent callbacks when changed through preset.
		/// </summary>
		public void ApplyPreset(T _value){
			allowPresetCallback = false;
			Value = _value;
			allowPresetCallback = true;
		}

		/// <summary>
		/// Override with the code relevant to applying the setting you want changed.
		/// Abstract rather than virtual because it has no base functionality and is core to the class.
		/// </summary>
		protected abstract void ApplySetting(T _value);

#if UNITY_EDITOR
		/// <summary>
		/// Automatic default optionName assignment.
		/// NOTE: Requires assignment in editor to work with multiple instances.
		/// VolumeSliders for example need separate names per slider or they'll over-write each other.
		/// </summary>
		protected virtual void Reset(){
			optionName = GetType().ToString();
		}
#endif
	}

	/// <summary>
	/// Wrapper class for UI data values.
	/// Used so the data can be drawn in a user-friendly way with PropertyDrawers.
	/// </summary>
	[System.Serializable]
	public class UIDataType<T> where T : struct {
		[Tooltip("Setting used if no saved setting exists. Can also be used externally to restore defaults.")]
		[SerializeField] public T value;
	}
	//Wrappers because Unity can't serialize generic classes
	/// <summary>
	/// Editor slider for a float value, with min/max/wholeNumbers parameters
	/// fetched from the UI Slider on the same GameObject.
	/// </summary>
	[System.Serializable] public class FloatSlider : UIDataType<float> {}
	/// <summary>
	/// Editor dropdown for an int value, with dropdown options
	/// fetched from the UI Dropdown on the same GameObject.
	/// </summary>
	[System.Serializable] public class IntDropdown : UIDataType<int> {}
	/// <summary>
	/// Wrapper class for a bool. Does nothing, but is required by the framework.
	/// </summary>
	[System.Serializable] public class BoolToggle : UIDataType<bool> {}
}
