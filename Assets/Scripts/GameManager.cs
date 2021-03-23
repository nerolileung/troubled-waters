using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region water data
    public struct Water {
        public float storage;
        public float clickMod;
        public float autoMod;
        public Water(float store, float click, float auto){
            storage = store;
            clickMod = click;
            autoMod = auto;
        }
    };

    public Water dirty;
    public Water clean;
    #endregion

    #region ui elements
    [SerializeField]
    private Text dirtyText;
    [SerializeField]
    private Text cleanText;
    [SerializeField]
    private Image cleanFill;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        dirty = new Water(0f, 1f, 0f);
        clean = new Water(0f, 0.2f, 0f);

        // todo load save data
    }

    // Update is called once per frame
    void Update()
    {
        // increment water by auto mod

        // water usage starts with clean water

        UpdateUI();
    }

    public float CleanFloat(float f) {
        int temp = (int)Mathf.Round(f * 10f);
        return (float)(temp / 10f);
    }

    public void DirtyClick(){
        dirty.storage = CleanFloat(dirty.storage+dirty.clickMod);
    }

    public void CleanClick(){
        if (dirty.storage < clean.clickMod) return;
        else {
            if (Mathf.Approximately(dirty.storage,clean.clickMod))
                dirty.storage = 0f;
            else dirty.storage = CleanFloat(dirty.storage-clean.clickMod);
            clean.storage = CleanFloat(clean.storage+clean.clickMod);
        }
    }

    private void UpdateUI(){
        // water
        if (dirty.storage % 1 == 0)
            dirtyText.text = dirty.storage.ToString("F0");
        else dirtyText.text = dirty.storage.ToString("F1");

        if (clean.storage % 1 == 0)
            cleanText.text = clean.storage.ToString("F0");
        else cleanText.text = clean.storage.ToString("F1");

        float remainder = clean.storage % 1;
        cleanFill.fillAmount = remainder;
    }
}
