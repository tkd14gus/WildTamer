﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //스폰 포인트를 담아둔다.
    private Transform[] spawnPoint;

    //3마리씩 움직이려면 Group에 담아둬야 한다.
    //담아둘 그룹 프리팹
    public GameObject groupFactory;
    
    // Start is called before the first frame update
    void Start()
    {
        //스폰 포인트 들을 모두 컴포넌트 해준다.
        ComponentPoint();

        //시작하면 단체 스폰 해준다.
        MagaSpawn();
    }

    private void ComponentPoint()
    {
        //동적할당
        spawnPoint = new Transform[transform.childCount];

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            spawnPoint[i] = transform.GetChild(i);
        }
    }

    private void MagaSpawn()
    {
        //총 몇개의 동물이 생성되었는지
        int groupNum = AnimalManager.Instans.maxAnimal;

        //3마리가 하나의 짝이므로 그룹은 3으로 나눈 값이다.
        for (int i = 0; i < groupNum / 3; i++)
        {
            GameObject groupOb = Instantiate(groupFactory);
            groupOb.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;

            for (int j = 0; j < 3; j++)
            {
                //AnimalManager에게서 mouse를 받는다.
                GameObject mouse = AnimalManager.Instans.MousePool;

                int xOffset = 0;
                if (j == 1)
                    xOffset = 1;
                else if (j == 2)
                    xOffset = -1;

                //중앙에 0번 양 옆에 1, 2애니멀이 세워지도록 배치
                Vector2 pos = new Vector2(groupOb.transform.position.x + xOffset, groupOb.transform.position.y);
                mouse.transform.position = pos;
                //그룹으로 묶어준다.
                mouse.transform.parent = groupOb.transform;

                //0번째 애니멀은 잡을 수 있다.
                if(j == 0)
                {
                    mouse.GetComponent<AnimalUI>().enabled = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}