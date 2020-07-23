using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TuochPlace : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private GameObject Background;

    // Start is called before the first frame update
    void Start()
    {
        Background = GameObject.Find("JoyBackground");
        Background.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Background.GetComponent<Joystick>().OnDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Background.SetActive(true);

        Background.transform.position = eventData.position;

        Background.GetComponent<Joystick>().OnPointerDown(eventData);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Background.GetComponent<Joystick>().OnPointerUp(eventData);
    }
}
