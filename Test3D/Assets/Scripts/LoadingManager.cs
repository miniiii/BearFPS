using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour {

    public UISlider _loadingPercent;
    public UILabel _loadingText;
    public UILabel _animText;

    AsyncOperation async;
    //TypewriterEffect typeEffect;

    Transform _characterTr;
    bool isLoadGame = false;
    
    public IEnumerator StartLoad(string strSceneName)
    {
        yield return new WaitForSeconds(1.0f);
        async = SceneManager.LoadSceneAsync(strSceneName);
        async.allowSceneActivation = false;

       
        if (isLoadGame == false)
        {
            isLoadGame = true;

            
            while(!async.isDone)
            {
                float p = async.progress * 100f;

                int pRounded = Mathf.RoundToInt(p);

                _loadingText.text = pRounded.ToString() + "%";
                
                _loadingPercent.value = async.progress;
                
                if(async.progress == 0.9f)
                {
                    async.allowSceneActivation = true;
                    
                }
                yield return true;
            }
        }
    }
    int characterIdx;
    private void Awake()
    {
        //typeEffect = GetComponent<TypewriterEffect>();
        //_characterTr = GameObject.Find("Character").GetComponent<Transform>();
        //characterIdx = Random.Range(0, 3);

        ////춤추는 캐릭터 생성
        //GameObject Character = Instantiate(Resources.Load(characterIdx.ToString(), typeof(GameObject))) as GameObject;

        //Character.name = "characterPrefab";
        //Character.transform.parent = _characterTr;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine("StartLoad", "LoadingScene");

	}


    float fTime = 0.0f;

	// Update is called once per frame
	void Update () {
        fTime += Time.deltaTime;
       
        _loadingPercent.value = fTime;

        if (fTime >= 5.0f)
        {
            async.allowSceneActivation = true;
        }
        //Debug.Log(_loadingPercent.value);
    }
}
