using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {
    // xp
    private int coins = 0;
    public TMP_Text coinText;
    public GameObject[] buttons;
    public GameObject[] positions;
    public GameObject[] upgrades;

    public bool anchor_a = true;
    public bool ballista_a = true;

    private float requiredXp = 10;
    private float currentXp = 0;

    private WeaponsManager weaponsManager;

    private void Start() {
        weaponsManager = gameObject.GetComponentInParent<WeaponsManager>();
    }

    public void AddCoin() {
        this.coins++;
        coinText.text = String.Concat("Coins: ", this.coins);

        this.currentXp++;

        if (currentXp >= requiredXp) {
            currentXp -= requiredXp;
            requiredXp += 10;

            // Ship upgrade
            PauseGame();

            random_upgrade();
        }

        weaponsManager.UpdateXP(currentXp / requiredXp);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void random_upgrade()
    {
        List<GameObject> availableItems = new List<GameObject>(buttons);

        List<GameObject> selectedItems = new List<GameObject>();

        int numSelected = 0;
        while (numSelected < 3)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableItems.Count);
            GameObject currentItem = availableItems[randomIndex];
            availableItems.RemoveAt(randomIndex);

            // Check if the item is special
            if (currentItem.name == "SetactiveD" && anchor_a)
            {
                selectedItems.Add(currentItem);
                upgrades[numSelected] = currentItem;
                numSelected++;
            }
            else if (currentItem.name == "SetactiveB" && ballista_a)
            {
                selectedItems.Add(currentItem);
                upgrades[numSelected] = currentItem;
                numSelected++;
            }
            else if (currentItem.name != "SetactiveD" && currentItem.name != "SetactiveB")
            {
                selectedItems.Add(currentItem);
                upgrades[numSelected] = currentItem;
                numSelected++;
            }
        }

        List<GameObject> availablepos = new List<GameObject>(positions);

        List<GameObject> selectedpos = new List<GameObject>();

        int posSelected = 0;
        while (posSelected < 3)
        {
            int randomI = UnityEngine.Random.Range(0, availablepos.Count);
            GameObject currentpos = availablepos[randomI];
            availablepos.RemoveAt(randomI);
            selectedpos.Add(currentpos);
            posSelected++;
        }

        for (int i = 0; i < selectedItems.Count; i++)
           {
               int positionIndex = UnityEngine.Random.Range(0, selectedpos.Count);
               selectedItems[i].SetActive(true);
               selectedItems[i].transform.position = selectedpos[positionIndex].transform.position;
               selectedpos.RemoveAt(positionIndex);
           }

    }
        
    

   public void upgrades_off()
    {
        Time.timeScale = 1f;
        upgrades[0].SetActive(false);
        upgrades[1].SetActive(false);
        upgrades[2].SetActive(false);
    }
}
