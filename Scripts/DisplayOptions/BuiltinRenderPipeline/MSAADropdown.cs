using UnityEngine;

namespace ModularOptions {
	/// <remarks>
	/// MSAA only works with Forward Rendering. Use PostProcess Anti-Aliasing for Deferred.
	/// </remarks>
	[AddComponentMenu("Modular Options/Display/Builtin Render Pipeline/MultiSample Anti-Aliasing Dropdown")]
	public sealed class MSAADropdown : DropdownOption {

		[Tooltip("Setting for the corresponding dropdown index.")]
		public MSAASamples[] options = {
			MSAASamples.None,
			MSAASamples.MSAA2x,
			MSAASamples.MSAA4x,
			MSAASamples.MSAA8x
		};
		public enum MSAASamples { None = 1, MSAA2x = 2, MSAA4x = 4, MSAA8x = 8 }

		protected override void ApplySetting(int _value){
			_value = Mathf.Min(_value, options.Length-1); //Limit max value to avoid invalid saved values
			QualitySettings.antiAliasing = (int)options[_value];
		}
    }
}
