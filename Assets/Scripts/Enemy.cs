using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ScoreBoard scoreBoard;

    [SerializeField] GameObject DeathEnemyFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int hits = 2;

    private void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject fx = Instantiate(DeathEnemyFX, transform.position, Quaternion.identity);
        hits--;
        if (hits < 0)
        {
            killEnemy(fx);
        }
    }

    private void killEnemy(GameObject fx)
    {
        fx.transform.parent = parent;
        scoreBoard.ScoreHit(scorePerHit);
        Destroy(gameObject);
    }
}
