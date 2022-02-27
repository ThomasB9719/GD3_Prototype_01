using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckColorsFinal : MonoBehaviour
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

    [SerializeField]
    private GameObject _endScreen;

    [SerializeField]
    private GameObject _quitButton;

    private void FixedUpdate()
    {
        if (_platformBlue.ColorArrived == true
            && _platformRed.ColorArrived == true
            && _platformYellow.ColorArrived == true
            )
        {
            _topdownCamera.SetActive(true);
            _playerCamera.SetActive(false);

            Invoke("LoadEndScreen", 4f);
        }
    }

    private void LoadEndScreen()
    {
        _endScreen.SetActive(true);

        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_quitButton);
    }
}
