// the code was written by Sage Mahmud, Elizabeth Castreje, Esmeralda Juarez, Miguel Aleman, Sebastian Carbajal

//the only job this script has is to create crops

using UnityEngine;
using UnityEngine.Tilemaps;

public class CropSpawner : MonoBehaviour
{
    //variables will help with instantiate prefabs and put them in the correct heirarchy
    [Header("References")]
    [SerializeField] private GameObject[] _plants;
    public enum plantType
    {
        potato,
        carrot,
        beet,
        sunflower,
        moonflower
    }

    private Transform _cropParent;

    //these variables help with the placement of the crops in soil tiles
    [Header("Crop Placement")]
    private Grid _gridLayout;
    private Tilemap _soilMap;
    [SerializeField] private float elevation = -0.25f;

    //bool that indicates if a plant is already in the place you want too plant
    private bool _isplanted = false;

    void Awake()
    {
        _cropParent = GameObject.Find("Crops").transform;
        _gridLayout = GameObject.Find("World/Grid").GetComponent<Grid>();
        _soilMap = GameObject.Find("World/Grid/Soil").GetComponent<Tilemap>();

    }


    //this function gets a string type that lets the function know which plant too instantiate
    public void CreatePlant(plantType plantIndex)
    {
        Vector3Int tileCellPos = _gridLayout.WorldToCell(transform.position);
        if (_soilMap.HasTile(_gridLayout.WorldToCell(transform.position)) && !_isplanted) 
        {
            if (GameManager.GetInstance.prosperity >= _plants[(int)plantIndex].GetComponent<Growth>().prosperityCost)
            {
                Debug.Log("Planted a crop");
                Vector3 centerCell = _gridLayout.GetCellCenterWorld(tileCellPos);
                centerCell = new Vector3(centerCell.x, centerCell.y + elevation, centerCell.z);

                GameObject _newCrop = Instantiate(_plants[(int)plantIndex], centerCell, Quaternion.identity, _cropParent);
                GameManager.GetInstance.SubtractProsperity(_newCrop.GetComponent<Growth>().prosperityCost);
                CropManager.GetInstance.AddCrop(_newCrop);
            }
        }
    }
    

    //this function will basically looking at what the player collided with and turn true if they collided with plant
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CropTag") && !_isplanted) 
        {
            _isplanted = true;
        }
    }

    //this function will basically check if player is still within a plant collider
    void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("CropTag") && !_isplanted)
        {
            _isplanted = true;
        }
    }


    //this function will basically looking at what the player collided with and turn false if they collided with plant
    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("CropTag") && _isplanted)
        {
            _isplanted = false;
        }
    }

}