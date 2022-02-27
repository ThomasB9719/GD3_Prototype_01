using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideColor : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _nextPortalsParticles;

    [SerializeField]
    private MeshRenderer[] _nextPortal;

    [SerializeField]
    private SphereCollider[] _nextPortalCollider;

    [SerializeField]
    private GameObject[] _nextPortalLight;

    [SerializeField]
    private PlaySound _soundSource;

    [SerializeField]
    private AudioClip _portalClip;

    [SerializeField]
    private int _colorLayer;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _colorLayer)
        {
            _nextPortalsParticles.Play();

            foreach (MeshRenderer renderer in _nextPortal)
            {
                renderer.material = this.GetComponent<MeshRenderer>().material;
            }
            foreach (Collider collider in _nextPortalCollider)
            {
                collider.enabled = false;
            }
            foreach (GameObject light in _nextPortalLight)
            {
                light.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _colorLayer)
        {
            _soundSource.StopCurrentAudio();
            _soundSource.PlayAudio(_portalClip);
        }
    }
}
