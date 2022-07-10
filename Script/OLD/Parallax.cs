using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Serializable]public struct ParallaxObject
    {
        public SpriteRenderer renderer;
        [HideInInspector]public float length, startpos;
        public float ParallaxSpeed;
    }
    public List<ParallaxObject> ParallaxObjects;

    private void Start()
    {
        ParallaxObjects.ForEach(a =>
        {
            a.startpos = a.renderer.transform.position.x;
            a.length = a.renderer.bounds.size.x;
        });
    }

    private void FixedUpdate()
    {
        ParallaxObjects.ForEach(obj =>
        {
            ParallaxCheck(obj);
        });
    }

    void ParallaxCheck(ParallaxObject obj)
    {
        float temp = (transform.position.x * (1 - obj.ParallaxSpeed));
        float dist = (transform.position.x * obj.ParallaxSpeed);

        obj.renderer.transform.position = new Vector3(obj.startpos + dist, obj.renderer.transform.position.y, obj.renderer.transform.position.z);
        if (temp > obj.startpos + obj.length)
        {
            obj.startpos += obj.length;
            return;
        }
        if(temp > obj.startpos - obj.length)
        {
            obj.startpos -= obj.length;
        }
    }
}
