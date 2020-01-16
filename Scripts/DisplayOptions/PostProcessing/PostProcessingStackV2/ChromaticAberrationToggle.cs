#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Chromatic Aberration Toggle")]
	public sealed class ChromaticAberrationToggle : PostProcessingStackToggle<ChromaticAberration> {
	}
}
#endif
