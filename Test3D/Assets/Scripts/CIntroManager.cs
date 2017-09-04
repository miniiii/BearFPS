using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CIntroManager : MonoBehaviour {

    UIWidget _pressAnyKey;
    UIWidget _mainMenu;
    UIWidget _modeSelect;
    UIWidget _matching;

    public GameObject _mainMenuObj;
    public GameObject _pressAnyObj;
    public GameObject _modeSelectObj;
    public GameObject _matchingObj;

    public AudioClip _introBG;
    public AudioClip _btnClickedSound;

    private AudioSource _audio;
   


    private void Awake()
    {
        _pressAnyKey = _pressAnyObj.GetComponent<UIWidget>();
        _mainMenu = _mainMenuObj.GetComponent<UIWidget>();
        _modeSelect = _modeSelectObj.GetComponent<UIWidget>();
        _matching = _matchingObj.GetComponent<UIWidget>();
        _audio = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
       
    }
    // 로딩화면으로 이동하는 코루틴(게임시작)
    private IEnumerator GoLoadingSceneCoroutine()
    {
        PhotonCloudConnect();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LoadingScene");
    }

    // 인트로 시작후 출력되는 Press To start 화면 클릭시
    public void PressAnyKeySpriteClicked()
    {
        _audio.PlayOneShot(_btnClickedSound);
        // pressAnykey 위젯 닫고 메인메뉴 open
        StartCoroutine(FadeIn(_mainMenu, _pressAnyKey));
    }

    
    // 게임 시작 버튼이 클릭됐을 때
    public void OnPlayBtnClicked()
    {
        _audio.PlayOneShot(_btnClickedSound);
        StartCoroutine(FadeIn(_modeSelect, _mainMenu));
    }

    // 매칭 팝업에서 취소버튼이 눌렸을 때
    public void MatchingCancelBtnClicked()
    {
        _audio.PlayOneShot(_btnClickedSound);
        StartCoroutine(FadeIn(_modeSelect, _matching));
    }

    // 매칭 팝업에서 바로 시작버튼이 눌렸을 때
    public void MatchingPlayNowBtnClicked()
    {
        _audio.PlayOneShot(_btnClickedSound);
        StartCoroutine("GoLoadingSceneCoroutine");
    }


    // 랭킹 버튼 클릭 됐을 때
    public void OnRankingBtnClick()
    {
        _audio.PlayOneShot(_btnClickedSound);
    }

    // 싱글 플레이버튼이 눌렸을 때
    public void OnSinglePlayBtnClick()
    {
        _audio.PlayOneShot(_btnClickedSound);
        StartCoroutine(FadeIn(_matching, _modeSelect));

        
    }

    public void OnBackBtnClick()
    {
        _audio.PlayOneShot(_btnClickedSound);
        StartCoroutine(FadeIn(_mainMenu, _modeSelect));
    }

    // Fadeout 이펙트 코루틴
    IEnumerator FadeOut(UIWidget widget)
    {
        widget.alpha = 1.0f;
        while (widget.alpha > 0.0f)
        {
            yield return null;
            widget.alpha -= Time.deltaTime * 2.0f;
            
        }
        widget.gameObject.SetActive(false);
    }

    // FadeIn 이펙트 코루틴
    IEnumerator FadeIn(UIWidget openWidget, UIWidget closeWidget)
    {
        yield return StartCoroutine("FadeOut", closeWidget);
        openWidget.gameObject.SetActive(true);
        openWidget.alpha = 0.0f;
        while(openWidget.alpha < 1.0f)
        {
            yield return null;
            openWidget.alpha += Time.deltaTime * 2.0f;
        }
    }


    ///////////////////////// 포톤 클라우드 관련 ///////////////////////////

    // 포톤 클라우드 접속
    // 클라우드 접속 여부를 확인하고 접속을 수행함
    public void PhotonCloudConnect()
    {
        // 이미 포톤 클라우드에 접속된 상태가 아니라면
        if (!PhotonNetwork.connected)
        {
            // 포톤 클라우드 접속함
            PhotonNetwork.ConnectUsingSettings("v1.0");
            Debug.Log("포톤 접속");
        }
    }


    // 포톤 로비 입장시 자동적으로 실행하는 이벤트
    private void OnJoinedLobby()
    {
        Debug.Log("로비 접속");

        // 방 생성 및 접속(이미 생성되어 있을 경우)
        PhotonNetwork.JoinOrCreateRoom(
            "dev", // 방제목
            new RoomOptions() // 방 옵션 정보
            {
                MaxPlayers = 10, // 최대 접속자 수
                IsOpen = true, // 공개 여부
                IsVisible = true // 검색 여부
            },
            TypedLobby.Default // 로비 타입(기본)
            );
    }


    // 방 입장에 성공시 호출되는 포톤 이벤트 메소드
    private void OnJoinedRoom()
    {
        Debug.Log("Entered Room!!");

        GameObject.Destroy(this.gameObject);
        StartCoroutine("LoadRoomSceneCoroutine");
    }


    // 룸씬으로 전환하는 코루틴
    private IEnumerator LoadRoomSceneCoroutine()
    {
        PhotonNetwork.isMessageQueueRunning = false;

        AsyncOperation _ao = SceneManager.LoadSceneAsync("Game");

        yield return _ao;
    }


    // 포톤 클라우드 접속 오류(Photon Event 메소드)
    public void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("[오류] 포톤 클라우드 접속을 실패함 : " + cause.ToString());
    }


    // 포톤 방 생성 실패시 호출되는 포톤 이벤트 메소드
    private void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Create Room Failed = " + codeAndMsg[1]);
    }


    // 포톤 방 접속을 실패함 (Photon Event 메소드)
    public void OnPhotonJoinRoomFailed(object[] errorMsg)
    {
        Debug.Log("[오류] 방 접속을 실패함 => " + errorMsg[1].ToString());
    }
}


