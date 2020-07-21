using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMoveTarget : MonoBehaviour
{
    //스폰매니저를 담을 변수
    private SpawnManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    
    public void ChangeTarget()
    {
        //Start보다 이 함수가 먼저 실행될 수 있음
        if(sm == null)
            sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        Transform target;
        while (true)
        {
            //sm에서 랜덤으로 타겟을 받는다.(프로퍼티로 설정)
            target = sm.SpawnPoint;
            //현재 위치와 거리가 0.5초과로 차이난다면 사용할것, 아니면 다시 돌도록 해준다.
            if (Vector2.Distance(transform.position, target.position) > 0.5f)
                break;
        }

        //타겟을 정했다면 모든 자식들에게 타겟을 전해준다.
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<AnimalFSM>().TargetPoint = target;
        }
    }

    public void SendPlayerTarget(Transform Player)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            print(i);
            transform.GetChild(i).GetComponent<AnimalFSM>().PlayerPoint = Player;
        }
    }
}
