using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyColor : MonoBehaviour
{
    [SerializeField]
    private int _firstOtherColorLayer;

    [SerializeField]
    private int _secondOtherColorLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _firstOtherColorLayer || other.gameObject.layer == _secondOtherColorLayer)
        {
            Destroy(other.gameObject);
            Invoke("ReloadScene", 1f);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
