using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public struct Boost {
        public float baseCost;
        public float currentCost;
        public int count;
        public Boost(float cost){
            baseCost = cost;
            currentCost = cost;
            count = 0;
        }
        public Boost(float original, float current, int num){
            baseCost = original;
            currentCost = current;
            count = num;
        }
    }

    public struct Upgrade{
        public bool bought;
        public float cost;
        public Upgrade(float price){
            bought = false;
            cost = price;
        }
        public Upgrade(float price, bool buy){
            bought = buy;
            cost = price;
        }
    }

    public Dictionary<string,Boost> boosts = new Dictionary<string, Boost>();
    private Dictionary<string,Upgrade> upgrades = new Dictionary<string,Upgrade>();
    [SerializeField]
    private GameManager manager;
    public bool upgradesChanged;
    [SerializeField]
    private ScrollRect scrollContent;

    // Start is called before the first frame update
    void Start()
    {
        boosts.Add("bucket",new Boost(10f));
        boosts.Add("helping hand",new Boost(10f));
        boosts.Add("water filter",new Boost(10f));
        boosts.Add("still",new Boost(50f));
        boosts.Add("rainwater collector",new Boost(100f));
        boosts.Add("well",new Boost(200f));

        upgrades.Add("big buckets",new Upgrade(100f));
        upgrades.Add("huge buckets",new Upgrade(500f));

        upgrades.Add("sewers",new Upgrade(2000f));
        upgrades.Add("sewage treatment",new Upgrade(5000f));

        upgrades.Add("activated carbon",new Upgrade(100f));
        upgrades.Add("ultraviolet",new Upgrade(500f));

        upgrades.Add("clean equipment",new Upgrade(500f));
        upgrades.Add("clean fuel",new Upgrade(1000f));
        upgrades.Add("trained operators",new Upgrade(500f));

        upgrades.Add("strong plastic",new Upgrade(1000f));
        upgrades.Add("water butts",new Upgrade(5000f));
        upgrades.Add("roof gutters",new Upgrade(5000f));

        upgrades.Add("hand pump",new Upgrade(1000f));
        upgrades.Add("electric pump",new Upgrade(5000f));
        upgrades.Add("hard workers",new Upgrade(1000f));

        upgradesChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradesChanged) { // dirty flag
            // initialise
            float dirtyClick = 1f;
            float dirtyAuto = 0f;
            float cleanClick = 0.2f;
            float cleanAuto = 0f;
            float pureClick = 0f;
            float pureAuto = 0f;

            // bucket
            dirtyClick = manager.CleanFloat(dirtyClick+boosts["bucket"].count);
            if (upgrades["big buckets"].bought) { // 100% increase
                dirtyClick = manager.CleanFloat(dirtyClick+boosts["bucket"].count);
                dirtyAuto = manager.CleanFloat(dirtyAuto+boosts["helping hand"].count);
            }
            if (upgrades["huge buckets"].bought) { // another 100%
                dirtyClick = manager.CleanFloat(dirtyClick+boosts["bucket"].count);
                dirtyAuto = manager.CleanFloat(dirtyAuto+boosts["helping hand"].count);
            }
            
            // helping hand
            dirtyAuto = manager.CleanFloat(dirtyAuto+boosts["helping hand"].count);
            if (upgrades["sewage treatment"].bought) // return half consumed water
                pureAuto = manager.CleanFloat(pureAuto+(manager.GetWaterUse()/2));
            else if (upgrades["sewers"].bought) // add half consumed water as dirty
                dirtyAuto = manager.CleanFloat(dirtyAuto+(manager.GetWaterUse()/2));
            
            // water filter
            cleanClick = manager.CleanFloat(cleanClick+(0.2f*boosts["water filter"].count));
            if(upgrades["activated carbon"].bought) // 100% boost
                cleanClick = manager.CleanFloat(cleanClick+(0.2f*boosts["water filter"].count));
            if(upgrades["ultraviolet"].bought) // +100%
                cleanClick = manager.CleanFloat(cleanClick+(0.2f*boosts["water filter"].count));

            // still
            cleanAuto = manager.CleanFloat(cleanAuto+(0.2f*boosts["still"].count));
            if (upgrades["clean equipment"].bought){
                cleanAuto = manager.CleanFloat(cleanAuto+(0.1f*boosts["still"].count));
            }
            if (upgrades["clean fuel"].bought){
                cleanAuto = manager.CleanFloat(cleanAuto+(0.1f*boosts["still"].count));
            }
            if (upgrades["trained operators"].bought){
                cleanAuto = manager.CleanFloat(cleanAuto+(0.02f*boosts["helping hand"].count*boosts["still"].count));
            }

            // rainwater collector
            pureAuto = manager.CleanFloat(pureAuto+(0.5f*boosts["rainwater collector"].count));
            if (upgrades["strong plastic"].bought) // 20% of 0.5
                pureAuto = manager.CleanFloat(pureAuto+(0.1f*boosts["rainwater collector"].count));
            if (upgrades["water butts"].bought) // 40% of 0.5
                pureAuto = manager.CleanFloat(pureAuto+(0.2f*boosts["rainwater collector"].count));
            if (upgrades["roof gutters"].bought) // 2% of 1.6x0.5
                dirtyAuto = manager.CleanFloat(dirtyAuto+(0.8f*boosts["rainwater collector"].count*0.02f*boosts["helping hand"].count));

            // well
            pureClick = manager.CleanFloat(pureClick+(0.1f*boosts["well"].count));
            if (upgrades["hand pump"].bought)
                pureClick = manager.CleanFloat(pureClick+(0.2f*boosts["well"].count));
            if (upgrades["electric pump"].bought)
                pureClick = manager.CleanFloat(pureClick+(0.2f*boosts["well"].count));
            if (upgrades["hard workers"].bought)
                pureClick = manager.CleanFloat(pureClick+(0.1f*boosts["well"].count*boosts["helping hand"].count));

            manager.ChangeWaterUse(0.2f*boosts["helping hand"].count);
            manager.dirty.clickMod = dirtyClick;
            manager.dirty.autoMod = dirtyAuto;
            manager.clean.clickMod = cleanClick;
            manager.clean.autoMod = cleanAuto;
            manager.pure.clickMod = pureClick;
            manager.pure.autoMod = pureAuto;

            // clean flag
            upgradesChanged = false;
        }
    }

    public void BuyBoost(string name, Text title, Text price){
        string id = name.ToLower();
        if (manager.clean.storage<boosts[id].currentCost) return;
        else {
            // pay
            manager.clean.storage = manager.CleanFloat(manager.clean.storage-boosts[id].currentCost);
            // cost is x1.05 current cost; geometric sequence a(r^((n+1)-1))
            float a = boosts[id].baseCost;
            float r = 1.05f;
            int n = boosts[id].count+1;
            float totalCost = manager.CleanFloat(a*Mathf.Pow(r,n));
            
            // update dictionary
            boosts[id] = new Boost(a,totalCost,n);

            // update ui
            title.text = name+" ("+boosts[id].count.ToString()+")";
            price.text = "Costs "+boosts[id].currentCost.ToString("F1");

            upgradesChanged = true;
        }
    }

    public void BuyUpgrade(string name, Button currButt, Button nextButt){
        string id = name.ToLower();
        if (manager.clean.storage<upgrades[id].cost) return;
        else {
            manager.clean.storage = manager.CleanFloat(manager.clean.storage-upgrades[id].cost);

            upgrades[id] = new Upgrade(upgrades[id].cost,true);
            
            currButt.interactable = false;
            if (nextButt != null)
                nextButt.interactable = true;

            upgradesChanged = true;
        }
    }

    public void SwitchDisplay(RectTransform trans){
        scrollContent.content = trans;
    }
}
