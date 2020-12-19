﻿using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float spawnPointX = 0f;
    public float minSpawnHeight = 0f;
    public float maxSpawnHeight = 0f;
    public float spawnPointDepth = 60f;

    [Header("Settings")]
    [Range(0, 1)]
    public float timeBetweenBalls = 0.3f;
    public int amountToSpawn = 0;

    float timeSinceLastBall = Mathf.Infinity;
    int amountSpawned = 0;

    [Header("Pool settings")]
    public Ball ballPrefab = null;
    public Material prefabMaterial = null;
    public Transform pool = null;
    public int amountInPool = 30;
    public float bouncheVolume = 1;

    Player player = null;

    private void Start()
    {
        PopulatePool();
        player = FindObjectOfType<Player>();
    }

    private void PopulatePool()
    {
        for (var i = 0; i < amountInPool; i++)
        {
            var ball = Instantiate(ballPrefab, pool);
            ball.GetComponent<Renderer>().material = prefabMaterial;
            ball.GetComponent<TrailRenderer>().startColor = prefabMaterial.color;
            ball.GetComponent<TrailRenderer>().endColor = prefabMaterial.color;
            ball.GetComponent<AudioSource>().volume = bouncheVolume;
        }
    }

    private void Update()
    {
        timeSinceLastBall += Time.deltaTime;
        if (timeSinceLastBall > timeBetweenBalls)
            SpawnBall();
    }

    private void SpawnBall()
    {
        if (HaveAllSpawned()) return;
        if (player.playerState == PlayerState.Down) return;

        foreach (Transform ball in pool)
        {
            if (ball.gameObject.activeInHierarchy == true)
                continue;
            ball.position = GetNewSpawnPosition();
            ball.gameObject.SetActive(true);
            ball.GetComponent<Ball>().Launch();
            timeSinceLastBall = 0;
            amountSpawned++;
            break;
        }
    }

    public void DisableActiveBalls()
    {
        foreach (Transform ball in pool)
        {
            if (ball.gameObject.activeInHierarchy == false) continue;
            ball.gameObject.SetActive(false);
        }
    }

    public void IncreaseAmountTo(int amount) 
    {
        amountToSpawn = amount;
    }

    public void IncreaseSpeed()
    {
        foreach (Transform ball in pool)
        {
            ball.GetComponent<Ball>().IncreaseSpeed();
        }
    }

    private bool HaveAllSpawned()
    {
        return amountSpawned == amountToSpawn;
    }

    private Vector3 GetNewSpawnPosition()
    {
        var x = Random.Range(-spawnPointX, spawnPointX);
        var y = Random.Range(minSpawnHeight, maxSpawnHeight);
        return new Vector3(x, y, spawnPointDepth);
    }
}
