using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<Slot> slots;
    private int selected = 0;
    private Text item_name;

    private void Awake()
    {
        slots = new List<Slot>();
        item_name = transform.Find("item_name").GetChild(1).GetComponent<Text>();

        for (int i = 0; i < 4; i++)
        {
            slots.Add(transform.Find("slot" + (i + 1)).GetComponent<Slot>());
        }

        slots[selected].Active(true);

        SetItemName();
    }

    public int Selected
    {
        get { return selected; }
        
        set 
        { 
            int index = value;

            if (index < 0) index = 0;
            else if (index > 3) index = 3;

            selected = index;

            foreach (Slot slot in slots)
            {
                slot.Active(false);
            }

            slots[selected].Active(true);
            SetItemName();
        }
    }

    public Slot SelectedSlot
    {
        get { return slots[selected]; }
    }

    public bool SelectedEmpty()
    {
        if (slots[selected].Item == null) return true;
        else return false;
    }

    public void SetItemName()
    {
        if (!SelectedEmpty())
        {
            Pickable pickable = slots[selected].Item.GetComponent<Pickable>();

            if (pickable.name != null) item_name.text = pickable.name;
            else item_name.text = "Sin nombre";
            
        } else item_name.text = "Espacio vacío";
    }

    public GameObject GetSelectedItem()
    {
        return slots[selected].Item;
    }
}