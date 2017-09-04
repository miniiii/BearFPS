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

using UnityEngine.UI;

#endregion // Namespaces

// ######################################################################
// UIPT_LITE_BGFindCanvas class
//
// This class searchs for Canvas in this object and also seach for a Camera that render "Ignore Raycast" layer then set Camera to the worldCamera variable of the Canvas.
// It is use with Canvas_Background_Landscape and Canvas_Background_Portrait.
// ######################################################################

public class UIPT_LITE_BGFindCanvas : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	Canvas m_Canvas = null;

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
		// Search for Canvas component of this object.
		if (m_Canvas == null)
			m_Canvas = gameObject.GetComponent<Canvas>();

		// If there is Canvas component then assign a Camera that render "Ignore Raycast" layer to it.
		if (m_Canvas != null)
		{
			if (m_Canvas.worldCamera == null)
			{
				FindCamera();
			}
		}
	}

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
		// Search for Canvas component of this object.
		if (m_Canvas == null)
			m_Canvas = gameObject.GetComponent<Canvas>();

		// If there is Canvas component then assign a Camera that render "Ignore Raycast" layer to it.
		if (m_Canvas != null)
		{
			if (m_Canvas.worldCamera == null)
			{
				FindCamera();
			}
		}
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{
		// If there is Canvas component then assign a Camera that render "Ignore Raycast" layer to it.
		if (m_Canvas != null)
		{
			if (m_Canvas.worldCamera == null)
			{
				FindCamera();
			}
		}
	}

	#endregion // MonoBehaviour

	// ########################################
	// Utilities
	// ########################################

	#region Utilities

	// Look for a Camera that render "Ignore Raycast" layer 
	void FindCamera()
	{
		Camera[] pCameraList = GameObject.FindObjectsOfType<Camera>();

		foreach (Camera child in pCameraList)
		{
			if (child.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
			{
				// Assign a Camera that render "Ignore Raycast" layer to worldCamera variable of the Canvas
				m_Canvas.worldCamera = child;
			}
		}

	}

	#endregion // Utilities

}
