using UnityEngine;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/Anisotropic Filtering Dropdown")]
	public sealed class AnisotropicFilteringDropdown : DropdownOption {

		[Tooltip("Setting for the corresponding dropdown index. Enable is per-texture (chosen in import settings), ForceEnable means 8xAF.")]
		public AnisotropicFiltering[] anisotropicFilteringOptions = {
			AnisotropicFiltering.Disable,
			AnisotropicFiltering.Enable,
			AnisotropicFiltering.ForceEnable
		};

		protected override void ApplySetting(int _value){
			_value = Mathf.Min(_value, anisotropicFilteringOptions.Length-1); //Limit max value to avoid invalid saved values
			QualitySettings.anisotropicFiltering = anisotropicFilteringOptions[_value];
		}
    }
}
