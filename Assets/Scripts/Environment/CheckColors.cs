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

    [SerializeField]
    private GameObject _playerCamera;

    [SerializeField]
    private GameObject _topdownCamera;

    private void Update()
    {
        if (_platformBlue.ColorArrived == true 
            && _platformRed.ColorArrived == true
            && _platformYellow.ColorArrived == true)
        {          
            _topdownCamera.SetActive(true);
            _playerCamera.SetActive(false);
            
            Invoke("LoadNextScene", 4f);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
