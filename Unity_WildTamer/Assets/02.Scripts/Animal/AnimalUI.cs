using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalUI : MonoBehaviour
{
    public GameObject WellPrefab;
    public Vector2 wellOffset = new Vector2(0, 1.5f);

    private Canvas uiCanvas;

    // Start is called before the first frame update
    void Start()
    {
        SetwWell();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}

    void SetwWell()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        GameObject well = Instantiate(WellPrefab, uiCanvas.transform);

        WellImage _well = well.GetComponent<WellImage>();
        _well.targetTr = transform;
        _well.offset = wellOffset;
    }
}
