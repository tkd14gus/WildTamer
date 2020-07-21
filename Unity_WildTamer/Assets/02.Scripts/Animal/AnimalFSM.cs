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
    //private Rigidbody2D rig;

    [SerializeField] private int hp = 100;
    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public float speed = 1.5f;
    public float attackTime = 1.5f;
    private float curTime = 0.0f;
    public int attackPower = 10;

    //충돌처리를 위한 변수들
    public float colRadius = 0.25f;
    public float pushPower = 0.2f;

    //가야할 할 타겟
    private Transform targetPoint;
    public Transform TargetPoint
    {
        get { return targetPoint; }
        set { targetPoint = value; }
    }

    //싸워야할 할 타겟
    private Transform playerPoint;
    public Transform PlayerPoint
    {
        get { return playerPoint; }
        set
        {
            playerPoint = value;
            print(playerPoint.name);
            anis = AnimalState.Attack;
        }
    }

    private float targetAngle;

    // Start is called before the first frame update
    void Start()
    {
        //애니메이터 컴포넌트
        anim = transform.GetChild(0).GetComponent<Animator>();
        //rig = GetComponent<Rigidbody2D>();
        //시작할 땐 널
        playerPoint = null;
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

        //매번 근처에 플레이어 있는지 체크
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        //주변에 플레이어가 있는지 확인
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, colRadius * 3, 1 << 9);
        
        if (player.Length != 0)
        {
            //있다면 그룹에게 보내 모든 자식들이 공격할 수 있도록 해준다.
            smt.SendPlayerTarget(player[0].transform);
        }
        else
        {
            //만일 없을 때 플에이어도 없다면 나가준다.
            if (playerPoint == null)
                return;
            //플레이어가 있다면 널로 바꿔준다.
            smt.SendPlayerTarget(null);
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
            //그리고 속력을 0으로
            //rig.velocity = Vector2.zero;
            //아래 움직이는 함수 실행 안하고 바로 탈출
            return;
        }

        anim.SetTrigger("Run");

        //충돌처리를 하면 바뀔 수 있으니 계속 각도를 조정해준다.
        targetAngle = Mathf.Atan2(targetPoint.position.y - transform.position.y, targetPoint.position.x - transform.position.x);

        // position으로 움직이기
        //float x = Mathf.Cos(targetAngle) * (speed * Time.deltaTime);
        //float y = Mathf.Sin(targetAngle) * (speed * Time.deltaTime);
        Vector2 dis = new Vector2(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle)) * (speed * Time.deltaTime);
        Vector2 pos = transform.position;

        pos += dis;

        transform.position = pos;
        
        //Enemy와 부딪혔는지 확인
        CheckCrash(dis);

        //리지드 바디로 움직이기
        //Vector2 dis = new Vector2(Mathf.Cos(targetAngle) * 5, Mathf.Sin(targetAngle) * 5).normalized;
        //print(dis);
        //
        ////점점 빨라지지 않게 속력을 계속 0으로
        //rig.velocity = Vector2.zero;
        //
        //rig.AddForce(dis * speed);


    }

    private void CheckCrash(Vector2 vt)
    {
        //1<<8은 Enemy
        //범위 안에 Enemy가 있다면
        if(Physics2D.OverlapCircleAll(transform.position, colRadius, 1 << 8).Length > 1)
        {
            //0은 자기 자신
            Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.position, colRadius, 1 << 8);
            //가장 가까운 녀석의 인덱스
            int index = 1;
            //거리
            float dis = Vector2.Distance(transform.position, enemy[1].transform.position);

            for (int i = 2; i < enemy.Length; i++)
            {
                //컬리더를 모두 계산해보고
                float temp = Vector2.Distance(transform.position, enemy[i].transform.position);
                //가장 가까이 있는 녀석을 고른다.
                if (dis > temp)
                    index = i;
            }
            //방향을 잡아준다.
            Vector2 vtDis = enemy[index].transform.position - transform.position;
            
            //만일 그 방향이 완전히 반대라면 추가적으로 움직일 수 있게 수정해준다.
            //실제 계산할 것은 vt라서 vt에 더해준다.
            if(vtDis.x == 0)
            {
                vt.x += 5.0f;
            }
            if(vtDis.y == 0)
            {
                vt.y += 5.0f;
            }

            //방향이니까 노멀라이즈 해준다.
            vt = (vt * 5).normalized;
            vt *= pushPower;

            //밀어준다.
            enemy[index].GetComponent<AnimalFSM>().Push(vt);
        }

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
        if(Vector2.Distance(transform.position, playerPoint.position) > colRadius * 2)
        {
            anim.SetTrigger("Run");

            //플레이어와 계속 각도 조절
            targetAngle = Mathf.Atan2(playerPoint.position.y - transform.position.y, playerPoint.position.x - transform.position.x);

            //방향을 잡아주고 이동한다.
            Vector2 dis = new Vector2(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle)) * (speed * Time.deltaTime);
            Vector2 pos = transform.position;

            pos += dis;

            transform.position = pos;

            //Enemy와 부딪혔는지 확인
            CheckCrash(dis);
        }
        else
        {
            anim.SetTrigger("Attack");

            curTime += Time.deltaTime;

            if(curTime >= attackTime)
            {
                //공격해준다.
                targetPoint.GetComponent<Damaged>().DoDamaged(attackPower);
            }
        }
    }

    private void Down()
    {

    }

    public void FirstTagetCheck()
    {
        //부모인 애니멀그룹은 만들어질 때 같이 만들어지는 것이 아니라
        //오브젝트풀 할때 만들어지므로
        //Start가 아니라 여기서 컴포넌트 한다.
        
        smt.ChangeTarget();
    }

    public void Setsmt()
    {
        //SelectMoveTaget 스크립트 컴포넌트
        smt = transform.parent.GetComponent<SelectMoveTarget>();
    }

    public void Push(Vector2 dis)
    {

        Vector2 vt = transform.position;
        vt += dis;

        transform.position = vt;
    }
}
