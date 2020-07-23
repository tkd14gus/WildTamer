using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //오른쪽을 눌렀다면 오른쪽을 보게 한다.
        if (GetComponent<PlayerFSM>().X > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        //왼쪽을 눌렀을 때
        else if (GetComponent<PlayerFSM>().X < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
