using UnityEngine;
using UnityEngine.EventSystems;

namespace ModularOptions {
	/// <summary>
	/// Put on whatever UI element you want to trigger a sound when interacting with.
	/// </summary>
	[AddComponentMenu("Modular Options/Selectable UI Sound")]
	[RequireComponent(typeof(UnityEngine.UI.Selectable), typeof(AudioSource))]
	public class SelectableUISound : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler, ISelectHandler, IDeselectHandler {

		[Tooltip("Reference to ScriptableObject containing sound data. Create a new one by right-clicking in the Project-window and clicking DataContainer/UI/SelectableSound")]
		public SelectableUISoundData soundData;

		AudioSource audioSource;
		void Awake(){
			audioSource = GetComponent<AudioSource>();
		}

		public void OnPointerClick(PointerEventData _eventData){ //For mouse use
			PlayIfNotNull(soundData.submitSound);
		}
		public void OnPointerEnter(PointerEventData _eventData){
			PlayIfNotNull(soundData.selectionSound);
		}
		public void OnPointerExit(PointerEventData _eventData){
			PlayIfNotNull(soundData.deselectionSound);
		}
		public void OnSubmit(BaseEventData _eventData){ //For non-mouse use (for example controllers)
			PlayIfNotNull(soundData.submitSound);
		}
		public void OnSelect(BaseEventData _eventData){ //NOTE: This will trigger in addition to OnPointerClick if clicking with mouse, since it moves the selector to the clicked Selectable
			PlayIfNotNull(soundData.selectionSound);
		}
		public void OnDeselect(BaseEventData _eventData){
			PlayIfNotNull(soundData.deselectionSound);
		}
		
		/// <summary>
		/// Plays OneShot if clip reference isn't null. Allows empty fields for no sound on those actions.
		/// </summary>
		void PlayIfNotNull(AudioClip _clip){
			if (_clip != null)
				audioSource.PlayOneShot(_clip);
		}
	}
}

// For FMOD you'll just use these instead:
// [FMODUnity.EventRef] public string soundEvent;
// 	FMODUnity.RuntimeManager.PlayOneShot(soundEvent);
