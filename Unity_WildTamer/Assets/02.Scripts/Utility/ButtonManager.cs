using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    
    public void OnClickTaming()
    {
        //체력 원상복구 시켜주고
        transform.GetComponent<AnimalFSM>().HP = 100;
        //레이어를 플레이어로 바꿔준 다음
        gameObject.layer = 1 << 9;
        //플레이어 와일드 리스트에 넣어준다.
        PlayerInfoManager.Instans.wild.Add(transform.GetComponent<WellImage>().targetTr.gameObject);
    }

    public void OnClickItme()
    {
        //아무일 없음
    }
}
