using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int count = PlayerInfoManager.Instans.wild.Count;
        if (count != 0)
        {
            for (int i = 0; i < count; i++)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        GameObject mouse = AnimalManager.Instans.MousePool;
        float x = Random.Range(0, 3.0f);
        float y = Random.Range(0, 3.0f);
        float angle = Random.Range(0, Mathf.PI * 2);

        //활성화
        mouse.SetActive(true);

        //위치 배정을 위해
        Vector2 pos = new Vector2(Mathf.Cos(angle) * x, Mathf.Sin(angle) * y);

        mouse.transform.parent = transform.parent;

        mouse.GetComponent<AnimalFSM>().IsTaming = true;
        mouse.layer = LayerMask.NameToLayer("Player");
        mouse.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
