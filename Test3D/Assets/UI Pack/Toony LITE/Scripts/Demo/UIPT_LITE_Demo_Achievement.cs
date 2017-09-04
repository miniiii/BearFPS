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
// UIPT_LITE_Demo_Achievement class
//
// Describes information of archievement.
// ######################################################################

public class UIPT_LITE_Demo_Achievement : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	public Image m_AchievePanel = null;
	public Image m_MedalBG = null;
	public Image m_Medal = null;
	public Text m_TextMission = null;
	public Text m_TextPoint = null;

	#endregion // Variables
	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour

	// Use this for initialization
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

	// Bind children objects to this object's variables.
	void BindGameObjects(Transform trans)
	{
		if (trans.name.Contains("AchieveInfo_"))
		{
			m_AchievePanel = trans.gameObject.GetComponent<Image>();
		}
		else
		{
			switch (trans.name)
			{
				case "MedalBG":
					m_MedalBG = trans.gameObject.GetComponent<Image>();
					break;
				case "Medal":
					m_Medal = trans.gameObject.GetComponent<Image>();
					break;
				case "TextMission":
					m_TextMission = trans.gameObject.GetComponent<Text>();
					break;
				case "TextPoint":
					m_TextPoint = trans.gameObject.GetComponent<Text>();
					break;
			}
		}

		// Bind objects for children
		foreach (Transform child in trans)
		{
			BindGameObjects(child.transform);
		}
	}

	// Set information to Archievement.
	// Note this function have to be called anytime after BindGameObjects is called.
	public void SetInfo(bool Completed, string Misson, int Point)
	{
		// Bind children objects to this object's variables.
		BindGameObjects(this.transform);

		if (Completed == true)
		{
			m_AchievePanel.color = new Color(1, 1, 1, 1);
			m_MedalBG.color = new Color(1, 1, 1, 1);
			m_Medal.color = new Color(1, 1, 1, 1);
			m_TextMission.color = new Color(0.74f, 0.26f, 0.63f, 1);
			m_TextPoint.color = new Color(0, 0.59f, 0.82f, 1);
		}
		else
		{
			m_AchievePanel.color = new Color(0.5f, 0.5f, 0.5f, 1);
			m_MedalBG.color = new Color(0.5f, 0.5f, 0.5f, 1);
			m_Medal.color = new Color(0.2f, 0.2f, 0.2f, 1);
			m_TextMission.color = new Color(1, 1, 1, 1);
			m_TextPoint.color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
		}
		m_TextMission.text = Misson;
		m_TextPoint.text = Point.ToString() + "p";
	}

	#endregion // Functions
}
