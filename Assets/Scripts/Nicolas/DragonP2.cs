using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonP2 : MonoBehaviour
{
    public int degat;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (collision.tag == "Player")
        {
            Debug.Log("is player");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().life --;
        }
    }
}
