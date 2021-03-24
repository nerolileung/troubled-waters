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
        public Water(float click){
            storage = 0f;
            clickMod = click;
            autoMod = 0f;
        }
    };

    public Water dirty;
    public Water clean;
    private float waterUse;
    #endregion

    #region ui elements
    [SerializeField]
    private Text dirtyText;
    [SerializeField]
    private Text cleanText;
    #endregion

    private float timer;

    public bool debugMode;

    // Start is called before the first frame update
    void Start()
    {
        dirty = new Water(1f);
        clean = new Water(0.2f);
        waterUse = 0f;
        timer = 0f;

        debugMode = false;

        // todo load save data
    }

    // Update is called once per frame
    void Update()
    {
        if (debugMode) {
            if (Input.GetKeyDown("r")) {
                dirty.storage = CleanFloat(dirty.storage+1000f);
                clean.storage = CleanFloat(clean.storage+1000f);
            }
        }
        // per second effects
        timer += Time.deltaTime;
        if (timer > 1){
            // increment water by auto modifiers every second
            dirty.storage = CleanFloat(dirty.storage+dirty.autoMod);
            clean.storage = CleanFloat(clean.storage+clean.autoMod);
            timer = 0f;
            // water usage starts with clean water
            if (waterUse > clean.storage){
                clean.storage = 0f;
                float remainder = CleanFloat(clean.storage-waterUse);
                dirty.storage = CleanFloat(dirty.storage-remainder);
            }
            else clean.storage = CleanFloat(clean.storage-waterUse);
        }
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

    public void ChangeWaterUse(float change){
        waterUse = CleanFloat(waterUse+change);
    }
    public float GetWaterUse(){
        return waterUse;
    }

    private void UpdateUI(){
        // water
        if (dirty.storage % 1 == 0)
            dirtyText.text = string.Format("DIRTY WATER: {0,0:F0}",dirty.storage);
        else dirtyText.text = string.Format("DIRTY WATER: {0,0:F1}",dirty.storage);

        if (clean.storage % 1 == 0)
            cleanText.text = string.Format("CLEAN WATER: {0,0:F0}",clean.storage);
        else cleanText.text = string.Format("CLEAN WATER: {0,0:F1}",clean.storage);
    }
}
