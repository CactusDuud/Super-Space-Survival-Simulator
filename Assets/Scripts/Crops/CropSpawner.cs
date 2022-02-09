// the code was written by Sage Mahmud, Elizabeth Castreje, Esmeralda Juarez, Miguel Aleman

//the only job this script has is too create crops

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropSpawner : MonoBehaviour
{
    //variables will help with instantiate prefabs and put them in the correct heirarchy
    [Header("Prefabs")]
    [SerializeField] private GameObject _plant1;
    [SerializeField] private Transform _cropParent;

    [Header("Placement")]
    [SerializeField] private Grid _gridLayout;
    [SerializeField] private float elevation = -0.25f;

    private Vector3 _placement;
    private bool _onSoil = false;
    



    //this function gets a string type that lets the function know which plant too instantiate
    public void CreatePlant(string plantType)
    {
        Vector3Int tileCellPos = _gridLayout.WorldToCell(transform.position);
        Vector3 centerCell = _gridLayout.GetCellCenterWorld(tileCellPos);
        centerCell = new Vector3(centerCell.x, centerCell.y+ elevation, centerCell.z);

        Instantiate(_plant1, centerCell, Quaternion.identity, _cropParent);
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
    /*
     *  
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        transform.position = gridLayout.CellToWorld(cellPosition); 
         var grid = _refMan.ground.GetComponentInParent<Grid>();
            if (_refMan.ground.GetTile(grid.WorldToCell(transform.position)))
            
           

            if(!_refMan.ground.GetTile(_refMan.ground.layoutGrid.WorldToCell(transform.position)))
           

    */
    //this function will basically look at what the player collidered with and turn true/false if they collided with soil
    /*void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (_onSoil)
        {
            _onSoil = false;
        }
        else
        {
            _onSoil = true;
        }
        
    }*/

}