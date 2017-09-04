using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour {

    private PhotonView pv = null;

    public Sprite _chat_heart;
    public Sprite _chat_fun;
    public Sprite _chat_joke;
    public Sprite _chat_melong;

    public GameObject _imotionMenu;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        CreatePhotonGameObject("Prefabs/Bear");    // 플레이어 생성

        PhotonNetwork.isMessageQueueRunning = true;


    }


    private void Start()
    {
        if(PhotonNetwork.player.ID == PhotonNetwork.room.MasterClientId)    // 방장이면
        {
            for (int i = 30; i > 0; i--)
            {
                CreatePhotonGameObject("Prefabs/AI");
            }

            //CreatePhotonGameObject("Prefabs/AI");
            //CreatePhotonGameObject("Prefabs/AI");
        }
    }


    // 포톤 객체 생성 메소드
    private void CreatePhotonGameObject(string _name)
    {
        float _createPosX = Random.Range(-12.0f, 12.0f);
        float _createPosZ = Random.Range(-12.0f, 12.0f);

        // 포톤 네트워크용 게임 오브젝트를 생성함
        // 이 게임 오브젝트는 포톤용 게임오브젝트이므로 반드시 PhotonView 컴포넌트를 가져야 함
        PhotonNetwork.Instantiate(_name,
            new Vector3(_createPosX, 0.5f, _createPosZ), Quaternion.identity, 0);
    }

    public void OnImotionMenuBtnClicked()
    {
        _imotionMenu.SetActive(true);
    }


}
