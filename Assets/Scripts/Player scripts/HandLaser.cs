using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class HandLaser : SteamVR_LaserPointer //Наследуемся от того скрипта от SteamVR, который с лазерами
{
    public override void OnPointerIn(PointerEventArgs e) //Когда наводишь луч
    {
        base.OnPointerIn(e); 
        if (e.target.CompareTag("ButtonUI"))
        {
            e.target.GetComponent<Image>().color = Color.blue;
            thickness = 0.005f;
        }
        if (e.target?.gameObject.GetComponent<Throwable>() != null)
        {
            thickness = 0.005f;
        }
    }

    public override void OnPointerClick(PointerEventArgs e) //Когда кликаешь с наведеным лучом
    {
        base.OnPointerIn(e);
        if (e.target.CompareTag("ButtonUI")) //Потом перепиши под switch
        {
            e.target.GetComponent<Button>().onClick.Invoke();
        }
    }

    public override void OnPointerOut(PointerEventArgs e) //Когда сводишь луч
    {

        base.OnPointerIn(e);
        if (e.target.CompareTag("ButtonUI"))
        {
            e.target.GetComponent<Image>().color = Color.white;
            thickness = 0;
        }
        if (e.target?.gameObject.GetComponent<Throwable>() == null)
        {
            thickness = 0;
        }
    }
    
}
