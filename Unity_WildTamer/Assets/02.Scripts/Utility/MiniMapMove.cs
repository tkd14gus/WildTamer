using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapMove : MonoBehaviour
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
        Vector2 pos = player.transform.position;
        transform.position = pos;
    }
}
