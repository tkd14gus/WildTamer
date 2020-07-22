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
                for (int i = 1; i < player.transform.parent.childCount; i++)
                {
                    //오브젝트풀에 넣어준다.
                    player.transform.parent.GetChild(i).GetComponent<AnimalFSM>().OBPool();
                }

                //씬이 바뀔 땐 3이상이 되어도 스폰을 못하게 막아준다.
                GameObject sm = GameObject.Find("SpawnManager");
                if (sm != null)
                    sm.GetComponent<SpawnManager>().enabled = false;

                //모든 동물 정보가 all에 들어가 있다.
                //그러니 그냥 all을 이용해서 전부 풀에 넣어준다.
                for (int i = 0; i < AnimalManager.Instans.all.Count; i++)
                {
                    if (!AnimalManager.Instans.all[i].activeSelf) continue;

                    AnimalManager.Instans.all[i].GetComponent<AnimalFSM>().OBPool();
                }

                //이동하면서 위치 조정
                PlayerInfoManager.Instans.position = new Vector2(0, -0.5f);
                //캠이 움직이도록
                PlayerInfoManager.Instans.isCam = true;
                SceneManager.LoadScene("FieldScene");
            }
            else
            {
                for (int i = 1; i < player.transform.parent.childCount; i++)
                {
                    //오브젝트풀에 넣어준다.
                    player.transform.parent.GetChild(i).GetComponent<AnimalFSM>().OBPool(true);
                }

                //씬이 바뀔 땐 3이상이 되어도 스폰을 못하게 막아준다.
                GameObject sm = GameObject.Find("SpawnManager");
                if (sm != null)
                    sm.GetComponent<SpawnManager>().enabled = false;

                //모든 동물 정보가 all에 들어가 있다.
                //그러니 그냥 all을 이용해서 전부 풀에 넣어준다.
                for (int i = 0; i < AnimalManager.Instans.all.Count; i++)
                {
                    if (!AnimalManager.Instans.all[i].activeSelf) continue;

                    AnimalManager.Instans.all[i].GetComponent<AnimalFSM>().OBPool(true);
                }

                //이동하면서 위치 조정
                PlayerInfoManager.Instans.position = new Vector2(0, 3.5f);
                //플레이어가 움직이도록
                PlayerInfoManager.Instans.isCam = false;
                SceneManager.LoadScene("CaveScene");
            }
        }
    }
}
