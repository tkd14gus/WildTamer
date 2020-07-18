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
    //공격 속도
    [SerializeField] float attackTime = 1.0f;
    //현재 공격 시간
    [SerializeField] float curTime = 0.0f;

    private Animator anim;
    private Animator armAnim;

    //장비 게임 오브젝트
    private GameObject[] Item;

    // Start is called before the first frame update
    void Start()
    {
        //애니메이터 컴포넌트
        anim = transform.GetChild(0).GetComponent<Animator>();
        armAnim = transform.GetChild(1).GetComponent<Animator>();

        //PlayerInfoManager로부터 위치를 받아준다.
        transform.position = PlayerInfoManager.Instans.position;

        //아이템 게임
        Item = new GameObject[3];
        Item[0] = transform.GetChild(1).gameObject;
        Item[1] = transform.GetChild(2).gameObject;
        Item[2] = transform.GetChild(3).gameObject;

        for (int i = 0; i < 3; i++)
        {
            if(PlayerInfoManager.Instans.itemIndex[i] != -1)
            {
                Item[i].transform.GetChild(PlayerInfoManager.Instans.itemIndex[i]).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //조금이라도 움직인다면 상태를 Run으로 바꿔준다.
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (ps == PlayerState.Stand)
            {
                ps = PlayerState.Run;
            }
            anim.SetTrigger("Run");
            armAnim.SetTrigger("Run");
        }
        //만일 움직이지 않는다면(나중에 Attack관련해서 수정이 요구됨)
        else
        {
            ps = PlayerState.Stand;
            anim.SetTrigger("Stand");
            armAnim.SetTrigger("Stand");
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
