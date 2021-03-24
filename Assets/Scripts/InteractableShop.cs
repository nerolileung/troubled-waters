using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableShop : Interactable
{
    [SerializeField]
    private GameObject shopPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public bool Interact(){
        // open shop
        shopPanel.gameObject.SetActive(true);
        return true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // close shop
        shopPanel.gameObject.SetActive(false);
    }
}
