﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public Trail TrailController;
    public BoxCollider2D TapCollider;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird _shotBird;
    private bool _isGameEnded = false;

    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;
        if (_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
            SlingShooter.InitiateBird(Birds[0]);
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Nextlevel(string nextLvl)
    {
        SceneManager.LoadScene(nextLvl);
    }
}