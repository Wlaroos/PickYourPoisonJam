using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WinLossController : MonoBehaviour
{
    public GameObject WtitleCard;
    public GameObject Wtitle;
    public GameObject Wdescription;
    public GameObject Winstruction;
    public GameObject Wbutton1;
    public GameObject Wbutton2;
    public GameObject Wtext1;
    public GameObject Wtext2;

    public GameObject LtitleCard;
    public GameObject Ltitle;
    public GameObject Ldescription;
    public GameObject Linstruction;
    public GameObject Lbutton1;
    public GameObject Lbutton2;
    public GameObject Ltext1;
    public GameObject Ltext2;

    public GameObject flashImage;

    public void RestartButton()
    {
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

    private IEnumerator Win()
    {
        GameObject.FindObjectOfType<PlayerMovement>().allowInput = false;
        if (GameObject.FindObjectOfType<PlayerWeapon>() != null)
            GameObject.FindObjectOfType<PlayerWeapon>().allowInput = false;
        if (GameObject.FindObjectOfType<LabyrinthWeapon>() != null)
            GameObject.FindObjectOfType<LabyrinthWeapon>().allowInput = false;

        Cursor.visible = true;

        flashImage.GetComponent<Animator>().Play("Flash", -1, 0);
        WtitleCard.SetActive(true);
        yield return new WaitForSeconds(1f);
        Wtitle.GetComponent<Animator>().Play("CardIn");
        yield return new WaitForSeconds(0.6f);
        Wdescription.GetComponent<Animator>().Play("CardIn");
        yield return new WaitForSeconds(0.6f);
        Winstruction.GetComponent<Animator>().Play("CardIn");
        yield return new WaitForSeconds(0.6f);
        Wbutton1.GetComponent<Animator>().Play("ButtonIn");
        Wbutton2.GetComponent<Animator>().Play("ButtonIn");
        Wtext1.GetComponent<Animator>().Play("CardIn");
        Wtext2.GetComponent<Animator>().Play("CardIn");
    }

    private IEnumerator Lose()
    {
        GameObject.FindObjectOfType<PlayerMovement>().allowInput = false;
        if (GameObject.FindObjectOfType<PlayerWeapon>() != null)
        GameObject.FindObjectOfType<PlayerWeapon>().allowInput = false;
        if (GameObject.FindObjectOfType<LabyrinthWeapon>() != null)
            GameObject.FindObjectOfType<LabyrinthWeapon>().allowInput = false;

        Cursor.visible = true;

        flashImage.GetComponent<Animator>().Play("Flash", -1, 0);
        LtitleCard.SetActive(true);
        yield return new WaitForSeconds(1f);
        Ltitle.GetComponent<Animator>().Play("CardIn");
        yield return new WaitForSeconds(0.6f);
        Ldescription.GetComponent<Animator>().Play("CardIn");
        yield return new WaitForSeconds(0.6f);
        Linstruction.GetComponent<Animator>().Play("CardIn");
        yield return new WaitForSeconds(0.6f);
        Lbutton1.GetComponent<Animator>().Play("ButtonIn");
        Lbutton2.GetComponent<Animator>().Play("ButtonIn");
        Ltext1.GetComponent<Animator>().Play("CardIn");
        Ltext2.GetComponent<Animator>().Play("CardIn");
    }

    public void WinEvent()
    {
        StartCoroutine(Win());
    }

    public void LoseEvent()
    {
        StartCoroutine(Lose());
    }
}
