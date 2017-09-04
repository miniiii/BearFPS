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

#endregion

// ######################################################################
// UIPT_LITE_Demo01_IntroAndLoading class
//
// Handles "Demo 01 - Landscape - Intro & Loading" and "Demo 01 - Portrait - Intro & Loading" scenes.
// ######################################################################

public class UIPT_LITE_Demo01_IntroAndLoading : MonoBehaviour
{
	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Intro
	public GameObject m_Intro = null;

	// Loading
	public GameObject m_Loading = null;
	public Slider m_Slider = null;

	// Time
	public float m_ShowLogoTime = 2.0f;
	public float m_ShowLoadingTime = 1.0f;
	public float m_IdleLoadingTime = 1.0f;

	// Loading Progress
	private AsyncOperation m_Async = null;

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
				string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
				if (CurrentLevel.Contains("Landscape") == true)
					Screen.SetResolution(960, 600, false);
				else
					Screen.SetResolution(540, 960, false);
			}
		}

		// Deactivates m_Intro and m_Loading GameObjects.
		m_Intro.SetActive(false);
		m_Loading.SetActive(false);
	}

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
		// Update Loading progress bar.
		StartCoroutine(ShowLogo());

		// Reset Loading progress bar to zero.
		m_Slider.value = 0.0f;
	}

	// Update is called once per frame.
	void Update()
	{
		// Update Loading progress bar.
		if (m_Async != null)
		{
			// Update Loading progress bar.
			if (m_Async.progress < 0.9f)
				m_Slider.value = m_Async.progress;
			else
				m_Slider.value = 1.0f;
		}
	}

	#endregion // MonoBehaviour

	// ########################################
	// Functions
	// ########################################

	#region Functions

	IEnumerator ShowLogo()
	{
		// Activate m_Intro GameObject then animate MoveIn animation.
		m_Intro.gameObject.SetActive(true);

		// Play MoveIn animation
		GUIAnimSystemFREE.Instance.MoveIn(m_Intro.transform, true);

		// Creates a yield instruction to wait for a given number of seconds
		// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
		yield return new WaitForSeconds(m_ShowLogoTime);

		// Start ShowLoading() coroutine.
		StartCoroutine(ShowLoading());
	}

	IEnumerator ShowLoading()
	{
		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(m_Intro.transform, true);

		// Creates a yield instruction to wait for a given number of seconds
		// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
		yield return new WaitForSeconds(m_ShowLoadingTime);

		// Start IdleLoading() coroutine.
		StartCoroutine(IdleLoading());
	}

	IEnumerator IdleLoading()
	{
		// Activate m_Loading GameObject then animate MoveIn animation.
		m_Loading.SetActive(true);

		// Play MoveIn animation
		GUIAnimSystemFREE.Instance.MoveIn(m_Loading.transform, true);

		// Creates a yield instruction to wait for a given number of seconds
		// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
		yield return new WaitForSeconds(m_IdleLoadingTime);

		// Start LoadNextScene() coroutine.
		if (Application.HasProLicense())
		{
			// NOTE: Asynchronous Background loading is only supported in Unity Pro.
			StartCoroutine(LoadNextSceneAsync());
		}
		else
		{
			// NOTE: Asynchronous Background loading is only supported in Unity Pro.
			LoadNextScene();
		}
	}

	IEnumerator LoadNextSceneAsync()
	{
		// Reset Loading progress bar to zero.
		m_Slider.value = 0.0f;

		// Load next scene asynchronously in the background.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";

		// NOTE: Asynchronous Background loading is only supported in Unity Pro.
		m_Async = SceneManager.LoadSceneAsync("ToonyLITE Demo 02 - " + OrientationName + " - Home");

		yield return m_Async;
	}

	void LoadNextScene()
	{
		// Reset Loading progress bar to zero.
		m_Slider.value = 0.0f;

		// Load next scene asynchronously in the background.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";

		// NOTE: Asynchronous Background loading is only supported in Unity Pro.
		if (Application.HasProLicense())
		{
			// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
			// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html
			SceneManager.LoadSceneAsync("ToonyLITE Demo 02 - " + OrientationName + " - Home");
		}
		else
		{
			// Use SceneManager.LoadScene instead of LoadLevelAsync here.
			// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
			// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
			// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.LoadScene.html
			SceneManager.LoadScene("ToonyLITE Demo 02 - " + OrientationName + " - Home");
		}
	}

	#endregion // Functions
}
