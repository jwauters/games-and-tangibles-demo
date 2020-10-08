using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject placementIndicator;

    void Start()
    {
        Application.targetFrameRate = 60;

        //hide the components
        rayManager = FindObjectOfType<ARRaycastManager>();          //find object in the entire scene
        placementIndicator = transform.GetChild( 0 ).gameObject;    //find object in children TODO: just use GetComponent?

        //hide the placement indicator
        placementIndicator.SetActive( false );
    }

    void Update()
    {
        //raycast from center screen, returns a list of hits
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast( new Vector2( Screen.width/2, Screen.height/2), hits, TrackableType.Planes );

        //if we hit an AR plane, update the position and rotation of indicator
        if( hits.Count > 0 )
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if( !placementIndicator.activeInHierarchy )
            {
                placementIndicator.SetActive( true );
            }
        }
    }

}
