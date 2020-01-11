using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Data that can live in your project and can be referenced by in-scene objects.
	/// Makes it a lot easier to maintain data for groups of items.
	/// </summary>
	[CreateAssetMenu(fileName = "UISoundData", menuName = "DataContainer/UI/SelectableSound")]
	public class SelectableUISoundData : ScriptableObject {

		public AudioClip submitSound;
		public AudioClip selectionSound;
		public AudioClip deselectionSound;
	}
}
