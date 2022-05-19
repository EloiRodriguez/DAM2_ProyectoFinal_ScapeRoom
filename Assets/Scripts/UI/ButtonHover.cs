using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 position = text.transform.localPosition;
        position.x += 10f;
        Color color = text.color;

        color.a = 1;

        text.transform.localPosition = position;
        text.color = color;

        Debug.Log("Entra");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector3 position = text.transform.localPosition;
        position.x -= 10f;

        Color color = text.color;

        color.a = 0.5f;

        text.transform.localPosition = position;
        text.color = color;

        Debug.Log("Sale");
    }
}
