#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	/// <summary>
	/// For Multi Scale Volumetric Obscurance. Toggle because the Quality Settings don't affect it.
	/// </summary>
	[AddComponentMenu("Modular Options/Display/PostProcessing/Ambient Occlusion Dropdown")]
	public sealed class AmbientOcclusionToggle : PostProcessingStackToggle<AmbientOcclusion> {
    }
}
#endif
