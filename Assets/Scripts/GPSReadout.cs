using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPSReadout : MonoBehaviour
{
    public Text textLatitude;
    public Text textLongitude;
    public Text textAltitude;
    public Text textHorizontalAccuracy;
    public Text textTimestamp;
    public Text textError;

    private bool isUpdating;


    private void Update()
    {
        if ( !isUpdating )
        {
            StartCoroutine( GetLocation() );
            isUpdating = !isUpdating;
        }
    }

    /// <summary>
    /// Coroutines using IEnumerator is Unity's built-in way of doing asynchronous function calls.
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator GetLocation()
    {
        if ( !Permission.HasUserAuthorizedPermission( Permission.FineLocation ) )
        {
            Permission.RequestUserPermission( Permission.FineLocation );
            Permission.RequestUserPermission( Permission.CoarseLocation );
        }
        // First, check if user has location service enabled
        if ( !Input.location.isEnabledByUser )
            yield return new WaitForSeconds( 10 ); //Basically: sleep for 10 seconds (only works in coroutines!)

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 10;
        while ( Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds( 1 );
            maxWait--;
        }

        // Service didn't initialize in 10 seconds
        if ( maxWait < 1)
        {
            textError.text = "Timed out";
            Debug.Log( "Timed out" );
            yield break;
        }

        // Connection has failed
        if ( Input.location.status == LocationServiceStatus.Failed )
        {
            textError.text = "Unable to determine device location";
            Debug.Log( "Unable to determine device location" );
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            LocationInfo gpsReadout = Input.location.lastData;
            textLatitude.text = gpsReadout.latitude.ToString("F8");
            textLongitude.text = gpsReadout.longitude.ToString("F8");
            textAltitude.text = gpsReadout.altitude.ToString("F8");
            textHorizontalAccuracy.text = gpsReadout.horizontalAccuracy.ToString("F4");
            textTimestamp.text = ConvertTimestampToDateTime( gpsReadout.timestamp ).ToString( "HH:mm:ss" );
                            
            Debug.Log( "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp );
        }

        // Stop service if there is no need to query location updates continuously
        isUpdating = !isUpdating;
        Input.location.Stop();
    }

    /// <summary>
    /// Standard UNIX timestamp is seconds since 01/01/1970, we convert to current time
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    private DateTime ConvertTimestampToDateTime( double timestamp )
    {
        DateTime result = new DateTime( 1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc );
        result = result.AddSeconds( timestamp ).ToLocalTime();
        return result;
    }
}