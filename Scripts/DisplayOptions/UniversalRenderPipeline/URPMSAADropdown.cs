#if URP_PRESENT
using UnityEngine;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/Universal Render Pipeline/MSAA Dropdown")]
	public sealed class URPMSAADropdown : DropdownOption {

		public UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset pipelineAsset;

		[Tooltip("Setting for the corresponding dropdown index.")]
		public MSAASamples[] options = {
			MSAASamples.None,
			MSAASamples.MSAA2x,
			MSAASamples.MSAA4x,
			MSAASamples.MSAA8x
		};
		public enum MSAASamples { None = 1, MSAA2x = 2, MSAA4x = 4, MSAA8x = 8 }

#if UNITY_EDITOR
		/// <summary>
		/// Auto-assign reference. Replace it in the editor if it was incorrect.
		/// </summary>
		protected override void Reset(){
			pipelineAsset = Resources.FindObjectsOfTypeAll<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>()[0];
			base.Reset();
		}
#endif

		protected override void ApplySetting(int _value){
			_value = Mathf.Min(_value, options.Length-1); //Limit max value to avoid invalid saved values
			pipelineAsset.msaaSampleCount = (int)options[_value];
		}
	}
}
#endif
