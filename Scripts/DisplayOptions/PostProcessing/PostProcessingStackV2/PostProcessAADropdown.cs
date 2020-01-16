#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Anti-Aliasing Dropdown")]
	public sealed class PostProcessAADropdown : DropdownOption {

		[Tooltip("Setting for the corresponding dropdown index.")]
		public PostProcessLayer.Antialiasing[] options = {
			PostProcessLayer.Antialiasing.None,
			PostProcessLayer.Antialiasing.FastApproximateAntialiasing, //FXAA
			PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing, //SMAA
			PostProcessLayer.Antialiasing.TemporalAntialiasing //TAA
		};

		public PostProcessLayer ppl;

		protected override void ApplySetting(int _value){
			_value = Mathf.Min(_value, options.Length-1); //Limit max value to handle invalid saved values
			ppl.antialiasingMode = options[_value];
		}

#if UNITY_EDITOR
		protected override void Reset(){
			ppl = Camera.main.GetComponent<PostProcessLayer>();
			base.Reset();
		}
#endif
	}
}
#endif
