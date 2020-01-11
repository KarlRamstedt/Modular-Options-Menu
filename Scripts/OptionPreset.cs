using System.Collections.Generic;
using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Offers preset functionality, allowing for changes to several referenced UI elements through one.
	/// </summary>
	[AddComponentMenu("Modular Options/Preset")]
	[DefaultExecutionOrder(3)] //Execute after normal options to overwrite their defaults
	public sealed class OptionPreset : DropdownOption {

		public SliderData[] sliderPresetData;
		public DropdownData[] dropdownPresetData;
		public ToggleData[] togglePresetData;

		/// <summary>
		/// Add itself to preset listeners to allow callbacks.
		/// Done in Start to ensure callbacks are executed after initialization regardless of ExecutionOrder.
		/// </summary>
		void Start(){
			for (int i = 0; i < sliderPresetData.Length; i++){
				sliderPresetData[i].slider.preset = this;
			}
			for (int i = 0; i < dropdownPresetData.Length; i++){
				dropdownPresetData[i].dropdown.preset = this;
			}
			for (int i = 0; i < togglePresetData.Length; i++){
				togglePresetData[i].toggle.preset = this;
			}
		}

		/// <summary>
		/// Sets preset index to custom.
		/// </summary>
		public void SetCustom(){
			Value = dropdown.options.Count-1;
		}

		protected override void ApplySetting(int _value){
			if (_value == dropdown.options.Count-1) //Do nothing if setting Custom preset.
				return;
			for (int i = 0; i < sliderPresetData.Length; i++){
				sliderPresetData[i].slider.ApplyPreset(sliderPresetData[i].presetData[_value]);
			}
			for (int i = 0; i < dropdownPresetData.Length; i++){
				dropdownPresetData[i].dropdown.ApplyPreset(dropdownPresetData[i].presetData[_value]);
			}
			for (int i = 0; i < togglePresetData.Length; i++){
				togglePresetData[i].toggle.ApplyPreset(togglePresetData[i].presetData[_value]);
			}
		}
	}

	[System.Serializable]
	public class SliderData {
		public SliderOption slider;
		public float[] presetData;
	}
	[System.Serializable]
	public class DropdownData {
		public DropdownOption dropdown;
		public int[] presetData;
	}
	[System.Serializable]
	public class ToggleData {
		public ToggleOption toggle;
		public bool[] presetData;
	}
}
