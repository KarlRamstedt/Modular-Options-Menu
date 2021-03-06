#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ModularOptions {
	public abstract class PostProcessingStackDropdown<T> : DropdownOption where T : PostProcessEffectSettings {

		[Tooltip("Reference to global baseline profile.")]
		public PostProcessProfile postProcessingProfile;

		protected T setting;

		protected override void Awake(){
			if (!postProcessingProfile.TryGetSettings<T>(out setting)){ //Try to get the setting
				setting = postProcessingProfile.AddSettings<T>(); //Create it if it can't be found
				setting.SetAllOverridesTo(true);
			}
			base.Awake();
		}

#if UNITY_EDITOR
		/// <summary>
		/// Auto-assign reference. Replace it in the editor if it was incorrect.
		/// </summary>
		protected override void Reset(){
			postProcessingProfile = Resources.FindObjectsOfTypeAll<PostProcessProfile>()[0];
			base.Reset();
		}
#endif
    }
}
#endif
