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
// UIPT_LITE_Demo_GUIPanel class
//
// Control Pause, Settings, News, Helps panels.
// ######################################################################

public class UIPT_LITE_Demo_GUIPanel : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	public Canvas[] m_CanvasesToDeactivated;                        // Canvases in this array will be ignored from raycasting.
	public bool m_ReactivateCanvasesWhenFinished = true;

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
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{
	}

	#endregion // MonoBehaviour

	// ########################################
	// Functions
	// ########################################

	#region Functions

	// Show this panel
	public void Show()
	{
		if (m_CanvasesToDeactivated != null)
		{
			if (m_CanvasesToDeactivated.Length > 0)
			{
				// Disable GraphicRaycaster of Canvas in m_CanvasesToDeactivated
				foreach (Canvas child in m_CanvasesToDeactivated)
				{
					// Disable GraphicRaycasters of Canvas in child of m_CanvasesToDeactivated
					// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(child, false);
				}
			}
		}

		// Enable GraphicRaycasters of Canvas in this.gameObject
		// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(this.gameObject, true);

		// Play MoveIn animation
		GUIAnimSystemFREE.Instance.MoveIn(this.transform, true);
	}

	// Hide this panel
	public void Hide()
	{
		// Disable GraphicRaycasters of Canvas in this.gameObject
		// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(this.gameObject, false);

		if (m_CanvasesToDeactivated != null && m_ReactivateCanvasesWhenFinished == true)
		{
			if (m_CanvasesToDeactivated.Length > 0)
			{
				foreach (Canvas child in m_CanvasesToDeactivated)
				{
					// Enable GraphicRaycasters of Canvas in child of m_CanvasesToDeactivated
					// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(child, true);
				}
			}
		}

		// Play Move Out animation
		GUIAnimSystemFREE.Instance.MoveOut(this.transform, true);
	}

	#endregion // Functions
}
