using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    [Header("Amount of audio to play:audiolength/clipduration")][Range(1,5)]public int clipduration;
    private void Start()
    {
        StartCoroutine(DestroyOnPlay());
    }

    IEnumerator DestroyOnPlay()
    {
        AudioSource audio = GetComponent<AudioSource>();

        var wait = audio.clip.length / clipduration;
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }
}
