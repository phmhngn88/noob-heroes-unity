using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AmmoData
{
    public int coin;
    public int[] numberOfAmmo;

    public AmmoData(int[] numberOfAmmo, int coin)
    {
        this.coin = coin;

        this.numberOfAmmo = new int[3];
        this.numberOfAmmo[0] = numberOfAmmo[0];
        this.numberOfAmmo[1] = numberOfAmmo[1];
        this.numberOfAmmo[2] = numberOfAmmo[2];
    }
}
