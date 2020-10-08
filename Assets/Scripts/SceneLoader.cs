using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Load a scene based on its position in the build settings
    /// </summary>
    public void LoadScene( int sceneNumber )
    {
        SceneManager.LoadScene( sceneNumber );
    }
}
