using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    //싱글톤
    static public PlayerInfoManager Instans = null;

    //플레이어가 씬이 이동해도 항상 유지되어야 하는 것들을 저장
    //체력
    public int hp = 500;
    //동물의 숫자
    public List<GameObject> wild;
    //착용중인 장비들의 인덱스
    public int[] itemIndex;
    //플레이어의 위치
    public Vector2 position = Vector2.zero;

    //현재 Scene이 캐릭터가 움직여야 하는지 아니면 카메라가 움직여야 하는지
    public bool isCam = false;

    private void Awake()
    {
        if (Instans == null)
        {
            //필요한 것들 할당
            wild = new List<GameObject>();
            itemIndex = new int[3];
            //0은 arm, 1은 acc, 2는 ar
            itemIndex[0] = -1;
            itemIndex[1] = 0;
            itemIndex[2] = -1;

            Instans = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
