using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class TouchReadout : MonoBehaviour
{
    public Text textTouchOldInput;
    public Text textTouchNewInput;

    // Start is called before the first frame update
    void Start()
    {
        //for the new system
        if ( InputSystem.GetDevice<Touchscreen>() != null )
        {
            InputSystem.EnableDevice( Touchscreen.current );
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTouchInputOld();
        ProcessTouchInputNew();
    }

    /// <summary>
    /// An example of how to process touch input via the old input system
    /// </summary>
    private void ProcessTouchInputOld()
    {
        Touch[] touches = Input.touches; //That's all there is to it, returns an array of touch objects that contain position and phase information
        textTouchOldInput.text = "";

        int i = 1;
        foreach( Touch t in touches )
        {
            textTouchOldInput.text += i + ": " + t.position + " " + t.phase + "\n";
            i++;
        }
    }

    /// <summary>
    /// An example of how to process touch input via the new input system
    /// https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Touch.html
    /// </summary>
    private void ProcessTouchInputNew()
    {
        if ( InputSystem.GetDevice<Touchscreen>() != null )
        {
            ReadOnlyArray<TouchControl> touches = Touchscreen.current.touches; //Contains even more detailed info than the old Touch class. Always of length 10 (one entry per finger)
            textTouchNewInput.text = "";

            int i = 1;
            foreach ( TouchControl t in touches )
            {
                textTouchNewInput.text += i + ": " + t.position.ReadValue() + " " + t.phase.ReadValue() + "\n";
                i++;
            }
        }
    }
}
