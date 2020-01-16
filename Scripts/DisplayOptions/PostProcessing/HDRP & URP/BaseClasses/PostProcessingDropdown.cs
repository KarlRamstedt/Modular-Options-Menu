#if (URP_PRESENT || HDRP_PRESENT)
using UnityEngine;
using UnityEngine.Rendering;

namespace ModularOptions {
	public abstract class PostProcessingDropdown<T> : DropdownOption where T : VolumeComponent {

		[Tooltip("Reference to global baseline profile.")]
		public VolumeProfile postProcessingProfile;

		protected T setting;
		
		protected override void Awake(){
			if (!postProcessingProfile.TryGet<T>(out setting)) //Try to get the setting
				setting = postProcessingProfile.Add<T>(true); //Create it if it can't be found
			base.Awake();
		}

#if UNITY_EDITOR
		/// <summary>
		/// Auto-assign reference. Replace it in the editor if it was incorrect.
		/// </summary>
		protected override void Reset(){
			postProcessingProfile = Resources.FindObjectsOfTypeAll<VolumeProfile>()[0];
			base.Reset();
		}
#endif
	}
}
#endif
