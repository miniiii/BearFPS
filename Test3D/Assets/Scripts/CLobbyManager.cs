using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CLobbyManager : MonoBehaviour {

    GameObject _introMgr;

    private void Start()
    {
        _introMgr = GameObject.Find("IntroManager");
        PhotonCloudConnect();
    }


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

        GameObject.Destroy(_introMgr);
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
