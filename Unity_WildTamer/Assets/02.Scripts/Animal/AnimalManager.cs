using System;
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //처음에 만들 animal개수
    public int maxAnimal = 10;
    //동물을 담을 빈 오브젝트
    private Transform[] poolAnimal;

    //모든 동물들을 한번에 불러담기 위한 리스트
    public List<GameObject> all;

    //오브젝트풀을 위한 쥐 생성 오브젝트
    public GameObject mouseFactory;
    //쥐 담을 풀
    private Queue<GameObject> mousePool;
    public GameObject MousePool
    {
        get
        {
            //mousePool을 내보내기
            if(mousePool.Count != 0)
            {
                GameObject mouse = mousePool.Dequeue();
                return mouse;
            }
            else
            {
                GameObject mouse = Instantiate(mouseFactory);

                //리스트에 넣어준다.
                all.Add(mouse);

                return mouse;
            }
        }
        //다시 돌려받기
        set
        {
            //받고
            GameObject mouse = value;
            //부모 안에 넣어준다.
            mouse.transform.parent = poolAnimal[0];
            //끈 다음
            mouse.SetActive(false);
            //넣어준다.
            mousePool.Enqueue(mouse);
        }
    }
    public int MouseCount
    {
        get { return mousePool.Count; }
    }


    // Start is called before the first frame update
    void Start()
    {
        //리스트 할당
        all = new List<GameObject>();
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

            mousePool.Enqueue(mouse);
            //리스트에도 넣어준다.
            all.Add(mouse);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
