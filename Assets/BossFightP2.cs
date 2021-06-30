using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightP2 : MonoBehaviour
{
    public GameObject[] targetHorizontal = new GameObject[2];
    public GameObject[] targetVertical = new GameObject[2];

    public GameObject eclairePrefab;
    public GameObject dragonPrefab;

    public GameObject flash;

    [Header("Part")]
    public part[] parts;

    bool action;
    bool fall = true;
    bool passe = true;

    float time = 0;
    float lerp = 0;

    int spawnPoint;
    int nFall;
    public int dragonPast;
    int intPart = 0;

    Vector2 targetDrack1;
    Vector2 targetDrack2;

    GameObject drack = null;

    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(parts[intPart].timeEventMin, parts[intPart].timeEventMax + 1);
        flash.GetComponent<Renderer>().material.color = new Vector4(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            action = true;
            nFall = Random.Range(parts[intPart].lightningMin, parts[intPart].lightningMax + 1);
            //dragonPast = Random.Range(parts[intPart].dragonMin, parts[intPart].dragonMax + 1);
        }

        if(action)
        {
            if (Random.Range(0, 2) == 0)
            {
                for (int i = 0; i < nFall; i++)
                    Storm();
            }
            else
            {
                Dragon();
            }

            if(dragonPast==0 || fall == true)
            {
                if (parts[intPart].eventByVague > 0)
                    parts[intPart].eventByVague--;
                else if (intPart < parts.Length-1)
                {
                    intPart++;
                    StartCoroutine(Flash());
                }
                
                action = false;
                fall = false;
                time = Random.Range(parts[intPart].timeEventMin, parts[intPart].timeEventMax + 1);
            }


        }

        if (drack != null)
            DragonMove(intPart);

    }

    public void Storm()
    {
        Debug.Log("lightning");
        float fallPont = Random.Range(targetHorizontal[0].transform.position.x+1, targetHorizontal[1].transform.position.x -1);
        GameObject.Instantiate(eclairePrefab, new Vector2(fallPont, 4.35f),Quaternion.identity);
        fall = true;
    }

    public void Dragon()
    {
        Debug.Log("dragon");
        
        float pastPont1 = Random.Range(targetVertical[0].transform.position.y - 1, targetVertical[1].transform.position.y + 1);
        float pastPont2 = Random.Range(targetVertical[0].transform.position.y - 1, targetVertical[1].transform.position.y + 1);
        targetDrack1 = new Vector2(targetHorizontal[0].transform.position.x - 2, pastPont1);
        targetDrack2 = new Vector2(targetHorizontal[1].transform.position.x + 2, pastPont2);

        if (dragonPast == 0)
        {
            spawnPoint = Random.Range(0, 2);
            dragonPast = Random.Range(parts[intPart].dragonMin, parts[intPart].dragonMax + 1);

            if (spawnPoint == 0)
                drack = GameObject.Instantiate(dragonPrefab, targetDrack2, Quaternion.identity);
            else
            {
                drack = GameObject.Instantiate(dragonPrefab, targetDrack1, Quaternion.identity);
                drack.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else if (spawnPoint == 0)
        {
            spawnPoint = 1;
            drack.transform.eulerAngles = new Vector3(0, drack.transform.eulerAngles.y - 180, 0);
        }
        else
        {
            spawnPoint = 0;
            drack.transform.eulerAngles = new Vector3(0, drack.transform.eulerAngles.y - 180, 0);
        }

        dragonPast--;

    }

    public void DragonMove(int i)
    {
        if(spawnPoint == 0)
            drack.transform.position = Vector2.Lerp(targetDrack2, targetDrack1, lerp);
        else
            drack.transform.position = Vector2.Lerp(targetDrack1, targetDrack2, lerp);

        lerp += Time.deltaTime*parts[i].speed;

        if (lerp >= 1)
        {
            lerp = 0;
            
            if (dragonPast > 0)
                Dragon();
            else
            {
                drack = null;
                GameObject.Destroy(GameObject.Find("dragon(Clone)"));
            }
        }

    }

    [System.Serializable]
    public struct part
    {
        public int lightningMin;
        public int lightningMax;
        public int dragonMin;
        public int dragonMax;
        public float speed;
        public float timeEventMin;
        public float timeEventMax;
        public int eventByVague;
    }

    public IEnumerator Flash()
    {
        float light = 0;
        while(light<255)
        {
            light += Time.deltaTime * 255;
            flash.GetComponent<Renderer>().material.color = new Vector4(255, 255, 255, light);
            Debug.Log("up");
        }
        while(light>0)
        {
            light -= Time.deltaTime * 255;
            flash.GetComponent<Renderer>().material.color = new Vector4(255, 255, 255, light);
            Debug.Log("down");
        }
        yield return null;
    }
}
