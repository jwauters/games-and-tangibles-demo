using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using Gyroscope = UnityEngine.InputSystem.Gyroscope; //resolve ambiguity between UnityEngine.Gyroscope and UnityEngine.InputSystem.Gyroscope


public class SensorReadout : MonoBehaviour
{
    //UI text fields
    public Text textAcceleration;
    public Text textGravity;
    public Text textLinearAcceleration;
    public Text textAngularVelocity;
    public Text textAttitude;
    public Text textAtmosphericPressure;
    public Text textRelativeHumidity;
    public Text textAmbientTemperature;
    public Text textMagneticField;
    public Text textLightLevel;
    public Text textDistance;
    public Text textStepCounter;

    /// <summary>
    /// Start is called before the first frame update
    /// Enable all available sensors at script startup (check if they exist first)
    /// </summary>
    void Start()
    {
        //Accelerometer
        if ( InputSystem.GetDevice<Accelerometer>() != null )
        {
            InputSystem.EnableDevice( Accelerometer.current );
        }
        if ( InputSystem.GetDevice<GravitySensor>() != null )
        {
            InputSystem.EnableDevice( GravitySensor.current );
        }
        if ( InputSystem.GetDevice<LinearAccelerationSensor>() != null )
        {
            InputSystem.EnableDevice( LinearAccelerationSensor.current );
        }

        //Gyroscope
        if ( InputSystem.GetDevice<Gyroscope>() != null )
        {
            InputSystem.EnableDevice( Gyroscope.current );
        }
        if ( InputSystem.GetDevice<AttitudeSensor>() != null )
        {
            InputSystem.EnableDevice( AttitudeSensor.current );
        }

        //Weather-related sensors
        if ( InputSystem.GetDevice<PressureSensor>() != null )
        {
            InputSystem.EnableDevice( PressureSensor.current );
        }
        if ( InputSystem.GetDevice<HumiditySensor>() != null )
        {
            InputSystem.EnableDevice( HumiditySensor.current );
        }
        if ( InputSystem.GetDevice<AmbientTemperatureSensor>() != null )
        {
            InputSystem.EnableDevice( AmbientTemperatureSensor.current );
        }

        //Misc sensors
        if ( InputSystem.GetDevice<MagneticFieldSensor>() != null )
        {
            InputSystem.EnableDevice( MagneticFieldSensor.current );
        }
        if ( InputSystem.GetDevice<LightSensor>() != null )
        {
            InputSystem.EnableDevice( LightSensor.current );
        }
        if ( InputSystem.GetDevice<ProximitySensor>() != null )
        {
            InputSystem.EnableDevice( ProximitySensor.current );
        }
        if ( InputSystem.GetDevice<StepCounter>() != null )
        {
            InputSystem.EnableDevice( StepCounter.current );
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// Read out all available sensor values and show them in the UI.
    /// </summary>
    void Update()
    {
        //Accelerometer
        if ( InputSystem.GetDevice<Accelerometer>() != null )
        {
            Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();
            textAcceleration.text = acceleration.ToString( "F2" ); //F2 indicates the number of decimal places to display
        }
        if ( InputSystem.GetDevice<GravitySensor>() != null )
        {
            Vector3 gravity = GravitySensor.current.gravity.ReadValue();
            textGravity.text = gravity.ToString( "F2" );
        }
        if ( InputSystem.GetDevice<LinearAccelerationSensor>() != null )
        {
            Vector3 linearAcceleration = LinearAccelerationSensor.current.acceleration.ReadValue();
            textLinearAcceleration.text = linearAcceleration.ToString( "F2" );
        }

        //Gyroscope
        if ( InputSystem.GetDevice<Gyroscope>() != null )
        {
            Vector3 angularVelocity = Gyroscope.current.angularVelocity.ReadValue();
            textAngularVelocity.text = angularVelocity.ToString( "F2" );
        }
        if ( InputSystem.GetDevice<AttitudeSensor>() != null )
        {
            Quaternion attitude = AttitudeSensor.current.attitude.ReadValue(); //A Quaternion is a way to express an orientation in 3D space
            textAttitude.text = attitude.ToString( "F2" );
        }

        //Weather-related sensors
        if ( InputSystem.GetDevice<PressureSensor>() != null )
        {
            float atmosphericPressure = PressureSensor.current.atmosphericPressure.ReadValue();
            textAtmosphericPressure.text = atmosphericPressure.ToString();
        }
        if ( InputSystem.GetDevice<HumiditySensor>() != null )
        {
            float relativeHumidity = HumiditySensor.current.relativeHumidity.ReadValue();
            textRelativeHumidity.text = relativeHumidity.ToString();
        }
        if ( InputSystem.GetDevice<AmbientTemperatureSensor>() != null )
        {
            float ambientTemperature = AmbientTemperatureSensor.current.ambientTemperature.ReadValue();
            textAmbientTemperature.text = ambientTemperature.ToString();
        }

        //Misc sensors
        if ( InputSystem.GetDevice<MagneticFieldSensor>() != null )
        {
            Vector3 magneticField = MagneticFieldSensor.current.magneticField.ReadValue();
            textMagneticField.text = magneticField.ToString();
        }
        if ( InputSystem.GetDevice<LightSensor>() != null )
        {
            float lightLevel = LightSensor.current.lightLevel.ReadValue();
            textLightLevel.text = lightLevel.ToString();
        }
        if ( InputSystem.GetDevice<ProximitySensor>() != null )
        {
            float distance = ProximitySensor.current.distance.ReadValue();
            textDistance.text = distance.ToString();
        }
        if ( InputSystem.GetDevice<StepCounter>() != null )
        {
            int stepCounter = StepCounter.current.stepCounter.ReadValue();
            textStepCounter.text = stepCounter.ToString();
        }
    }
}
