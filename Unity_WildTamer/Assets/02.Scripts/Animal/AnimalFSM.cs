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
    private SelectMoveTarget smt;

    public int hp = 100;
    public float speed = 1.0f;
    public float attackTime = 1.5f;
    private float curTime = 0.0f;

    //가야할 할 타겟
    private Transform targetPoint;
    public Transform TargetPoint
    {
        get { return targetPoint; }
        set { targetPoint = value; }
    }
    

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
        //계속 현재 시간을 0으로 만들어준다.
        curTime = 0.0f;
        anim.SetTrigger("Default");
        //거리가 target과 0.5 초과가 된다면 런으로 바꿔준다
        if (Vector2.Distance(transform.position, targetPoint.position) > 0.5f)
        {
            //일단 모든 코루틴을 꺼준다.
            //3개의 자식이 있는데 안거주고 모두 위치를 찾게 된다면 갈팡질팡함.
            StopAllCoroutines();
            //상태를 런으로 바꿔준다.
            anis = AnimalState.Run;
        }
    }

    private void Run()
    {
        //먼저 target에 다가왔는지 확인
        if(Vector2.Distance(transform.position, targetPoint.position) <= 0.5f)
        {
            //다가왔으면 새로운 타겟을 받는다.(코루틴으로 잠시 뒤에 받을것.
            StartCoroutine(NewTarget());
            //받는동안 디폴트 상태
            anis = AnimalState.Default;
            //아래 움직이는 함수 실행 안하고 바로 탈출
            return;
        }

        anim.SetTrigger("Run");
    }

    IEnumerator NewTarget()
    {
        //2초 후에
        yield return new WaitForSeconds(2.0f);
        //새로운 타겟 할당(모든 자식들)
        smt.ChangeTarget();
    }

    private void Attack()
    {

    }

    private void Down()
    {

    }

    public void FirstTagetCheck()
    {
        //부모인 애니멀그룹은 만들어질 때 같이 만들어지는 것이 아니라
        //오브젝트풀 할때 만들어지므로
        //Start가 아니라 여기서 컴포넌트 한다.

        //SelectMoveTaget 스크립트 컴포넌트
        smt = transform.parent.GetComponent<SelectMoveTarget>();
        smt.ChangeTarget();
    }
}
