using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingManager : MonoBehaviour
{

    // ENCAPSULATION
    private Vector3 plantingPoint; // Raycast hit point
    [SerializeField] private GameObject raycastStartPoint; // Attached to the player
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private SoundManager soundManager;

    public void PlantTree()
    {
        if(CheckValidGround())
        {
            int index = GetRandomIndex();
            Instantiate(treePrefabs[index], plantingPoint, treePrefabs[index].transform.rotation);
            soundManager.PlaySound();
        }  
    }

    private bool CheckValidGround()
    {
        //Raycast from plantingPointObject to the ground
        //If it hits to the "Ground", it is valid and otherwise not.

        RaycastHit hit;
        if (Physics.Raycast(raycastStartPoint.transform.position, raycastStartPoint.transform.TransformDirection(Vector3.down), out hit, 10f) && hit.collider.tag == "Ground")
        {
            plantingPoint = hit.point;
            return true;
        }

        return false;
    }

    private int GetRandomIndex()
    {
        return Random.Range(0, treePrefabs.Length);
    }

}
