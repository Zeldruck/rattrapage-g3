using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private float weatherTimer = 0f;
    private float lightningTimer = 0f;

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

        WindChangeDirection();
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
    }

    private void WindChangeDirection()
    {
        // wind
        wind.x = Random.Range(-0.5f, 0.5f);
        wind.y = Random.Range(-6f, 1f);

        float nPos = Mathf.Lerp(-4f, 4f, -wind.x + 0.5f);
        rainParticles.transform.position = new Vector3(nPos, rainParticles.transform.position.y, rainParticles.transform.position.z);
        rainParticles.transform.rotation = Quaternion.Euler(0f, 0f, wind.x * 100f);

        player.windForce = wind;
    }

    private void SpawnLightning()
    {
        Vector2 spawnFlash;
        spawnFlash.x = Random.Range(-lightningSpawnRangeX.x, lightningSpawnRangeX.x);
        spawnFlash.y = lightningSpawnRangeX.y;

        GameObject cloud = Instantiate(cloudPrefab, spawnFlash, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0f, lightningSpawnRangeX.y, 0f), new Vector3(lightningSpawnRangeX.x, 1f, 0f));
    }
}
