using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellImage : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectWell;

    //얼마큼 떨어트릴 것인가
    [HideInInspector] public Vector2 offset = Vector2.zero;
    [HideInInspector] public Transform targetTr;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectWell = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}

    //다 움직이고 나서 그 뒤에 이미지가 따라가야 하므로
    private void LateUpdate()
    {
        Vector2 targetPos = targetTr.position;
        Vector2 screenPos = targetPos + offset;

        //Vector2 localPos = Vector2.zero;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos,
        //    uiCamera, out localPos);
        Vector2 canPos = canvas.transform.position;
        rectWell.position = canPos + screenPos;

        //Vector2 localPos = Vector2.zero;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos,
        //    uiCamera, out localPos);
        //
        //rectWell.localPosition = localPos;
    }
}
