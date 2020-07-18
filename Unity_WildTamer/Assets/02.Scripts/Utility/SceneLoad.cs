using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= 0.25f)
        {
            if (SceneManager.GetActiveScene().name == "CaveScene")
            {
                //이동하면서 위치 조정
                PlayerInfoManager.Instans.position = new Vector2(0, -0.5f);
                SceneManager.LoadScene("FieldScene");
            }
            else
            {
                //이동하면서 위치 조정
                PlayerInfoManager.Instans.position = new Vector2(0, 3.5f);
                SceneManager.LoadScene("CaveScene");
            }
        }
    }
}
