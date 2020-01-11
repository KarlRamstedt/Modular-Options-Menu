#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	/// <summary>
	/// Blurs the background when parent object is Enabled.
	/// </summary>
	[AddComponentMenu("Modular Options/Misc/Menu Depth Of Field Blur")]
	public sealed class MenuDepthOfFieldBlur : MonoBehaviour {

		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		[Range(0.01f, 9f)] public float focusDistance = 0.7f;

		DepthOfField dof;
		float normalFocusDistance;
		bool dofActive;

		void Awake(){
			if (!postProcessingProfile.TryGetSettings<DepthOfField>(out dof)){ //Try to get the setting override
				dof = (DepthOfField)postProcessingProfile.AddSettings<DepthOfField>(); //Create one if it can't be found
			}
		}

		void OnEnable(){
			normalFocusDistance = dof.focusDistance.value;
			dof.focusDistance.value = focusDistance;
			dofActive = dof.enabled.value;
			dof.enabled.value = true;
		}
		void OnDisable(){
			dof.enabled.value = dofActive;
			dof.focusDistance.value = normalFocusDistance;
		}
	}
}
#endif
