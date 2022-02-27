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

    [SerializeField]
    private GameObject _nose;

    [SerializeField]
    private int _colorLayer;

    [SerializeField]
    private PlaySound _soundHandler;

    [SerializeField]
    private AudioClip _clip;

    [SerializeField]
    private ParticleSystem[] _particles;

    [SerializeField]
    private GameObject[] _colliders;

    [SerializeField]
    private MeshRenderer[] _ringMeshRenderer;

    [SerializeField]
    private GameObject[] _ringLight;

    [SerializeField]
    private Material _nextMaterial;

    public bool ColorArrived;

    private MeshRenderer _platformMeshRenderer;

    private void Awake()
    {
        _platformMeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _colorLayer)
        {
            _platformMeshRenderer.material = other.GetComponent<MeshRenderer>().material;
            //Debug.Log(other.gameObject.name + " has reached the destination");

            if (other.gameObject.name == _characterName)
            {
                other.gameObject.GetComponent<PlayerLocomotion>().enabled = false;
                other.gameObject.GetComponent<PlayerManager>().enabled = false;
                other.gameObject.GetComponent<InputManager>().enabled = false;
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                _nose.gameObject.SetActive(false);

                _nextCharacter.SetActive(true);
                _cameraManager.TargetTransform = _nextCharacter.transform;
                _cameraManager.InputManager = _nextCharacter.GetComponent<InputManager>();

                _soundHandler.StopCurrentAudio();
                _soundHandler.PlayAudio(_clip);

                foreach (ParticleSystem particles in _particles)
                {
                    particles.Play();
                }

                foreach(GameObject collider in _colliders)
                {
                    collider.SetActive(true);
                }

                foreach (MeshRenderer renderer in _ringMeshRenderer)
                {
                    renderer.material = _nextMaterial;
                }

                foreach(GameObject light in _ringLight)
                {
                    light.SetActive(true);
                }

                ColorArrived = true;
            }
        }
    }
}
