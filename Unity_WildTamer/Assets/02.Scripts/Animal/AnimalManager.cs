﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    //싱글톤 만들기
    static public AnimalManager Instans = null;

    private void Awake()
    {
        if(Instans == null)
        {
            Instans = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    //처음에 만들 animal개수
    private int maxAnimal = 10;
    //동물을 담을 빈 오브젝트
    private Transform[] poolAnimal;

    //오브젝트풀을 위한 쥐 생성 오브젝트
    public GameObject mouseFactory;
    //쥐 담을 풀
    private Queue<GameObject> mousePool;
    
    // Start is called before the first frame update
    void Start()
    {
        //배열 할당
        poolAnimal = new Transform[5];
        //쥐를 담을 곳은 0번째 인덱스
        poolAnimal[0] = transform.Find("MousePool");
        //쥐 오브젝트풀링
        MousePooling();
    }

    private void MousePooling()
    {
        //동적 할당
        mousePool = new Queue<GameObject>();

        for (int i = 0; i < maxAnimal; i++)
        {
            GameObject mouse = Instantiate(mouseFactory);
            //부모 안에 넣어준다.
            mouse.transform.parent = poolAnimal[0];
            //비활성화
            mouse.SetActive(false);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}