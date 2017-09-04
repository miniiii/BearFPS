// UI Pack : Toony LITE
// Version: 1.2.0
// Compatilble: Unity 5.5.1 or higher, see more info in Readme.txt file.
//
// Developer:			Gold Experience Team (https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:4162)
//
// Unity Asset Store:	https://www.assetstore.unity3d.com/en/#!/content/38836
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

using UnityEngine;

using System.Collections;

#region Namespaces

using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Add since Unity 5.3, see http://docs.unity3d.com/Manual/UpgradeGuide53.html and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html

#endregion // Namespaces

// ######################################################################
// UIPT_LITE_Demo_GUIPanel class
//
// Contains HelpText array to display in Helps panel.
// ######################################################################

public class UIPT_LITE_Demo_Help : UIPT_LITE_Demo_GUIPanel
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Help texts class describes title and details of each help.
	[System.Serializable]           // Embed this class with sub properties in the inspector. http://docs.unity3d.com/ScriptReference/Serializable.html
	public class HelpText
	{
		public string Title;
		public string Details;
	}

	// Texts
	public Text m_HelpTitle = null;
	public Text m_HelpDetails = null;

	// Help texts
	public HelpText[] m_HelpText;

	// status
	private int m_ShowingHelpTextIndex = 0;

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
		// Set Help index to first one.
		m_ShowingHelpTextIndex = 0;

		// Replace <br> in details with new line 
		m_HelpTitle.text = m_HelpText[m_ShowingHelpTextIndex].Title.Replace("<br>", "\n");          // Replace <br> in details with new line 
		m_HelpDetails.text = m_HelpText[m_ShowingHelpTextIndex].Details.Replace("<br>", "\n");          // Replace <br> in details with new line 
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

	public void Button_Close()
	{
		// Play Back button sound
		UIPT_LITE_SoundController.Instance.Play_SoundBack();

		// Hide this panel
		Hide();
	}

	public void Button_Left()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		// Decrease index
		m_ShowingHelpTextIndex--;
		if (m_ShowingHelpTextIndex < 0)
			m_ShowingHelpTextIndex = m_HelpText.Length - 1;

		// Update Title and Deatils according to current index
		m_HelpTitle.text = m_HelpText[m_ShowingHelpTextIndex].Title.Replace("<br>", "\n");          // Replace <br> in details with new line 
		m_HelpDetails.text = m_HelpText[m_ShowingHelpTextIndex].Details.Replace("<br>", "\n");          // Replace <br> in details with new line 
	}

	public void Button_Right()
	{
		// Play Click sound
		UIPT_LITE_SoundController.Instance.Play_SoundClick();

		// Increase index
		m_ShowingHelpTextIndex++;
		if (m_ShowingHelpTextIndex >= m_HelpText.Length)
			m_ShowingHelpTextIndex = 0;

		// Update Title and Deatils according to current index
		m_HelpTitle.text = m_HelpText[m_ShowingHelpTextIndex].Title.Replace("<br>", "\n");          // Replace <br> in details with new line 
		m_HelpDetails.text = m_HelpText[m_ShowingHelpTextIndex].Details.Replace("<br>", "\n");          // Replace <br> in details with new line 
	}

	#endregion // UI Responder
}
