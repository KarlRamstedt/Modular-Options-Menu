#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	[AddComponentMenu("Modular Options/Display/PostProcessing/Motion Blur Toggle")]
	public sealed class MotionBlurToggle : PostProcessingStackToggle<MotionBlur> {
	}
}
#endif
