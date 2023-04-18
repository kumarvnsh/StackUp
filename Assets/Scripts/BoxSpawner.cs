using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;

    private void Start()
    {
        
    }

    public void SpawnBox()
    {
        GameObject boxObj = Instantiate(boxPrefab);

        Vector3 temp = transform.position;
        temp.z = 0f;
        
        boxObj.transform.position = temp;
 
    }
    

}
