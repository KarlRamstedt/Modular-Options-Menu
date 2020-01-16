#if (URP_PRESENT || HDRP_PRESENT)
using UnityEngine;
#if URP_PRESENT
using UnityEngine.Rendering.Universal;
#elif HDRP_PRESENT
using UnityEngine.Rendering.HighDefinition;
#endif
namespace ModularOptions {
	/// <summary>
	/// Blurs the background when parent object is Enabled. Works with Bokeh mode in URP and "Use Physical Camera" in HDRP.
	/// </summary>
	[AddComponentMenu("Modular Options/Misc/Menu Depth Of Field Blur")]
	public sealed class MenuDepthOfFieldBlur : MonoBehaviour {

		[Tooltip("Reference to global baseline profile.")]
		public UnityEngine.Rendering.VolumeProfile postProcessingProfile;

		[Range(0.01f, 9f)] public float focusDistance = 0.7f;

		DepthOfField dof;
		float normalFocusDistance;
		bool dofActive;

		void Awake(){
			if (!postProcessingProfile.TryGet<DepthOfField>(out dof)){ //Try to get the setting override
				dof = postProcessingProfile.Add<DepthOfField>(true); //Create one if it can't be found
				dof.active = false;
			}
		}

		void OnEnable(){
			normalFocusDistance = dof.focusDistance.value;
			dof.focusDistance.value = focusDistance;
			dofActive = dof.active;
			dof.active = true;
		}
		void OnDisable(){
			dof.active = dofActive;
			dof.focusDistance.value = normalFocusDistance;
		}
	}
}
#endif
