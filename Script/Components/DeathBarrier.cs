using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathBarrier : MonoBehaviour
{
    Player player;

    public bool PlayerOnly;

    public bool PropOnly;

    void Start()=>player = Player.Instance;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerOnly)
        {
            if (collision.gameObject == player.gameObject)
            {
                player.Alive = false;
                return;
            }
            return;
        }
        if (PropOnly)
        {
            if (collision.GetComponent<MapObject>())
            {
                collision.GetComponent<MapObject>().reset();
                return;
            }
            return;
        }
        collision.GetComponent<MapObject>()?.reset();
        if (collision.gameObject == player.gameObject) player.Alive = false;
    }
}
