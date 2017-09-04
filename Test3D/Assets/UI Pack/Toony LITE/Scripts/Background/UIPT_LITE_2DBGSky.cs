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
// UIPT_LITE_2DBGSky class
//
// This class strengths sky sprite, fit the background.
// ######################################################################

public class UIPT_LITE_2DBGSky : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	// If you have error at this line on Unity 5.x, please make sure that you are using Unity 5.x with a valid license.
	RectTransform m_RectTransform = null;

	Vector2 m_OldSize;

	#endregion // Variables

	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
		InitMe();
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{
		// Check if the screen resolution was changed
		if (m_CameraRightEdge != m_ParentCanvasRectTransform.rect.width / 2 || m_CameraTopEdge != m_ParentCanvasRectTransform.rect.height / 2)
		{
			InitMe();
		}
	}

	#endregion // MonoBehaviour

	// ########################################
	// Utilities
	// ########################################

	#region Utilities

	// Initial sprite
	void InitMe()
	{
		// Search for parent Canvas and calculate camera view size
		FindParentCanvasAndCameraArea();

		m_RectTransform = (RectTransform)this.transform;
		this.transform.localScale = new Vector3((m_CameraRightEdge - m_CameraLeftEdge) / m_RectTransform.rect.width, (m_CameraTopEdge - m_CameraBottomEdge) / m_RectTransform.rect.height, 1);
	}

	// This class need Canvas to work properly.
	Canvas m_Parent_Canvas = null;

	// Edge position of camera perspective
	float m_CameraLeftEdge;
	float m_CameraRightEdge;
	float m_CameraTopEdge;
	float m_CameraBottomEdge;

	// If you have error at this line on Unity 5.x, please make sure that you are using Unity 5.x with a valid license.
	RectTransform m_ParentCanvasRectTransform = null;

	// Search for parent Canvas and calculate view size of camera 
	void FindParentCanvasAndCameraArea()
	{
		// Search for the parent Canvas
		if (m_Parent_Canvas == null)
			m_Parent_Canvas = GUIAnimSystemFREE.Instance.GetParent_Canvas(transform);

		// Calculate view size of camera 
		if (m_Parent_Canvas != null)
		{
			m_ParentCanvasRectTransform = m_Parent_Canvas.GetComponent<RectTransform>();

			m_CameraRightEdge = (m_ParentCanvasRectTransform.rect.width / 2);
			m_CameraLeftEdge = -m_CameraRightEdge;
			m_CameraTopEdge = (m_ParentCanvasRectTransform.rect.height / 2);
			m_CameraBottomEdge = -m_CameraTopEdge;
		}

	}

	#endregion // Utilities

}