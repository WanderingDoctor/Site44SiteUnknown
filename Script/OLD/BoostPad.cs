using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoostPad : MonoBehaviour
{
    public Vector2 ForcePlayer, ForceProp;

    public float Delay;

    Player player;

    private void Start() => player = Player.Instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "LevelProp")
        {
            StartCoroutine(Launch(collision.GetComponent<Rigidbody2D>(), ForceProp));
            return;
        } else if (collision.gameObject == player.gameObject)
        {
            StartCoroutine(Launch(collision.GetComponent<Rigidbody2D>(), ForcePlayer));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            StopCoroutine(Launch(collision.GetComponent<Rigidbody2D>(), ForcePlayer));
        }
    }

    IEnumerator Launch(Rigidbody2D obj,Vector2 Force)
    {
        yield return new WaitForSeconds(Delay);
        obj.velocity = Vector2.zero;
        obj.AddForce(Force, ForceMode2D.Impulse);
    }
}
