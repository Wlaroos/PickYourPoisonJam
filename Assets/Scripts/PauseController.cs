using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _panelRef;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            _panelRef.SetActive(true);
        }
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        _panelRef.SetActive(false);
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public IEnumerator StartActions()
    {
        yield return new WaitForSeconds(0.4f);

    }

    public void ButtonHover()
    {
        AudioManager.PlaySound("ButtonHover");
    }
}
