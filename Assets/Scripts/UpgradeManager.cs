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
    }

    public Dictionary<string,Boost> boosts = new Dictionary<string, Boost>();
    private Dictionary<string,Upgrade> upgrades = new Dictionary<string,Upgrade>();
    [SerializeField]
    private GameManager manager;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
