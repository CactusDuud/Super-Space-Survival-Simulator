// Written by Sage Mahmud

using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static CropManager _instance;
    public static CropManager GetInstance { get { return _instance; } }

    [Header("References")]
    List<GameObject> _livingCrops = new List<GameObject>();

    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this; 

        foreach (GameObject _crop in GameObject.FindGameObjectsWithTag("CropTag"))
        {
            _livingCrops.Add(_crop);
        }
    }

    public GameObject GetRandomCrop()
    {
        return _livingCrops[Random.Range(0, _livingCrops.Count - 1)];
    }

    public void AddCrop(GameObject crop)
    {
        _livingCrops.Add(crop);
    }

    public void RemoveCrop(GameObject crop)
    {
        _livingCrops.Remove(crop);
    }

    public bool IsLivingCrop()
    {
        return _livingCrops.Count > 0;
    }
}
