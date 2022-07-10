using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : ChamberProp
{

    public LineRenderer lineRenderer;

    public GameObject OnHitPrefab;

    GameObject Hitobj;
    public enum Use { Right,Up }public Use use;

    public bool invertdir;

    Vector2 dir;
    public LayerMask hitmask;

    public enum OnStart {Nothing,On,Off }public OnStart onstart;

    public override void Initialize()
    {
        if (OnHitPrefab)
        {
            Hitobj = Instantiate(OnHitPrefab);
            return;
        }
    }

    public override void reset()
    {
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        switch (use)
        {
            case Use.Right:
                if (invertdir)
                {
                    dir = -transform.right;
                    break;
                }
                dir = transform.right;
                break;
            case Use.Up:
                if (invertdir)
                {
                    dir = -transform.up;
                    break;
                }
                dir = transform.up;
                break;
        }
        var hits = Physics2D.RaycastAll(transform.position, dir, Mathf.Infinity, hitmask);
        if (hits.Length > 0)
        {
            lineRenderer.SetPosition(1, hits[0].point);
            if (Hitobj) Hitobj.transform.position = new Vector3(lineRenderer.GetPosition(1).x, lineRenderer.GetPosition(1).y, 1);
            if (hits[0].transform.TryGetComponent<MapObject>(out var m) || hits[0].transform.TryGetComponent<Player>(out var p)) DestroyHit(hits[0].transform);
        }
    }

    private void OnDisable()
    {
        if(Hitobj)Hitobj.SetActive(false);
    }

    private void OnEnable()
    {
        if(Hitobj)Hitobj.SetActive(true);
    }

    public void DestroyHit(Transform hit)
    {
        StartCoroutine(destroy(hit));
    }

    IEnumerator destroy(Transform hit)
    {
        yield return new WaitForSeconds(.15f);
        if (hit.transform.GetComponent<ChamberProp>())
        {
            hit.transform.GetComponent<ChamberProp>().reset();
            yield return null;
        }
        else if (hit.transform == Player.Instance.transform)
        {
            Player.Instance.Alive = false;
            yield return null;
        }
    }
}
