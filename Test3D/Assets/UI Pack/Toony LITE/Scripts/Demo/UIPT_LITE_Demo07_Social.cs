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
// UIPT_LITE_Demo07_Social class
//
// Handles "Demo 07 - Landscape - Social" and "Demo 07 - Portrait - Social" scenes.
// ######################################################################

public class UIPT_LITE_Demo07_Social : MonoBehaviour
{
	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Tabs
	public Image m_ImageButtonFriends = null;
	public Image m_ImageButtonGifts = null;

	// Texts
	public Text m_TextFriendDetails = null;
	public Text m_TextButtonFriends = null;
	public Text m_TextButtonGifts = null;

	// GameObjects
	public GameObject m_PanelFriends = null;
	public GameObject m_PanelGifts = null;

	// Contents
	public Image m_PanelContentFriends = null;
	public Image m_PanelContentGifts = null;

	// Scroll
	public Scrollbar m_Scrollbar = null;

	// Status
	private int m_CurrentTab = 0;

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
	}

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
		// Swith tab, show/hide and update information of each content
		SwitchTab();

		// Play MoveIn animation
		GUIAnimSystemFREE.Instance.MoveIn(this.transform, true);
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{
	}

	#endregion // MonoBehaviour

	// ########################################
	// UI Responder functions
	// ########################################

	#region UI Responder

	public void Button_Home()
	{
		// Play Back button sound
		UIPT_LITE_SoundController.Instance.Play_SoundBack();

		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(this.transform, true);

		// Keep particles stay alive until it finished playing.
		GUIAnimSystemFREE.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

		// Load next scene according to orientation of current scene.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.
		string OrientationName = "Portrait";
		if (CurrentLevel.Contains("Landscape") == true)
			OrientationName = "Landscape";
		GUIAnimSystemFREE.Instance.LoadLevel("ToonyLITE Demo 02 - " + OrientationName + " - Home", 1.5f);
	}

	public void Button_Friends()
	{
		// Play Tap button sound
		UIPT_LITE_SoundController.Instance.Play_SoundTap();

		// Set current tab to 0
		m_CurrentTab = 0;

		// Swith tab, show/hide and update information of each content
		SwitchTab();
	}

	public void Button_Gifts()
	{
		// Play Tap button sound
		UIPT_LITE_SoundController.Instance.Play_SoundTap();

		// Set current tab to 1
		m_CurrentTab = 1;

		// Swith tab, show/hide and update information of each content
		SwitchTab();
	}

	public void Button_Invite()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();
	}

	public void Button_Send()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();
	}

	public void Scrollbar_ValueChanged()
	{
		// Load next scene according to orientation of current scene.
		string CurrentLevel = SceneManager.GetActiveScene().name; // No longer use Application.loadedLevelName in Unity 5.3 or higher.

		// Lanscape demo scene
		if (CurrentLevel.Contains("Landscape") == true)
		{
			if (m_CurrentTab == 0)
			{
				RectTransform pRectTransform = m_PanelContentFriends.transform.GetComponent<RectTransform>();
				if (pRectTransform != null)
					pRectTransform.anchoredPosition = new Vector3(0, 840.0f * m_Scrollbar.value, 0);
			}
			else if (m_CurrentTab == 1)
			{
				RectTransform pRectTransform = m_PanelContentGifts.transform.GetComponent<RectTransform>();
				if (pRectTransform != null)
					pRectTransform.anchoredPosition = new Vector3(0, 840.0f * m_Scrollbar.value, 0);
			}
		}
		// Portrait demo scene
		else
		{
			if (m_CurrentTab == 0)
			{
				RectTransform pRectTransform = m_PanelContentFriends.transform.GetComponent<RectTransform>();
				if (pRectTransform != null)
					pRectTransform.anchoredPosition = new Vector3(0, 280.0f * m_Scrollbar.value, 0);
			}
			else if (m_CurrentTab == 1)
			{
				RectTransform pRectTransform = m_PanelContentGifts.transform.GetComponent<RectTransform>();
				if (pRectTransform != null)
					pRectTransform.anchoredPosition = new Vector3(0, 280.0f * m_Scrollbar.value, 0);
			}
		}
	}

	#endregion // UI Responder

	// ########################################
	// Functions
	// ########################################

	#region Functions

	// Swith tab, show/hide and update information of each content
	void SwitchTab()
	{
		if (m_CurrentTab == 0)
		{
			m_ImageButtonFriends.color = new Color(1, 1, 1, 1);
			m_TextButtonFriends.color = new Color(1, 1, 1, 1);

			m_ImageButtonGifts.color = new Color(0.75f, 0.75f, 0.75f, 1);
			m_TextButtonGifts.color = new Color(0.0f, 0.0f, 0.0f, 1);

			m_TextFriendDetails.text = "5/50 friends";

			m_Scrollbar.value = 0;
			m_PanelFriends.SetActive(true);
			m_PanelGifts.SetActive(false);
		}
		else if (m_CurrentTab == 1)
		{
			m_ImageButtonFriends.color = new Color(0.75f, 0.75f, 0.75f, 1);
			m_TextButtonFriends.color = new Color(0.0f, 0.0f, 0.0f, 1);

			m_ImageButtonGifts.color = new Color(1, 1, 1, 1);
			m_TextButtonGifts.color = new Color(1, 1, 1, 1);

			m_TextFriendDetails.text = "5/50 friends";

			m_Scrollbar.value = 0;
			m_PanelFriends.SetActive(false);
			m_PanelGifts.SetActive(true);
		}
	}

	#endregion // Functions
}
