using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField]
    private UpgradeManager manager;
    [SerializeField]
    private Button nextButton;


    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(()=>manager.BuyUpgrade(gameObject.name,button,nextButton));
    }
}
