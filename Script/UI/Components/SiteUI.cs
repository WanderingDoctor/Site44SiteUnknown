using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiteUI : MonoBehaviour
{
    [Serializable]public enum Type {Fade,Scale}
    public Type anim;

    public float Duration;

    RectTransform rect;
    Image image;
    Color OriginC;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        OriginC = image.color;
    }

    private void OnEnable()
    {
        if (anim == Type.Fade)
        {
            StartCoroutine(FadingIn());
            return;
        }
        StartCoroutine(ScalingOut());
    }

    IEnumerator FadingIn()
    {
        Color start = new Color(0, 0, 0, 0);
        Color end = OriginC;
        float t = 0;
        while (t < Duration)
        {
            image.color = Color.Lerp(start, end, t / Duration);
            t += Time.deltaTime;
            yield return null;
        }
        image.color = OriginC;
    }

    IEnumerator ScalingOut()
    {
        Vector3 start = new Vector3(0, 0, 1);
        Vector3 end = Vector3.one;
        float t = 0;
        while (t < Duration)
        {
            rect.localScale = Vector3.Lerp(start, end, t / Duration);
            t += Time.deltaTime;
            yield return null;
        }
        rect.localScale = Vector3.one;
    }
}
