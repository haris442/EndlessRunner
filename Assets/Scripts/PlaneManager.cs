using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> planePrefabs = new List<GameObject>();
    
    Transform previousSpawnPlane;
    void Start()
    {
        previousSpawnPlane = transform;
        for (int i = 0; i < planePrefabs.Count; i++)
        {
            GameObject obj = Instantiate(planePrefabs[i]);
            obj.SetActive(false);
            planePrefabs[i] = obj;

        }
        GeneratePlane();
        GeneratePlane();
        GeneratePlane();
        GeneratePlane();

        GeneratePlane();

    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
   
    }
    private GameObject GetPooledObject()
    {
        int randomNumber = Random.Range(0, planePrefabs.Count);

        if (!planePrefabs[randomNumber].activeInHierarchy)
        {
         //   Debug.Log("GetPooledObject Run" + randomNumber);
            return planePrefabs[randomNumber];
        }
        return null;
    }

    private void GeneratePlane()
    {
        GameObject plane = GetPooledObject();

        if(plane==null)
        {
             GeneratePlane();
        }
        if (plane != null)
        {
            float groundScale = plane.GetComponent<PlaneHandler>().planeGround.localScale.z;

            Debug.Log("GeneratePlane  " + groundScale + " previousSpawnPlane  " + previousSpawnPlane.transform.localScale.z);
            plane.transform.position = new Vector3(0, 0, previousSpawnPlane.transform.localScale.z/2+ groundScale / 2  + previousSpawnPlane.transform.position.z); 
            plane.gameObject.SetActive(true);


            previousSpawnPlane.transform.localScale = new Vector3(1, 1, groundScale);
            previousSpawnPlane.transform.position = plane.transform.position;
        }
    }
}

