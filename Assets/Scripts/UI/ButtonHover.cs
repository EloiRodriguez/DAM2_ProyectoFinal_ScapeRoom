using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;
    private Button button;
    private Navigator navigator;
    public string action = "Navigate";
    private Vector3 originalPosition;
    public GameObject pauseManager;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<Text>();
        button = GetComponent<Button>();
        navigator = GetComponent<Navigator>();
        originalPosition = text.transform.localPosition;
    }

    private void Start()
    {
       button.onClick.AddListener(OnClick);
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
        ResetPosition();

        Debug.Log("Sale");
    }

    private void OnClick()
    {
        if (action == "Navigate") navigator.Navigate();
        
        if (action == "Close") Application.Quit();
        
        if (action == "Pause")
        {
            PauseManager pm = pauseManager.GetComponent<PauseManager>();

            if (pm.Paused) pm.UnPause();
            else pm.Pause();
        }

        if (action == "Reload") navigator.ReloadScene();

        ResetPosition();
    }

    private void ResetPosition()
    {
        Color color = text.color;
        color.a = 0.5f;
        text.color = color;
        text.transform.localPosition = originalPosition;
    }
}
