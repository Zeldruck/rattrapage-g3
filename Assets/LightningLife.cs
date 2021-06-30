using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningLife : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spot;
    public GameObject lightning;

    public float time = 0;
    float onOff = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time < 2)
        {
            if (time > onOff)
            {
                spot.SetActive(!spot.activeSelf);
                onOff += 0.25f;
            }
        }
        else
        {
            spot.SetActive(false);
            lightning.SetActive(true);

        }

        if (time > 2.75f)
        {
            GameObject.Destroy(prefab);
            //Debug.Log("j'ai juste envie de te faire chier");
        }

    }
}
