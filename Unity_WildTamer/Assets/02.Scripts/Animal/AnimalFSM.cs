using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFSM : MonoBehaviour
{
    //Animal상태
    enum AnimalState
    {
        Default, Run, Attack, Down
    }

    AnimalState anis = AnimalState.Default;

    private Animator anim;

    public int hp = 100;
    public float speed = 1.0f;
    public float attackTime = 1.5f;
    private float curTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //애니메이터 컴포넌트
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (anis)
        {
            case AnimalState.Default:
                Default();
                break;
            case AnimalState.Run:
                Run();
                break;
            case AnimalState.Attack:
                Attack();
                break;
            case AnimalState.Down:
                Down();
                break;
        }
    }

    private void Default()
    {
        curTime = 0.0f;
    }

    private void Run()
    {

    }

    private void Attack()
    {

    }

    private void Down()
    {

    }
}
