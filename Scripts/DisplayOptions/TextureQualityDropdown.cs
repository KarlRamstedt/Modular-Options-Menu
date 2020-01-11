using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Sets Texture quality through Texture Resolution and Filtering.
	/// Works in all Render Pipelines, but breaks rendering and requires a restart in HDRP.
	/// Filtering is grouped in with resolution due to Unity having lackluster options for filtering; The only options are Disable, Per-Texture(Enable) and 8x(ForceEnable).
	/// </summary>
	[AddComponentMenu("Modular Options/Display/Texture Quality Dropdown")]
	public sealed class TextureQualityDropdown : DropdownOption {

		public enum TextureResolution { Full = 0, Half = 1, Quarter = 2, Eighth = 3 } //0 is full texture quality, 3 is the lowest. Each setting is half the resolution of the previous one
		[Tooltip("Setting for the corresponding dropdown index.")]
		public TextureResolution[] textureResolutionOptions = {
			TextureResolution.Half,
			TextureResolution.Full,
			TextureResolution.Full
		};

		[Tooltip("Setting for the corresponding dropdown index. Enable is per-texture (chosen in import settings), ForceEnable means 8xAF.")]
		public AnisotropicFiltering[] anisotropicFilteringOptions = {
			AnisotropicFiltering.Disable,
			AnisotropicFiltering.Enable,
			AnisotropicFiltering.ForceEnable
		};

		protected override void ApplySetting(int _value){
			_value = Mathf.Min(_value, textureResolutionOptions.Length-1); //Limit max value to avoid invalid saved values
			QualitySettings.masterTextureLimit = (int)textureResolutionOptions[_value];
			QualitySettings.anisotropicFiltering = anisotropicFilteringOptions[_value];
		}
    }
}
