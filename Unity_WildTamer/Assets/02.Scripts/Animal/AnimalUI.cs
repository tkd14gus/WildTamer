using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour
{
    public GameObject WellPrefab;
    public GameObject ItemPrefab;
    public GameObject TamingPrefab;

    public Vector2 wellOffset = new Vector2(0, 1.5f);
    public Vector2 ItemOffset = new Vector2(1, -1.0f);
    public Vector2 TamingOffset = new Vector2(-1, -1.0f);

    private Canvas uiCanvas;
    private WellImage _well;
    private WellImage _Item;
    private WellImage _Taming;


    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        SetwWell();
        setButton();
    }

    void SetwWell()
    {
        GameObject well = Instantiate(WellPrefab, uiCanvas.transform);

        _well = well.GetComponent<WellImage>();
        _well.targetTr = transform;
        _well.offset = wellOffset;
    }

    private void setButton()
    {
        //생성해준다.
        GameObject Item = Instantiate(ItemPrefab, uiCanvas.transform);
        GameObject Taming = Instantiate(TamingPrefab, uiCanvas.transform);

        //스크립트 받아온 후 정보를 준다.
        _Item = Item.GetComponent<WellImage>();
        _Item.targetTr = transform;
        _Item.offset = ItemOffset;
        //이미지를 false로 바꿔 화면에 안 나오도록 해준다.
        _Item.transform.GetComponent<Image>().enabled = false;
        _Item.transform.GetComponent<Button>().interactable = false;

        _Taming = Taming.GetComponent<WellImage>();
        _Taming.targetTr = transform;
        _Taming.offset = TamingOffset;
        _Taming.transform.GetComponent<Image>().enabled = false;
        _Taming.transform.GetComponent<Button>().interactable = false;
    }

    public void DeadUI()
    {
        _well.gameObject.SetActive(false);

        _Item.transform.GetComponent<Image>().enabled = true;
        _Item.transform.GetComponent<Button>().interactable = true;

        _Taming.transform.GetComponent<Image>().enabled = true;
        _Taming.transform.GetComponent<Button>().interactable = true;
    }
}
