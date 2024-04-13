using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] GameObject Slots;
    public SlotManager[] slotManagers = new SlotManager[9];
    AI ai;
    // Start is called before the first frame update
    private void Awake()
    {
        slotManagers = Slots.GetComponentsInChildren<SlotManager>().ToArray();
    }
    void Start()
    {
        
        ai = FindAnyObjectByType<AI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisuals();
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        UpdateControl();
        CheckWin();
        CheckTie();
    }

    void UpdateVisuals()
    {
        foreach (var slot in slotManagers)
        {
            slot.HideIcon();
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            SlotManager slot = hit.collider.GetComponent<SlotManager>();
            if (slot)
            {
                slot.ShowIcon();
            }
        }
    }

    void UpdateControl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            SlotManager slot = hit.collider.GetComponent<SlotManager>();
            if (slot && Input.GetMouseButtonUp(0) && !slot.Occupied)
            {
                slot.ShowObject();
                GameManager.instance.SwitchSide();
                ai.MakeBestMove();
                GameManager.instance.SwitchSide();
            }
        }
    }

    void CheckWin()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        for(int i = 0; i <= 6; i+=3)
        {
            if (slotManagers[0 + i].Occupied != false && slotManagers[1 + i].Occupied != false && slotManagers[2 + i].Occupied != false
                && slotManagers[0 + i].isCircle == slotManagers[1 + i].isCircle
                && slotManagers[1 + i].isCircle == slotManagers[2 + i].isCircle)
            {
                GameManager.instance.Win();
            }
        }

        for(int i = 0; i < 3; i++)
        {
            if (slotManagers[0 + i].Occupied != false && slotManagers[3 + i].Occupied != false && slotManagers[6 + i].Occupied != false
                && slotManagers[0 + i].isCircle == slotManagers[3 + i].isCircle
                && slotManagers[3 + i].isCircle == slotManagers[6 + i].isCircle)
            {
                GameManager.instance.Win();
            }
        }

        if (slotManagers[0].Occupied != false && slotManagers[4].Occupied != false && slotManagers[8].Occupied != false
                && slotManagers[0].isCircle == slotManagers[4].isCircle
                && slotManagers[4].isCircle == slotManagers[8].isCircle)
        {
            GameManager.instance.Win();
        }

        if (slotManagers[2].Occupied != false && slotManagers[4].Occupied != false && slotManagers[6].Occupied != false
                && slotManagers[2].isCircle == slotManagers[4].isCircle
                && slotManagers[4].isCircle == slotManagers[6].isCircle)
        {
            GameManager.instance.Win();
        }
    }

    void CheckTie()
    {
        foreach(var slot in slotManagers)
        {
            if(slot.Occupied == false)
            {
                return;
            }
        }

        GameManager.instance.Tie();
    }
}
