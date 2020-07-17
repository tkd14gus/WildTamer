using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    //player상태
    enum PlayerState
    {
        Stand, Run, Attack, Dead
    }

    //처음 시잗은 Stand상태
    PlayerState ps = PlayerState.Stand;

    //이동 속도
    [SerializeField] float speed = 1.0f;
    //이동 이미지가 바뀌는 시간
    [SerializeField] float runChangeTime = 0.3f;
    //공격 속도
    [SerializeField] float attackTime = 1.0f;
    //공격 이미지가 바뀌는 시간
    [SerializeField] float attackChangeTime = 0.5f;
    //현재 공격 시간
    [SerializeField] float curTime = 0.0f;
    
    //각각 상태때 사용할 GameObject
    GameObject stand;
    GameObject[] run;
    GameObject[] attack;
    GameObject dead;

    // Start is called before the first frame update
    void Start()
    {
        //스텐드 상태 오브젝트 컴포넌트
        stand = transform.GetChild(0).gameObject;
        //런 상태 오브젝트 컴포넌트
        run = new GameObject[2];
        run[0] = transform.GetChild(1).gameObject;
        run[1] = transform.GetChild(2).gameObject;
        //어택 상태 오브젝트 컴포넌트
        attack = new GameObject[2];
        attack[0] = transform.GetChild(3).gameObject;
        attack[1] = transform.GetChild(4).gameObject;
        //스텐드 상태 오브젝트 컴포넌트
        dead = transform.GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        print(ps);
        //조금이라도 움직인다면 상태를 Run으로 바꿔준다.
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (ps == PlayerState.Stand)
            {
                ps = PlayerState.Run;
                //stand이미지를 비활성화
                stand.SetActive(false);
                //run의 첫번째 이미지 활성화
                run[0].SetActive(true);
            }
        }
        //만일 움직이지 않는다면(나중에 Attack관련해서 수정이 요구됨)
        else
        {
            ps = PlayerState.Stand;
            //stand이미지를 활성화
            stand.SetActive(true);
            //run의이미지 비활성화
            run[0].SetActive(false);
            run[1].SetActive(false);
        }

        //상태에 따른 함수 호출
        switch (ps)
        {
            case PlayerState.Stand:
                Stand();
                break;
            case PlayerState.Run:
                Run();
                break;
            case PlayerState.Attack:
                Attack();
                break;
            case PlayerState.Dead:
                Dead();
                break;
        }
    }

    private void Stand()
    {
        //스텐드 상태면 curTime을 무조건 0으로 바꿔준다.
        curTime = 0.0f;
    }

    private void Run()
    {
        //현재 위치에서 조이스틱이 움직인 만큼 이동해 준다.(캐스팅)
        Vector2 move = transform.position;

        move += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime;

        transform.position = move;

        //이동 이미지를 해주기 위한 시간 추가
        curTime += Time.deltaTime;
        //만일 이동 시간보다 커졌다면
        if(curTime >= runChangeTime)
        {
            //만일 첫번째 이미지가 사용중이라면
            if(run[0].activeSelf)
            {
                //첫번째 것 비활성화
                run[0].SetActive(false);
                //두번째 것 활성화
                run[1].SetActive(true);
            }
            //아니라면
            else
            {
                //첫번째 것 활성화
                run[0].SetActive(true);
                //두번째 것 비활성화
                run[1].SetActive(false);
            }

            //시간 초기화
            curTime = 0.0f;
        }

    }

    private void Attack()
    {
        throw new NotImplementedException();
    }

    private void Dead()
    {
        throw new NotImplementedException();
    }
}
