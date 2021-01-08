using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAll : Power
{
    private const float COLLECTALL_MAGNET_STRENGTH = 9;
    private WaveSpawner waveSpawner;
    public override void Activate()
    {
        ItemPicker magnet = FindObjectOfType<ItemPicker>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        magnet.IncreaseMagnetStrengthForSeconds(COLLECTALL_MAGNET_STRENGTH,waveSpawner.TimeBetweenWaves());
    }


}
