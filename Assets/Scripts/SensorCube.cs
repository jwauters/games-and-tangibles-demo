using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SensorCube : MonoBehaviour
{
    private int prevStepCount;
    public AudioClip audioFootsteps;

    private Color curColor = new Color( 1, 0, 0 );

    /// <summary>
    /// Initialize step count
    /// </summary>
    void Start()
    {
        if ( InputSystem.GetDevice<StepCounter>() != null )
        {
            prevStepCount = StepCounter.current.stepCounter.ReadValue();
        }
    }

    /// <summary>
    /// Updates the cube based on sensor readouts
    /// </summary>
    void Update()
    {
        UpdatePosition();
        UpdateRotation();
        UpdateBrightness();
        UpdateVisibility();
        PlayStepSound();
        UpdateColor();

        //Continuously drift the cube back to (0,0,0) to counteract accelerometer movement
        this.transform.position = Vector3.MoveTowards( this.transform.position, new Vector3( 0, 0, 0 ), 0.5f * Time.deltaTime ); // multiply by Time.deltatime for framerate independence
    }

    /// <summary>
    /// Updates the cube's position based on accelerometer data
    /// </summary>
    public void UpdatePosition()
    {
        if ( InputSystem.GetDevice<LinearAccelerationSensor>() != null ) //We use the linear acceleration because we want to ignore gravity
        {
            this.transform.Translate( LinearAccelerationSensor.current.acceleration.ReadValue() / 5 );
        }
    }

    /// <summary>
    /// Updates the cube's rotation based on the gyroscope
    /// </summary>
    public void UpdateRotation()
    {
        if ( InputSystem.GetDevice<AttitudeSensor>() != null )
        {
            this.transform.rotation = AttitudeSensor.current.attitude.ReadValue();
        }
    }
    
    /// <summary>
    /// Update the cube's brightness based on the light sensor
    /// </summary>
    public void UpdateBrightness()
    {
        if ( InputSystem.GetDevice<LightSensor>() != null )
        {
            float brightness = Mathf.Clamp( LightSensor.current.lightLevel.ReadValue(), 0, 200 ) / 200; //For demonstration purposes, we take 200 as maximum brightness
            this.GetComponent<Renderer>().material.SetColor( "_Color", curColor * brightness );
        }
    }

    /// <summary>
    /// Makes the cube invisible if the proximity sensor is triggered
    /// </summary>
    public void UpdateVisibility()
    {
        if ( InputSystem.GetDevice<ProximitySensor>() != null )
        {
            this.GetComponent<MeshRenderer>().enabled = ProximitySensor.current.distance.ReadValue() > 1;
        }
    }

    /// <summary>
    /// Play a footstep sound if the step counter has gone up
    /// </summary>
    public void PlayStepSound()
    {
        if ( InputSystem.GetDevice<StepCounter>() != null )
        {
            int curStepCount = StepCounter.current.stepCounter.ReadValue();
            if( curStepCount > prevStepCount )
            {
                prevStepCount = curStepCount;
                this.GetComponent<AudioSource>().PlayOneShot( audioFootsteps );
            }
        }
    }

    /// <summary>
    /// Cycles the cube's color between R, G and B when it is tapped. 
    /// Not we do not actually have to set the cube's color here, that happens in UpdateBrightness the next frame anyway
    /// </summary>
    public void UpdateColor()
    {
        if( Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began ) //old input system
        {
            Ray raycast = Camera.main.ScreenPointToRay( Input.GetTouch(0).position ); //old input system
            RaycastHit raycastHit;
            if ( Physics.Raycast( raycast, out raycastHit ) && raycastHit.collider.name == "Cube" )
            {
                if ( curColor.r != 0 )
                {
                    curColor = new Color( 0, 1, 0 );
                }
                else if ( curColor.g != 0 )
                {
                    curColor = new Color( 0, 0, 1 );
                }
                else
                {
                    curColor = new Color( 1, 0, 0 );
                }
            }
        }
    }
}
