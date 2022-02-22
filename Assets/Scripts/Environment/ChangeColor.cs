using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //[SerializeField]
    //private Material _greyMaterial;

    private Material _platformMaterial;
    private MeshRenderer _platformMeshRenderer;

    private string _greyMaterialName = "MAT_Grey";
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
            }
        }
        else
        {
            Debug.Log("Character needs to be removed");
        }

        Debug.Log("platform color: " + _platformMaterialName);
        Debug.Log(_greyMaterialName);
    }
}
