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

		PostProcessLayer ppl;

		protected override void Awake(){ //Options could be autofetched (downside: cannot choose option names): string[] AAOptions = System.Enum.GetNames(typeof(PostProcessLayer.Antialiasing));
			ppl = Camera.main.GetComponent<PostProcessLayer>();
			base.Awake();
		}

		protected override void ApplySetting(int _value){
			_value = Mathf.Min(_value, options.Length-1); //Limit max value to handle invalid saved values
			ppl.antialiasingMode = options[_value];
		}
	}
}
#endif
