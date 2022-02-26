using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckColors : MonoBehaviour
{
    [SerializeField]
    private ChangeColor _platformBlue;

    [SerializeField]
    private ChangeColor _platformRed;

    [SerializeField]
    private ChangeColor _platformYellow;

    private void Update()
    {
        if (_platformBlue.ColorArrived == true 
            && _platformRed.ColorArrived == true
            && _platformYellow.ColorArrived == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}