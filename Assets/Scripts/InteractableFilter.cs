using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFilter : Interactable
{
    public GameManager gameManager;

    public override bool Interact()
    {
        gameManager.CleanClick();
        return true;
    }
}