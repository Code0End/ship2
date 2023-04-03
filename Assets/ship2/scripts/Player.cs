using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {
    // xp
    private int coins = 0;
    public TMP_Text coinText;

    public void AddCoin(int value) {
        this.coins += value;
        coinText.text = String.Concat("Coins: ", this.coins);
    }
}
