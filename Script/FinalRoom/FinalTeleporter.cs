using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalTeleporter : MonoBehaviour
{
    public float delay;

    public string Scene;

    Player player;

    void Start()=>player = Player.Instance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.gameObject == player.gameObject)
         {
            StartCoroutine(PrepareToLoad());
         }
    }

    IEnumerator PrepareToLoad()
    {
        if(!player.NoControl)player.NoControl = true;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = transform.position+Vector3.up*2;
        yield return new WaitForSeconds(delay);
        try
        {
            SceneManager.LoadScene(Scene, LoadSceneMode.Single);
        }
        catch
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
