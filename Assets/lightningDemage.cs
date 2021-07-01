using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningDemage : MonoBehaviour
{
    public int degat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
                    Debug.Log("enter");
            if (collision.tag == "Player")
            {
                Debug.Log("is player");
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().life -= degat;
            }
    }

}
