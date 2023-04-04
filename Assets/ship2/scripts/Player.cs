using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    // xp
    private int coins = 0;
    public TMP_Text coinText;

    private float requiredXp = 10;
    private float currentXp= 0;

    private WeaponsManager weaponsManager;
    //public Button btnUpgradeDamage;
    //public Button btnUpgradeRate;
    //public Button btnUpgradeTurn;

    private void Start() {
        weaponsManager = gameObject.GetComponentInParent<WeaponsManager>();
    }

    public void AddCoin() {
        this.coins++;
        coinText.text = String.Concat("Coins: ", this.coins);

        this.currentXp++;

        if(currentXp >= requiredXp) {
            currentXp -= requiredXp;
            requiredXp += 10;

            // Ship updagrade
            //btnUpgradeDamage.enabled = true;
            //btnUpgradeRate.enabled = true;
            //btnUpgradeTurn.enabled = true;
        }

        weaponsManager.UpdateXP(currentXp / requiredXp);
    }
}
