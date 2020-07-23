using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamRotation : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.rotation.y == 180)
        {
            Vector3 dis = new Vector3(player.transform.position.x, player.transform.position.y, 10);
            transform.position = dis;
            Vector3 ro = new Vector3(0, -180, 0);
            transform.rotation = Quaternion.Euler(ro);
        }
        else
        {
            Vector3 dis = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            transform.position = dis;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
