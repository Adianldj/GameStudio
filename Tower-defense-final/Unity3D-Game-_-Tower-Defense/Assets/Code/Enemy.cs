using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed;
    public float _enemySpeed;
	public float _health = 400f;
	public float _currentHealth;
    private Transform _nextpoint;
    private int pointIndex = 0;
    private Enemy _nearest;
    public bool beingShocked;
    public LineRenderer shockBeam;

	private int hitTimes = 0;

	public static EnemyManager Manager;
    
	void Start ()
	{
	    _nextpoint = Pathfinding.points[0];
	    _currentHealth = _health;
	    beingShocked = false;
	    shockBeam.enabled = false;
	}
	
	void Update ()
	{
	    if (MenuManager.instance.WinLoseShowing)
	    {
	        _enemySpeed = 0f;
	        return;
	    }

	    if (_currentHealth <= 0)
	    {
	        Destroy(gameObject);
	        shockBeam.enabled = false;
	        FindObjectOfType<EnemyManager>()._enemiesLeft--;
	        MoneyManager.M.AddMoney(_health);
	        if (FindObjectOfType<EnemyManager>()._enemiesLeft <= 0)
	        {
	            MenuManager.instance.PlayerWins();
	        }
	    }

        Vector3 direction = _nextpoint.position - transform.position;
        transform.Translate(direction.normalized * _enemySpeed * Time.deltaTime, Space.World);

	    if (Vector3.Distance(transform.position, _nextpoint.position) <= 0.1f)
	    {
	        GetNextPoint();
	    }

	    


        if (beingShocked)
	    {
	        if (_nearest != null)
	        {
	            float dist = Vector3.Distance(transform.position, _nearest.transform.position);
	            if (dist > 7)
	            {
	                shockBeam.enabled = false;
	                _nearest = null;
	                return;
	            }

	            else
	            {
	                shockBeam.enabled = true;
	                shockBeam.SetPosition(0, transform.position);
	                shockBeam.SetPosition(1, _nearest.transform.position);
	                return;
	            }
	        }

	        Enemy[] neighbors = FindObjectsOfType<Enemy>();

	        if (neighbors != null)
	        {
	            foreach (Enemy e in neighbors)
	            {
	                float distance = Vector3.Distance(e.transform.position, transform.position);
	                if (distance <= 7 && !e.beingShocked 
                        && EnemyManager.instance.beingShocked.Count < 3)
	                {
	                    e.Shocked();
	                    _nearest = e;
	                }
	            }
	        }
        }

	    if (!beingShocked)
	    {
	        shockBeam.enabled = false;
	    }
        
	}

    void GetNextPoint()
    {
        if (pointIndex >= Pathfinding.points.Length - 1)
        {
            Destroy(gameObject);
			FindObjectOfType<EnemyManager>()._enemiesLeft--;
			if (FindObjectOfType<EnemyManager>()._enemiesLeft <= 0)
			{
				MenuManager.instance.PlayerWins();
			}
        }

        else
        {
            pointIndex++;
            _nextpoint = Pathfinding.points[pointIndex];
        }
    }

	public void TakeDamage (float amount)
	{
		hitTimes++;


		_currentHealth -= amount;
        
	}

    public void Shocked()
    {
        beingShocked = true;
        EnemyManager.instance.beingShocked.Add(this);
        Invoke("StopShock", 0.5f);
    }

    private void StopShock()
    {
        beingShocked = false;
        EnemyManager.instance.beingShocked.Remove(this);
    }

    public void Slow (float slowFactor)
    {
        _enemySpeed = _enemySpeed * (1f - slowFactor);
    }
}
