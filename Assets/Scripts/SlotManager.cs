using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] GameObject GreenCircle, GreenCross, RedCircle, RedCross;
    [SerializeField] GameObject Circle, Cross;
    public bool Occupied = false;
    public bool isCircle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowIcon()
    {
        if (Occupied)
        {
            if (GameManager.instance.CurPlayerSide == GameManager.PlayerSide.Circle)
            {
                RedCross.SetActive(false);
                RedCircle.SetActive(true);
            }
            else if (GameManager.instance.CurPlayerSide == GameManager.PlayerSide.Cross)
            {
                RedCross.SetActive(true);
                RedCircle.SetActive(false);
            }
        }
        else
        {
            if (GameManager.instance.CurPlayerSide == GameManager.PlayerSide.Circle)
            {
                GreenCross.SetActive(false);
                GreenCircle.SetActive(true);
            }
            else if (GameManager.instance.CurPlayerSide == GameManager.PlayerSide.Cross)
            {
                GreenCross.SetActive(true);
                GreenCircle.SetActive(false);
            }
        }    
    }

    public void HideIcon()
    {
        GreenCross.SetActive(false);
        GreenCircle.SetActive(false);
        RedCross.SetActive(false);
        RedCircle.SetActive(false);
    }

    public void ShowObject()
    {
        if (Occupied)
        {
            return;
        }

        if (GameManager.instance.CurPlayerSide == GameManager.PlayerSide.Circle)
        {
            isCircle = true;
            Cross.SetActive(false);
            Circle.SetActive(true);
        }
        else if (GameManager.instance.CurPlayerSide == GameManager.PlayerSide.Cross)
        {
            Cross.SetActive(true);
            Circle.SetActive(false);
        }
        Occupied = true;
    }

    public void ShowCircle()
    {
        if (Occupied)
        {
            return;
        }

        isCircle = true;
        Cross.SetActive(false);
        Circle.SetActive(true);
        Occupied = true;
    }

    public void ShowCross()
    {
        if (Occupied)
        {
            return;
        }

        isCircle = false;
        Cross.SetActive(true);
        Circle.SetActive(false);
        Occupied = true;
    }

    public void UndoMove()
    {
        Cross.SetActive(false);
        Circle.SetActive(false);
        Occupied = false;
        isCircle = false;
    }

    public void ResetSlot()
    {
         Cross.SetActive(false);
        Circle.SetActive(false);
        Occupied = false;
        isCircle = false;
    }
}
