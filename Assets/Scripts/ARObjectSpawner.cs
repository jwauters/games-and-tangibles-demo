using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    private ARPlacementIndicator placementIndicator;

    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = GetComponent<ARPlacementIndicator>(); //retrieve component on the same gameobject
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began ) //is there at least one touch and is this the frame that the first touch began (uses the old input system!)
        {
            GameObject objectToSpawn = objectsToSpawn[ Random.Range( 0, objectsToSpawn.Length - 1 )];
            GameObject obj = Instantiate( objectToSpawn, this.transform.position, this.transform.rotation );
        }
    }
}
