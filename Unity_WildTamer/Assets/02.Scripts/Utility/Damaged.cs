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
    public void DoDamaged(int damage)
    {
        if(isPlayer)
        {
            PlayerInfoManager.Instans.hp -= damage;
            print(PlayerInfoManager.Instans.hp);
        }
        else
        {
            afsm.HP -= damage;
        }
    }

}
