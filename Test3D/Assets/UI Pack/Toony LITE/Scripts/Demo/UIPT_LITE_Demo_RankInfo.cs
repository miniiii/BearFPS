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

#endregion // Namespaces

// ######################################################################
// UIPT_LITE_Demo_RankInfo class
//
// Describes information of each rank
// ######################################################################

public class UIPT_LITE_Demo_RankInfo : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	public Text m_TextRank = null;
	public Text m_TextPlayerName = null;
	public Text m_TextBestScore = null;
	public Image m_Portrait = null;

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

	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{

	}

	#endregion // MonoBehaviour

	// ########################################
	// Functions functions
	// ########################################

	#region Functions
	
	// Bind children objects to this object's variables.
	void BindGameObjects(Transform trans)
	{
		switch (trans.name)
		{
			case "TextRank":
				m_TextRank = trans.gameObject.GetComponent<Text>();
				break;
			case "TextPlayerName":
				m_TextPlayerName = trans.gameObject.GetComponent<Text>();
				break;
			case "TextBestScore":
				m_TextBestScore = trans.gameObject.GetComponent<Text>();
				break;
			case "Portrait":
				m_Portrait = trans.gameObject.GetComponent<Image>();
				break;
		}

		// Bind objects for children
		foreach (Transform child in trans)
		{
			BindGameObjects(child.transform);
		}
	}

	// Set information to Rank.
	// Note this function have to be called anytime after BindGameObjects is called.
	public void SetInfo(string Number, string Name, string Score, Sprite ProtraitSprite)
	{
		// Bind children objects to this object's variables.
		BindGameObjects(this.transform);

		m_TextRank.text = Number;
		m_TextPlayerName.text = Name;
		m_TextBestScore.text = Score;
		m_Portrait.sprite = ProtraitSprite;
	}
	
	#endregion // Functions
}
