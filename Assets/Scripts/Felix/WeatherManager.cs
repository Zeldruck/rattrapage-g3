using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private float weatherTimer = 0f;
    private float lightningTimer = 0f;
    private float cameraTimer = 0f;
    private float cameraSpeed = 0f;
    private float cameraDirection = 1f;

    [Header("Weather")]
    public float weatherChangeTime;
    public GameObject cloudPrefab;
    public Vector2 lightningRangeTime;
    public Vector2 lightningSpawnRangeX;
    [Space]
    public ParticleSystem rainParticles;
    public Player player;
    [Space]
    public Vector2 wind;

    // Start is called before the first frame update
    void Start()
    {
        weatherTimer = weatherChangeTime;
        lightningTimer = lightningTimer = Random.Range(lightningRangeTime.x, lightningRangeTime.y); ;

        WindChangeDirection();
        cameraSpeed = Random.Range(0.2f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (weatherTimer > 0f)
            weatherTimer -= Time.deltaTime;
        else
        {
            weatherTimer = weatherChangeTime;
            WindChangeDirection();
        }

        if (lightningTimer > 0f)
            lightningTimer -= Time.deltaTime;
        else
        {
            lightningTimer = Random.Range(lightningRangeTime.x, lightningRangeTime.y);
            SpawnLightning();
        }

        if (cameraTimer >= 1f)
        {
            cameraTimer = 0f;
            cameraDirection *= -1f;
            cameraSpeed = Random.Range(0.1f, 0.25f);
        }
        else
        {
            cameraTimer += Time.deltaTime * cameraSpeed;
            Vector3 camPos = new Vector3(Mathf.Lerp(-0.4f * cameraDirection, 0.4f * cameraDirection, cameraTimer), 0f, Camera.main.transform.position.z); ;
            Camera.main.transform.position = camPos;
        }
    }

    private void WindChangeDirection()
    {
        // wind
        wind.x = Random.Range(-0.5f, 0.5f);
        wind.y = Random.Range(-6f, 1f);

        float nPos = Mathf.Lerp(-4f, 4f, -wind.x + 0.5f);
        rainParticles.transform.position = new Vector3(nPos, rainParticles.transform.position.y, rainParticles.transform.position.z);
        rainParticles.transform.rotation = Quaternion.Euler(0f, 0f, wind.x * 100f);

        if (player != null)
            player.windForce = wind;
    }

    private void SpawnLightning()
    {
        Vector2 spawnFlash;
        spawnFlash.x = Random.Range(-lightningSpawnRangeX.x, lightningSpawnRangeX.x);
        spawnFlash.y = lightningSpawnRangeX.y;

        GameObject cloud = Instantiate(cloudPrefab, spawnFlash, Quaternion.identity);
        Destroy(cloud, 3f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0f, lightningSpawnRangeX.y, 0f), new Vector3(lightningSpawnRangeX.x, 1f, 0f));
    }
}
