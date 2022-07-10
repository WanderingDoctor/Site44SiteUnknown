using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AntiGravPad : MonoBehaviour
{
    public float Duration;

    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "LevelProp")
        {
            StartCoroutine(ChangeGrav(collision.GetComponent<Rigidbody2D>()));
        }
    }

    IEnumerator ChangeGrav(Rigidbody2D target)
    {
        var rb2dgrav = target.gravityScale;
        target.gravityScale = 0.0095f;
        target.velocity = Vector2.one * speed * transform.up;
        yield return new WaitForSecondsRealtime(Duration);
        target.gravityScale = rb2dgrav;
    }
}
