using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinePopUpController : MonoBehaviour
{
    
    private CanvasGroup canvasGroup;
    private Tween fadeTween;

    [SerializeField ] private GameObject myPopUP; // Image'in RectTransform bileşeni
    [SerializeField ] private Vector3 TransformOffSett=new Vector3(0,00,0); 
    
    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        canvasGroup = myPopUP.GetComponent<CanvasGroup>();
        
        myPopUP.SetActive(true);
    }
    private void OnEnable()
    {
        EventController.MachineChoseAction += FadeOut;
        EventController.BackChoseAction += FadeIn;
    }

    private void OnDisable()
    {
        EventController.MachineChoseAction -= FadeOut;
        EventController.BackChoseAction -= FadeIn;
    }

    void Start()
    {
        SetPopUpPosition();
    }

    [SerializeField] private float FadeOutDuration=1;
    [SerializeField] private float FadeInDuration=1;
    public void FadeIn()
    {
        Fade(1f, FadeInDuration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    public void FadeOut()
    {
        Fade(0f, FadeOutDuration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
    
    private void SetPopUpPosition()
    {
        // Popup objesini sabit bir ofsetle hedeflediğiniz noktaya konumlandırma
        Vector3 targetPosition = transform.position + TransformOffSett; // Örneğin, yükseklik için 0.5f ofset ekleyin
        myPopUP.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
    }
}
