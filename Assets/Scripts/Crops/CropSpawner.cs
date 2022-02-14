// the code was written by Sage Mahmud, Elizabeth Castreje, Esmeralda Juarez, Miguel Aleman

//the only job this script has is too create crops

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private Transform _cropParent;

    [Header("Placement")]
    [SerializeField] private Grid _gridLayout;
    [SerializeField] private float elevation = -0.25f;

    private Vector3 _placement;
    private bool _onSoil = false;
    private bool _isplanted = false;
    



    //this function gets a string type that lets the function know which plant too instantiate
    public void CreatePlant(plantType plantIndex)
    {
        if (_onSoil && !_isplanted) 
        {
            Vector3Int tileCellPos = _gridLayout.WorldToCell(transform.position);
            Vector3 centerCell = _gridLayout.GetCellCenterWorld(tileCellPos);
            centerCell = new Vector3(centerCell.x, centerCell.y + elevation, centerCell.z);
            
            GameObject _newCrop = Instantiate(_plants[(int)plantIndex], centerCell, Quaternion.identity, _cropParent);
            CropManager.GetInstance.AddCrop(_newCrop);
        }
        //position of crops is now good too go

        /*if (_onSoil)
        {
            _placement = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            if (plantType == "potato")
            {
                Debug.Log("potato enemy!!!");
                Instantiate(_plant1, _placement, Quaternion.identity, _cropParent);
            }
        }*/


    }
    

    //this function will basically looking at what the player collided with and turn true if they collided with soil/plant
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CropTag")) 
        {
            Debug.Log("plant tile enter");
            _isplanted = true;
        }
        if (other.gameObject.CompareTag("Soil Tag")) 
        {
            _onSoil = true;
        }  
    }


    //this function will basically looking at what the player collided with and turn false if they collided with soil/plant
    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("CropTag"))
        {
            Debug.Log("plant tile exit");
            _isplanted = false;
        }
        if (other.gameObject.CompareTag("Soil Tag"))
        {
            _onSoil = false;
        }
    }

}