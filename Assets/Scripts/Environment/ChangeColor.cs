using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField]
    private GameObject _nextCharacter;

    [SerializeField]
    private CameraManager _cameraManager;

    [SerializeField]
    private string _characterName;

    private Material _platformMaterial;
    private MeshRenderer _platformMeshRenderer;

    private string _greyMaterialName = "MAT_Grey (Instance)";
    private string _platformMaterialName;

    private void Awake()
    {
        //_greyMaterialName = _greyMaterial.name;

        _platformMeshRenderer = GetComponent<MeshRenderer>();
        _platformMaterial = _platformMeshRenderer.material;
        //_platformMaterial = _greyMaterial;
        _platformMaterialName = _platformMaterial.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_platformMaterialName == _greyMaterialName)
        {
            if (other.gameObject.layer == 6)
            {
                _platformMeshRenderer.material = other.GetComponent<MeshRenderer>().material;
                Debug.Log(other.gameObject.name + " has reached the destination");

                if(other.gameObject.name == _characterName)
                {
                    other.gameObject.GetComponent<PlayerLocomotion>().enabled = false;
                    other.gameObject.GetComponent<PlayerManager>().enabled = false;
                    other.gameObject.GetComponent<InputManager>().enabled = false;
                    other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    

                    _nextCharacter.SetActive(true);
                    _cameraManager.TargetTransform = _nextCharacter.transform;
                    _cameraManager.InputManager = _nextCharacter.GetComponent<InputManager>();
                }
            }
        }
        else
        {
            Debug.Log("Character needs to be removed");
        }
    }
}
