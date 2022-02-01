// the code was written by Sage Mahmud, Elizabeth Castreje, Esmeralda Juarez, Miguel Aleman

//the only job this script has is too create crops

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropSpawner : MonoBehaviour
{
    //variables will help with instantiate prefabs and put them in the correct heirarchy
    [Header("Prefabs")]
    public GameObject plant1;
    public Transform cropParent;


    //this function gets a string type that lets the function know which plant too instantiate
    public void CreatePlant(string plantType)
    {
        if (plantType == "potato")
        {
            Debug.Log("potato enemy!!!");
            Instantiate(plant1, transform.position, Quaternion.identity, cropParent);
        }


    }
}