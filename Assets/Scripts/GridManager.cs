// Written by Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static GridManager _instance;
    public static GridManager GetInstance { get { return _instance; } }

    [Header("Grid References")]
    [SerializeField] Tile _tilePrefab;

    [Header("Grid Attributes")]
    [SerializeField] int _width;
    [SerializeField] int _height;

    [Header("Grid Statistics")]
    Dictionary<Vector2, Tile> _tileMap;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void CreateGrid()
    {
        _tileMap = new Dictionary<Vector2, Tile>();

        for (int x = -(_width - 1 / 2); x < _width; x++)
        {
            for (int y = -(_height - 1 / 2); y < _height; y++)
            {
                var newTile = Instantiate(_tilePrefab, new Vector3(x, y, 1), Quaternion.identity);
                newTile.name = $"Tile ({x}, {y})";

                _tileMap[new Vector2(x, y)] = newTile;
            }
        }
    }

    public Tile GetTileByIndex(Vector2 pos)
    {
        if(_tileMap.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
