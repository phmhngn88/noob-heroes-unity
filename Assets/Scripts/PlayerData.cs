using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public float health;
    public float[] position;
    public int coin;
    public int sceneIndex;

    public PlayerData(float health, Vector2 position, int coin, int scene)
    {
        this.health = health;
        this.position = new float[2];
        this.position[0] = position.x;
        this.position[1] = position.y;

        this.coin = coin;
        this.sceneIndex = scene;
    }
}