using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostButton : MonoBehaviour
{
    [SerializeField]
    private UpgradeManager manager;
    [SerializeField]
    private Text[] texts = new Text[2];


    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(()=>manager.BuyBoost(gameObject.name,texts[0],texts[1]));
    }
}
