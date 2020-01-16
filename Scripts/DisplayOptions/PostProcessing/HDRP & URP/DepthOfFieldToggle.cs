#if (URP_PRESENT || HDRP_PRESENT)
using UnityEngine;
#if URP_PRESENT
using UnityEngine.Rendering.Universal;
#elif HDRP_PRESENT
using UnityEngine.Rendering.HighDefinition;
#endif
namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Depth Of Field Toggle")]
	public sealed class DepthOfFieldToggle : PostProcessingToggle<DepthOfField> {}
}
#endif
