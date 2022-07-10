using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HeatObject : ChamberProp
{
    public enum Mode { None,Cooler,Heater}public Mode mode;

    public float Heat;

    public GameObject HeatEffect;

    [Range(0,1)]public float XChange;

    public float SizeIncrease;

    public float MaxHeatThresh;

    public float MinHeatThresh;

    [Range(0,1)]public float heatdecay;

    [Range(0,1)]public float SizeChange;

    public bool Active;

    public enum Onstart { Nothing,On,Off}public Onstart onstart;

    Vector2 startsize;
    Vector3 startscale;

    public override void reset()
    {
    }

    public override void Initialize()
    {
        if(mode == Mode.None)
        {
            startsize = GetComponent<SpriteRenderer>().size;
            startscale = transform.localScale;
        }
        switch (onstart)
        {
            case Onstart.Off:
                Active = false;
                break;
            case Onstart.On:
                Active = true;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (mode == Mode.None)
        {
            var rend = transform.GetComponent<SpriteRenderer>();
            if (Heat > MaxHeatThresh)
            {
                Sizechange(1);
                Heat = Mathf.Lerp(Heat,0,heatdecay);
                return;
            }
            if (Heat < MinHeatThresh)
            {
                Sizechange(-1);
                Heat = Mathf.Lerp(Heat, 0, heatdecay);
                return;
            }
            Sizechange(0);
            Heat = Mathf.Lerp(Heat, 0, heatdecay);
            return;
        }
        if(HeatEffect && Active && mode == Mode.Heater || mode == Mode.Cooler)
        {
            HeatEffect.SetActive(true);
            return;
        }
        HeatEffect.SetActive(false);
    }

    void Sizechange(int change)
    {
        var schange = new Vector3(0 + XChange, 1 - XChange) * change;
        transform.localScale = Vector3.Lerp(transform.localScale, startscale+(schange*SizeIncrease), SizeChange);
        return;
    }

    public void HeatUp()=>Heat += .5f;
    public void CoolDown() => Heat -= .5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.GetComponent<HeatObject>()) return;
        var obj = collision.GetComponent<HeatObject>();
        switch (mode)
        {
            case Mode.Cooler:
                if (Active && mode == Mode.Cooler && obj.mode == Mode.None) obj.CoolDown();
                break;
            case Mode.Heater:
                if (Active && mode == Mode.Heater && obj.mode == Mode.None) obj.HeatUp();
                break;
            default:
                break;
        }
    }
}
