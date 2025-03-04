using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _firtstMenu;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _playMenu;

    public void PlayButton()
    {
        _firtstMenu.SetActive(false);
        _playMenu.SetActive(true);
    }

    public void SettingsButton()
    {
        _firtstMenu.SetActive(false);
        _settings.SetActive(true);
    }

    public void BackFromSettings()
    {
        _firtstMenu.SetActive(true);
        _settings.SetActive(false);
    }

    public void BackFromPlayButton()
    {
        _firtstMenu.SetActive(true);
        _playMenu.SetActive(false);
    }

    public void PlayNormal()
    {
        SceneManager.LoadScene("Normal");
    }

    public void PlayHard()
    {
        SceneManager.LoadScene("Hard");
    }

    public void PlayUnreal()
    {
        SceneManager.LoadScene("Unreal");
    }
    
}
