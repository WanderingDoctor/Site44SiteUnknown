using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] List<string> GroundTags;

    Player player;

    private void Start() => player = Player.Instance;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GroundTags.Contains(collision.transform.tag) == true) player.IsGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GroundTags.Contains(collision.transform.tag) == true) player.IsGrounded = false;
    }
}
