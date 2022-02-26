using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _startMenu;

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _continueButton;

    [SerializeField]
    private AudioSource _musicAudio;

    private void Awake()
    {
        Time.timeScale = 0;
        _startMenu.gameObject.SetActive(true);
        _pauseMenu.gameObject.SetActive(false);
        _musicAudio.Pause();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        _startMenu.SetActive(false);
        _musicAudio.Play();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);

        _musicAudio.Pause();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_continueButton);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);

        _musicAudio.Play();
    }
}
