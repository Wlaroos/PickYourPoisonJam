using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public void StartButton(RectTransform t){
        AudioManager.PlaySound("ButtonSelect");
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(0.3f);
        mySequence.Append(t.DOAnchorPos(Vector2.up*-1080,0.6f,false).SetEase(Ease.InOutQuint));
    }

    public void QuitButton(){
        Application.Quit();
    }

    public IEnumerator StartActions(){
        yield return new WaitForSeconds(0.4f);

    }

    public void ButtonHover(){
        AudioManager.PlaySound("ButtonHover");
    }
}
