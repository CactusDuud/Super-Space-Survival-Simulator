// Written by Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManager : MonoBehaviour
{
    [SerializeField] Tilemap _cropMap;
    Dictionary<Vector3Int, GameObject> _cropTiles;

    void Awake()
    {
        _cropTiles = new Dictionary<Vector3Int, GameObject>();
    } 

    void SetCrop(Vector3Int tilePos, GameObject crop)
    {
        if (!_cropTiles.ContainsKey(tilePos)) _cropTiles.Add(tilePos, crop);
    }
}
