using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaged : MonoBehaviour
{
    private AnimalFSM afsm;

    private bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 안에 들어있는지 아닌지 확인해준다.
        if(transform.name.Contains("Player"))
        {
            isPlayer = true;
        }
        else
        {
            afsm = GetComponent<AnimalFSM>();
        }
    }

    //데미지를 주는 함수
    public void DoDamaged(int damage, Transform ta)
    {
        if(isPlayer)
        {
            PlayerInfoManager.Instans.hp -= damage;
            //print(PlayerInfoManager.Instans.hp);
        }
        else
        {
            //피를 깎아주고
            afsm.HP -= damage;
            print(afsm.HP);
            //대상을 옮겨준다.
            gameObject.GetComponent<AnimalFSM>().SetTarget(ta);
        }
    }

}
