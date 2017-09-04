// UI Pack : Toony LITE
// Version: 1.2.0
// Compatilble: Unity 5.5.1 or higher, see more info in Readme.txt file.
//
// Developer:			Gold Experience Team (https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:4162)
//
// Unity Asset Store:	https://www.assetstore.unity3d.com/en/#!/content/38836
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion // Namespaces

// ######################################################################
// UIPT_LITE_ParticleController class
//
// Creates ParticleSystem object in the scene when CreateParticle function is called.
// ######################################################################

public class UIPT_LITE_ParticleController : MonoBehaviour
{
	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Private reference which can be accessed by this class only
	private static UIPT_LITE_ParticleController instance;

	// Public static reference that can be accesd from anywhere
	public static UIPT_LITE_ParticleController Instance
	{
		get
		{
			// Check if instance has not been set yet and set it it is not set already
			// This takes place only on the first time usage of this reference
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<UIPT_LITE_ParticleController>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

	// ParticleSystem prefabs
	public ParticleSystem m_PrefabButton_01 = null;
	public ParticleSystem m_PrefabButton_02 = null;
	public ParticleSystem m_PrefabUseItem = null;
	public ParticleSystem m_PrefabFullFIll = null;

	#endregion // Variables

	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour

	// Awake is called when the script instance is being loaded.
	void Awake()
	{
		if (instance == null)
		{
			// Make the current instance as the singleton
			instance = this;

			// Make it persistent  
			DontDestroyOnLoad(this);
		}
		else
		{
			// If more than one singleton exists in the scene find the existing reference from the scene and destroy it
			if (this != instance)
			{
				Destroy(this.gameObject);
			}
		}
	}

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
	}

	#endregion // MonoBehaviour

	// ########################################
	// Functions
	// ########################################

	#region Functions

	// Create particle on a UI object
	public void CreateParticle(GameObject goParent, ParticleSystem goOriginal)
	{
		// Instantiate a ParticleSystem object
		ParticleSystem pParticle = (ParticleSystem)Instantiate(goOriginal);
		if (pParticle != null)
		{
			// Set parent
			pParticle.transform.SetParent(goParent.transform);

			// Set local position
			pParticle.transform.localPosition = new Vector3(0, 0, -1.0f);
			if (pParticle.main.playOnAwake == false)
			{
				// Play particle
				pParticle.Play();
			}

			// Destroy when it finished
			Destroy(pParticle.gameObject, pParticle.main.duration + 1.5f);
		}
	}

	#endregion // Functions
}
