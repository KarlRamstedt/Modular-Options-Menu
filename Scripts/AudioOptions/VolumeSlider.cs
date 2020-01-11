using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Volume slider for Unity mixer Volume. Requires an optionName that matches the exposed Volume parameter.
	/// </summary>
	[AddComponentMenu("Modular Options/Audio/Volume Slider")]
	public sealed class VolumeSlider : SliderOption {

		[Tooltip("Mixer with exposed Volume parameter matching OptionName.")]
		public UnityEngine.Audio.AudioMixer mixer;

		protected override void ApplySetting(float _value){
			mixer.SetFloat(optionName, ConvertToDecibel(_value/slider.maxValue)); //Dividing by max allows arbitrary positive slider maxValue
		}

		/// <summary>
		/// Converts a percentage fraction to decibels,
		/// with a lower clamp of 0.0001 for a minimum of -80dB, same as Unity defaults.
		/// </summary>
		public float ConvertToDecibel(float _value){
			return Mathf.Log10(Mathf.Max(_value, 0.0001f))*20f;
		}
	}
}

	// /// <summary>
	// /// Volume slider for projects using FMOD. Requires an optionName that matches a VCA name.
	// /// </summary>
	// [AddComponentMenu("Modular Options/Volume Slider")]
	// public sealed class VolumeSlider : SliderOption {

	// 	FMOD.Studio.VCA soundVCA;

	// 	protected override void Awake(){
	// 		soundVCA = FMODUnity.RuntimeManager.GetVCA("vca:/" + optionName);
	// 		base.Awake();
	// 	}

	// 	protected override void ApplySetting(float _value){
	// 		soundVCA.setVolume(_value/slider.maxValue); //FMOD volume goes from 0 to 1, dividing by max allows arbitrary positive slider maxValue
	// 	}
    // }
