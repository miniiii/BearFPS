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
using UnityEngine.SceneManagement;  // Add since Unity 5.3, see http://docs.unity3d.com/Manual/UpgradeGuide53.html and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
using UnityEngine.UI;

#endregion // Namespaces

// ######################################################################
// UIPT_LITE_Demo02_Home class
//
// Handles "Demo 02 - Landscape - Home" and "Demo 02 - Portrait - Home" scenes.
// ######################################################################

public class UIPT_LITE_Demo02_Home : MonoBehaviour
{
	// ########################################
	// Variables
	// ########################################

	#region Variables

	// UIPT_LITE_Demo_Settings
	public UIPT_LITE_Demo_Settings m_Settings = null;

	// UIPT_LITE_Demo_News
	public UIPT_LITE_Demo_News m_News = null;

	// UIPT_LITE_Demo_Help
	public UIPT_LITE_Demo_Help m_Help = null;

	// Buttons
	public GUIAnimFREE m_CheckOutGUIAnimatorButton = null;

	// Timer
	private float m_CheckOutGUIAnimatorCheckOutButtonShowTime = 15.0f;

	#endregion // Variables

	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour

	// Awake is called when the script instance is being loaded.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
	void Awake()
	{
		// Set GUIAnimSystemFREE.Instance.m_AutoAnimation to false, 
		// this will let you control all GUI Animator elements in the scene via scripts.
		if (enabled)
		{
			GUIAnimSystemFREE.Instance.m_GUISpeed = 4.0f;
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}

		// If this class is not running on Unity Editor, the resolution will be change to 960x600px for Lanscape demo scene or 540x960px for Portrait demo scene
		if (Application.isEditor == false)
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
			{
				// Load next scene according to orientation of current scene.
				string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
				if (CurrentLevel.Contains("Landscape") == true)
					Screen.SetResolution(960, 600, false);
				else
					Screen.SetResolution(540, 960, false);
			}
		}

		// Activate all UI Canves GameObjects.
		if (m_Help.gameObject.activeSelf == false)
			m_Help.gameObject.SetActive(true);

		if (m_News.gameObject.activeSelf == false)
			m_News.gameObject.SetActive(true);

		if (m_Settings.gameObject.activeSelf == false)
			m_Settings.gameObject.SetActive(true);

	}

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
		// Update Loading progress bar.
		StartCoroutine(Show());
	}

	// Update is called once per frame.
	void Update()
	{
		// Count down to hide Check Out Button
		if (m_CheckOutGUIAnimatorCheckOutButtonShowTime > 0.0f)
		{
			m_CheckOutGUIAnimatorCheckOutButtonShowTime -= Time.deltaTime;
			if (m_CheckOutGUIAnimatorCheckOutButtonShowTime < 0.0f)
			{
				GUIAnimSystemFREE.Instance.EnableButton(m_CheckOutGUIAnimatorButton.transform, false);

				// Play Move Out animation
				m_CheckOutGUIAnimatorButton.MoveOut();
			}
		}
	}

	#endregion // MonoBehaviour

	// ########################################
	// UI Responder functions
	// ########################################

	#region UI Responder

	public void Button_Help()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		// Show this panel
		m_Help.Show();
	}

	public void Button_Credits()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		GUIAnimSystemFREE.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(this.transform, true);

		// Load next scene according to orientation of current scene.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";
		GUIAnimSystemFREE.Instance.LoadLevel("ToonyLITE Demo 09 - " + OrientationName + " - Credits", 1.5f);
	}

	public void Button_Social()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		// Keep particles stay alive until it finished playing.
		GUIAnimSystemFREE.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(this.transform, true);

		// Load next scene according to orientation of current scene.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";
		GUIAnimSystemFREE.Instance.LoadLevel("ToonyLITE Demo 07 - " + OrientationName + " - Social", 1.5f);
	}

	public void Button_News()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		// Show this panel
		m_News.Show();
	}

	public void Button_CheckoutGUIAnimator()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();
/*
#if UNITY_EDITOR
		// http://docs.unity3d.com/ScriptReference/Application.OpenURL.html
		Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/28709");
#else
		// http://docs.unity3d.com/ScriptReference/Application.ExternalEval.html
		Application.ExternalEval("window.open('https://www.assetstore.unity3d.com/en/#!/content/28709','GUI Animator for Unity UI')");
#endif
*/
        Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/28709");

    }

    public void Button_Play(GameObject goButton)
	{
		// Play Play button sound
		UIPT_LITE_SoundController.Instance.Play_SoundPlay();

		int ParticleIndex = Random.Range(0, 2);
		if (ParticleIndex == 0)
			UIPT_LITE_ParticleController.Instance.CreateParticle(goButton, UIPT_LITE_ParticleController.Instance.m_PrefabButton_01);
		else
			UIPT_LITE_ParticleController.Instance.CreateParticle(goButton, UIPT_LITE_ParticleController.Instance.m_PrefabButton_02);

		// Keep particles stay alive until it finished playing.
		GUIAnimSystemFREE.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(this.transform, true);

		// Load next scene according to orientation of current scene.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";
		GUIAnimSystemFREE.Instance.LoadLevel("ToonyLITE Demo 03 - " + OrientationName + " - Level Select", 1.5f);
	}

	public void Button_Settings()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		// Show this panel
		m_Settings.Show();
	}

	public void Button_Ranks()
	{
		GUIAnimSystemFREE.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(this.transform, true);

		// Load next scene according to orientation of current scene.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";
		GUIAnimSystemFREE.Instance.LoadLevel("ToonyLITE Demo 05 - " + OrientationName + " - Ranks", 1.5f);
	}

	public void Button_Achievement()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		// Keep particles stay alive until it finished playing.
		GUIAnimSystemFREE.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(this.transform, true);

		// Load next scene according to orientation of current scene.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";
		GUIAnimSystemFREE.Instance.LoadLevel("ToonyLITE Demo 06 - " + OrientationName + " - Achievements", 1.5f);
	}

	public void Button_More()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		GUIAnimSystemFREE.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(this.transform, true);

		// Load next scene according to orientation of current scene.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";
		GUIAnimSystemFREE.Instance.LoadLevel("ToonyLITE Demo 08 - " + OrientationName + " - More Samples", 1.5f);
	}

	public void Button_Shop()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		// Keep particles stay alive until it finished playing.
		GUIAnimSystemFREE.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(this.transform, true);

		// Load next scene according to orientation of current scene.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";
		GUIAnimSystemFREE.Instance.LoadLevel("ToonyLITE Demo 10 - " + OrientationName + " - Shop", 1.5f);
	}

	#endregion // UI Responder

	// ########################################
	// Functions
	// ########################################

	#region Functions

	IEnumerator Show()
	{
		// Play MoveIn animation
		GUIAnimSystemFREE.Instance.MoveIn(this.transform, true);

		// Creates a yield instruction to wait for a given number of seconds
		// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
		yield return new WaitForSeconds(1.0f);

		// Play particles in the hierarchy of given transfrom
		GUIAnimSystemFREE.Instance.PlayParticle(this.transform);
	}

	#endregion // Functions
}
