using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    int bucketNum;
    int helpingNum;
    int filterNum;
    int stillNum;
    int collectorNum;
    int wellNum;

    private void Start()
    {
        
    }

    void Update()
    {
        if (upgradeManager.upgradesChanged == true)
        {
            bucketNum = upgradeManager.boosts["bucket"].count;
            helpingNum = upgradeManager.boosts["helping hand"].count;
            filterNum = upgradeManager.boosts["water filter"].count;
            stillNum = upgradeManager.boosts["still"].count;
            collectorNum = upgradeManager.boosts["rainwater collector"].count;
            wellNum = upgradeManager.boosts["well"].count;

            switch(bucketNum)
            {
                case 1:
                    transform.Find("Buckets").GetChild(0).gameObject.SetActive(true);
                    break;
                case 10:
                    transform.Find("Buckets").GetChild(1).gameObject.SetActive(true);
                    break;
                case 50:
                    transform.Find("Buckets").GetChild(2).gameObject.SetActive(true);
                    break;
            }

            switch (helpingNum)
            {
                case 1:
                    transform.Find("Guys").GetChild(0).gameObject.SetActive(true);
                    break;
                case 2:
                    transform.Find("Guys").GetChild(1).gameObject.SetActive(true);
                    break;
                case 3:
                    transform.Find("Guys").GetChild(2).gameObject.SetActive(true);
                    break;
                case 4:
                    transform.Find("Guys").GetChild(3).gameObject.SetActive(true);
                    break;
                case 5:
                    transform.Find("Guys").GetChild(4).gameObject.SetActive(true);
                    break;
                case 6:
                    transform.Find("Guys").GetChild(5).gameObject.SetActive(true);
                    break;
                case 7:
                    transform.Find("Guys").GetChild(6).gameObject.SetActive(true);
                    break;
                case 8:
                    transform.Find("Guys").GetChild(7).gameObject.SetActive(true);
                    break;
                case 9:
                    transform.Find("Guys").GetChild(8).gameObject.SetActive(true);
                    break;
                case 10:
                    transform.Find("Guys").GetChild(9).gameObject.SetActive(true);
                    break;
                case 11:
                    transform.Find("Guys").GetChild(10).gameObject.SetActive(true);
                    break;
                case 12:
                    transform.Find("Guys").GetChild(11).gameObject.SetActive(true);
                    break;
                case 13:
                    transform.Find("Guys").GetChild(12).gameObject.SetActive(true);
                    break;
                case 14:
                    transform.Find("Guys").GetChild(13).gameObject.SetActive(true);
                    break;
            }

            switch (filterNum)
            {
                case 1:
                    transform.Find("Filters").GetChild(0).gameObject.SetActive(true);
                    break;
                case 10:
                    transform.Find("Filters").GetChild(1).gameObject.SetActive(true);
                    break;
                case 50:
                    transform.Find("Filters").GetChild(2).gameObject.SetActive(true);
                    break;
            }

            switch (stillNum)
            {
                case 1:
                    transform.Find("Stills").GetChild(0).gameObject.SetActive(true);
                    break;
                case 10:
                    transform.Find("Stills").GetChild(1).gameObject.SetActive(true);
                    break;
                case 50:
                    transform.Find("Stills").GetChild(2).gameObject.SetActive(true);
                    break;
            }

            switch (collectorNum)
            {
                case 1:
                    transform.Find("Collectors").GetChild(0).gameObject.SetActive(true);
                    break;
                case 10:
                    transform.Find("Collectors").GetChild(1).gameObject.SetActive(true);
                    break;
                case 50:
                    transform.Find("Collectors").GetChild(2).gameObject.SetActive(true);
                    break;
            }

            switch (wellNum)
            {
                case 1:
                    transform.Find("Wells").GetChild(0).gameObject.SetActive(true);
                    break;
                case 10:
                    transform.Find("Wells").GetChild(1).gameObject.SetActive(true);
                    break;
                case 50:
                    transform.Find("Wells").GetChild(2).gameObject.SetActive(true);
                    break;
            }
        }
    }
}
