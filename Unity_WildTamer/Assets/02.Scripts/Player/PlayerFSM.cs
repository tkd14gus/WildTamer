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
    //[SerializeField] float speed = 2.5f;
    //공격 속도
    [SerializeField] float attackTime = 1.0f;
    //현재 공격 시간
    [SerializeField] float curTime = 0.0f;
    //공격력
    [SerializeField] int power = 30;

    private Animator anim;
    private Animator armAnim;

    //장비 게임 오브젝트
    private GameObject[] Item;

    //카메라가 움직여야 하는지, 플레이어가 움직여야 하는지
    private bool isCam;

    private Transform[] moveCam;

    //갈 수 있는지 확인하기 위해
    private GameObject Ground;

    //적의 위치
    private Transform animalPoint;

    private float x;
    private float y;
    public float X
    {
        get { return x; }
        set { x = value; }
    }
    public float Y
    {
        get { return y; }
        set { y = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //애니메이터 컴포넌트
        anim = transform.GetChild(0).GetComponent<Animator>();
        armAnim = transform.GetChild(1).GetComponent<Animator>();

        //그라운드 컴포넌트
        Ground = GameObject.Find("Ground");

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

        isCam = PlayerInfoManager.Instans.isCam;
        //카메라가 움직여야 한다면 카메라를 받아와준다.
        if(isCam)
        {
            componentCam();
        }
    }

    private void componentCam()
    {
        moveCam = new Transform[2];

        moveCam[0] = Camera.main.transform;
        moveCam[1] = GameObject.Find("UICamera").GetComponent<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        //조금이라도 움직인다면 상태를 Run으로 바꿔준다.
        //if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        if(x != 0 || y != 0)
        {
            if (ps != PlayerState.Dead)
            {
                ps = PlayerState.Run;
            }
            anim.SetTrigger("Run");
            armAnim.SetTrigger("Run");
        }
        //만일 움직이지 않는다면(나중에 Attack관련해서 수정이 요구됨)
        else
        {
            if (ps != PlayerState.Attack)
            {
                ps = PlayerState.Stand;
                anim.SetTrigger("Stand");
                armAnim.SetTrigger("Stand");
            }
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
        //Stand상태에서만 적을 찾는다
        FindEnemy();
    }

    private void FindEnemy()
    {
        //1 << 8이 에너미
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, 3.0f, 1 << 8);

        if(enemys.Length != 0)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                if (enemys[i].transform.GetComponent<AnimalFSM>().HP <= 0) continue;
                
                animalPoint = enemys[i].transform;
                ps = PlayerState.Attack;
                return;
            }
            //리턴을 겪지않고 나왔다는 것은 적이 모두 죽었다는 의미
            ps = PlayerState.Stand;
        }
        else
        {
            ps = PlayerState.Stand;
        }
    }

    private void Run()
    {
        //상하좌우
        float H = x;
        float V = y;

        //물을 만나면 그 방향 0으로
        if (!HorizontalCheck())
            H = 0;
        if (!VerticalCheck())
            V = 0;

        //현재 위치에서 조이스틱이 움직인 만큼 이동해 준다.(캐스팅)
        Vector2 move = new Vector2(H, V) * Time.deltaTime;

        Vector2 casting = transform.position;

        casting += move;

        transform.position = casting;

        //player그룹의 전체에게 이동하라고 보내준다.
        //0은 자기 자신
        for (int i = 1; i < transform.parent.childCount; i++)
        {
            float x = Random.Range(0, 2.0f);
            float y = Random.Range(0, 2.0f);
            float angle = Random.Range(0.0f, Mathf.PI * 2);
            
            casting = transform.position;

            //플레이어를 중심으로 2의 반지름 안에 임의의 자리에 가도록 해준다.
            Vector2 sendPos = new Vector2(Mathf.Cos(angle) * x, Mathf.Sin(angle) * y);

            casting += sendPos;

            transform.parent.GetChild(i).GetComponent<AnimalFSM>().TargetPoint = casting;
        }

        //카메라가 움직여야 한다면
        if (isCam)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 camCasting = moveCam[i].position;
                camCasting.x += move.x;
                camCasting.y += move.y;
                moveCam[i].position = camCasting;
            }
        }

    }

    private bool VerticalCheck()
    {
        //플레이어 발
        Vector2 bottom = transform.position;
        bottom.y -= 1;
        
        
        //왼쪽으로 갈 때
        if (y < 0)
        {
            //아래쪽으로 0.5떨어진 곳
            Vector2 temp = bottom;
            temp.y -= 0.5f;
        
        
            for (int i = 0; i < Physics.OverlapSphere(temp, 0.1f).Length; i++)
            {
                if (Physics.OverlapSphere(temp, 0.1f)[i].name.Contains("Ground"))
                    break;
        
                if (i == Physics.OverlapSphere(temp, 0.1f).Length - 1)
                    return false;
            }
        
        }
        //오른쪽으로 갈 때
        else if (y > 0)
        {
            //위쪽으로 0.5떨어진 곳
            Vector2 temp = bottom;
            temp.y += 0.5f;
        
        
            for (int i = 0; i < Physics.OverlapSphere(temp, 0.1f).Length; i++)
            {
                if (Physics.OverlapSphere(temp, 0.1f)[i].name.Contains("Ground"))
                    break;
        
                if (i == Physics.OverlapSphere(temp, 0.1f).Length - 1)
                    return false;
            }
        
        }
        
        return true;

    }

    private bool HorizontalCheck()
    {
        //플레이어 발
        Vector2 bottom = transform.position;
        bottom.y -= 1;
        

        //왼쪽으로 갈 때
        if (x < 0)
        {
            //왼쪽으로 0.5떨어진 곳
            Vector2 temp = bottom;
            temp.x -= 0.5f;

            
            for (int i = 0; i < Physics.OverlapSphere(temp, 0.1f).Length; i++)
            {
                if (Physics.OverlapSphere(temp, 0.1f)[i].name.Contains("Ground"))
                    break;

                if (i == Physics.OverlapSphere(temp, 0.1f).Length - 1)
                    return false;
            }
                
        }
        //오른쪽으로 갈 때
        else if(x > 0)
        {
            //오른쪽으로 0.5떨어진 곳
            Vector2 temp = bottom;
            temp.x += 0.5f;


            for (int i = 0; i < Physics.OverlapSphere(temp, 0.1f).Length; i++)
            {
                if (Physics.OverlapSphere(temp, 0.1f)[i].name.Contains("Ground"))
                    break;

                if (i == Physics.OverlapSphere(temp, 0.1f).Length - 1)
                    return false;
            }

        }

        return true;
    }

    private void Attack()
    {
        //체력이 0보다 작으면
        if(animalPoint.GetComponent<AnimalFSM>().HP <= 0)
        {
            //대상을 다시 찾고
            FindEnemy();
            //나간다.
            return;
        }

        //애니메이션을 실행
        //anim.SetTrigger("Attack");

        //거리가 맞으면
        if(Vector2.Distance(animalPoint.position, transform.position) <= 2.5f)
        {
            //시간을 추가해주고
            curTime += Time.deltaTime;
            //공격한다.
            if(curTime >= attackTime)
            {
                animalPoint.GetComponent<Damaged>().DoDamaged(power, transform);
                curTime = 0.0f;
            }
        }
        //거리가 멀면
        else
        {
            curTime = 0.0f;
            //일반상태로 바꿔준다.
            ps = PlayerState.Stand;
        }
    }

    private void Dead()
    {

    }

    
}
