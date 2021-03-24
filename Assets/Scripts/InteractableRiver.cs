using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRiver : Interactable
{
    public GameManager gameManager;

    public override bool Interact()
    {
        gameManager.DirtyClick();
        return true;
    }
}
