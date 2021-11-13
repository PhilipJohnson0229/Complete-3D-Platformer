using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewGameButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IDeselectHandler
{
    public Animator mainMenuSHowpiece;
    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Do something.
        Debug.Log("<color=red>Event:</color> Completed mouse highlight.");
    }
    // When selected.
    public void OnSelect(BaseEventData eventData)
    {
        // Do something.
        if (mainMenuSHowpiece != null)
        {
            mainMenuSHowpiece.SetBool("NewGame", true);
        }
        else
        {
            return;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // Do something.
        if (mainMenuSHowpiece != null)
        {
            mainMenuSHowpiece.SetBool("NewGame", false);
        }
        else
        {
            return;
        }
    }
}
