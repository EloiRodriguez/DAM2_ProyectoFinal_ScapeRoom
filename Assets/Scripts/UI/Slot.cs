using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Texture placeholder;
    private Image background;
    private RawImage icon;
    private GameObject item;

    private void Awake()
    {
        background = transform.Find("bg").GetComponent<Image>();
        icon = transform.Find("icon").GetComponent<RawImage>();
    }

    public void SetIcon(Texture texture)
    {
        Color color = icon.color;
        
        color.a = 255;

        icon.texture = texture;
        icon.color = color;
    }

    public void ClearIcon()
    {
        Color color = icon.color;

        color.a = 0;

        icon.texture = null;
        icon.color = color;
    }

    public void Active(bool active)
    {
        Color color = background.color;
        float rgb;

        if (active) rgb = 150;
        else rgb = 0;

        color.r = rgb;
        color.g = rgb;
        color.b = rgb;

        background.color = color;
    }

    public void SaveItem(GameObject item)
    {
        this.item = item;
        Pickable picked = item.GetComponent<Pickable>();
        
        if (picked.icon != null) SetIcon(picked.icon);
        else SetIcon(placeholder);
    }

    public GameObject DropItem()
    {
        GameObject dropped = item;

        item = null;
        ClearIcon();

        return dropped;
    }

    public GameObject Item
    {
        get { return item; }
    }
}
