using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravElevator : MonoBehaviour
{
    Player player;

    public float force;

    [Range(-1, 1)] public float dir;

    private void Start()=>player = Player.Instance;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Character")
        {
            var rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            var pos = rb2d.position;
            pos.y = Mathf.Lerp(pos.y,transform.position.y, .1f);
            rb2d.position = new Vector2(rb2d.position.x, pos.y);
            rb2d.gravityScale = 0;
            rb2d.AddForce((transform.up*dir) * force);
            return;
        }
        var _ToMove = new Vector2(dir, 0);
        var _rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
        var _pos = _rb2d.position; 
        _pos.y = Mathf.Lerp(_pos.y, transform.position.y, .1f); 
        _rb2d.position = new Vector2(_rb2d.position.x, _pos.y);
        _rb2d.gravityScale = 0;
        _rb2d.AddForce(_ToMove * force);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MapObject>())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = collision.gameObject.GetComponent<MapObject>().StartingGrav;
            return;
        }
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
