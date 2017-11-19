using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

public class EnemyManager : MonoBehaviour
{
	private const float timeBetweenWaves = 5f; // Spawn every 3 seconds
	private const int MaxEnemyCount = 8; // Only 8 enemies allowed on screen at a time   
	private static Object _enemyPrefab;
	private float _lastspawn;
	private Transform _holder;
	private float countdown = 2f;
	private int waveIndex = 1;
   
	internal void Start ()
	{
	    _enemyPrefab = Resources.Load("Enemy");
		_holder = transform;
		Enemy.Manager = this;
	}
    
	internal void Update () {

        if (MenuManager.instance.WinLoseShowing)
        {
            return;
        }

		if ((Time.time - _lastspawn) < timeBetweenWaves) return;
		_lastspawn = Time.time;
		Spawn();
	}

	private void Spawn () {
		if (_holder.childCount >= MaxEnemyCount) { return; }
		Vector3 pos = new Vector3 (0, 1, 0);
		Quaternion rotation = new Quaternion (-45, 0, 0, 0);
		GameObject tmpEnemy = (GameObject) Object.Instantiate(_enemyPrefab, pos, rotation, _holder);
		tmpEnemy.tag = "Enemy";
	}

	/*
	void Update()
	{
		if (countdown <= 0f)
		{
			StartCoroutine(Spawn());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
	}

	IEnumerator Spawn()
	{
		waveIndex++;
		for (int i = 0; i < waveIndex; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(0.2f);
		}
		
	}

	void SpawnEnemy()
	{
		Vector3 pos = new Vector3 (0, 1, 0);
		Quaternion rotation = new Quaternion (-45, 0, 0, 0);
		GameObject tmpEnemy = (GameObject) Object.Instantiate(_enemyPrefab, pos, rotation, _holder);
	}
	*/

}
	
