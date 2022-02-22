//Code written by Sebastian Carbajal

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropItem : MonoBehaviour
{
    public enum CropType
    {
        potato,
        carrot,
        beet,
        sunflower,
        moonflower
    }

    public CropType itemType;
    public int amount;
}
