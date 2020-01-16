#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Vignette Toggle")]
	public sealed class VignetteToggle : PostProcessingStackToggle<Vignette> {
	}
}
#endif
