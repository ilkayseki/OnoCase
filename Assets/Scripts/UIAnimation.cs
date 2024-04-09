using System;
using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private float FadeOutDuration = 1f;
    [SerializeField] private float FadeInDuration = 1f;
    [SerializeField] private CanvasGroup[] canvasGroup;

    [SerializeField] private GameObject ErrorImage;
    
    private Tween fadeTween;

    private void OnEnable()
    {
        EventController.MachineChoseAction += FadeIn;
        EventController.BackChoseAction += FadeOut;
        EventController.keyStateAction += AlertAnim;
    }

    private void AlertAnim(KeyState key)
    {
        switch (key)
        {
            case KeyState.A:
                
                HandleAlertAnimation();
                
                break;
        }
    }

    private void HandleAlertAnimation()
    {
        if (ErrorImage.activeSelf)
        {
            ErrorImage.SetActive(false);
        }
        else
        {
            ErrorImage.SetActive(true);
        }
    }


    private void OnDisable()
    {
        EventController.MachineChoseAction -= FadeIn;
        EventController.BackChoseAction -= FadeOut;
    }

    private void Awake()
    {
        ToggleCanvas(true);
        Fade(0f, 0, () =>
        {
            foreach (var cg in canvasGroup)
            {
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
        });
    }

    public void FadeIn()
    {
        Fade(1f, FadeInDuration, () =>
        {
            ToggleCanvas(true);
            foreach (var cg in canvasGroup)
            {
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
        });
    }

    public void FadeOut()
    {
        Fade(0f, FadeOutDuration, () =>
        {
            foreach (var cg in canvasGroup)
            {
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
        });
    }

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        foreach (var cg in canvasGroup)
        {
            fadeTween = cg.DOFade(endValue, duration).OnComplete(onEnd);
        }
    }

    private void ToggleCanvas(bool setActive)
    {
        foreach (var cg in canvasGroup)
        {
            cg.gameObject.SetActive(setActive);
        }
    }
}