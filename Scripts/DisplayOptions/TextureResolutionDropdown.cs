using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Works in all Render Pipelines, but breaks 3D-rendering in HDRP (a restart fixes it and properly applies the setting) in 2019.3.
	/// </summary>
	[AddComponentMenu("Modular Options/Display/Texture Resolution Dropdown")]
	public sealed class TextureResolutionDropdown : DropdownOption {

		public enum TextureResolution { Full = 0, Half = 1, Quarter = 2, Eighth = 3 } //0 is the highest texture resolution, 3 is the lowest. Each setting is half the resolution of the previous one
		[Tooltip("Setting for the corresponding dropdown index.")]
		public TextureResolution[] textureResolutionOptions = {
			TextureResolution.Quarter,
			TextureResolution.Half,
			TextureResolution.Full
		};

		protected override void ApplySetting(int _value){
			_value = Mathf.Min(_value, textureResolutionOptions.Length-1); //Limit max value to avoid invalid saved values
			QualitySettings.masterTextureLimit = (int)textureResolutionOptions[_value];
		}
    }
}
