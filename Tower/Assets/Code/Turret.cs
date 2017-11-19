using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform target;
	private Enemy targetEnemy;
    
	public float range = 10f;
    
	public GameObject bulletPrefab;
	public float fireSpeed = 0.8f;
	private float fireCountdown = 0f;
    
	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float rotateSpeed = 8f;

	public Transform firePoint;

    public float value;

	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.7f);
	}

	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float min = Mathf.Infinity;
		GameObject closetEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < min)
			{
				min = distanceToEnemy;
				closetEnemy = enemy;
			}
		}

		if (closetEnemy != null && min <= range)
		{
			target = closetEnemy.transform;
			targetEnemy = closetEnemy.GetComponent<Enemy>();
		}

        else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		
		if (target == null)
			return;

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireSpeed;
		}

		fireCountdown -= Time.deltaTime;

	}

	void Shoot ()
	{
	    if (MenuManager.instance.WinLoseShowing)
	    {
	        return;
	    }

		GameObject shootBullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = shootBullet.GetComponent<Bullet>();

		if (bullet != null)
			bullet.Seek(target);
	}

    public void SellTurret()
    {
        Destroy(gameObject);
        MoneyManager.M.AddMoney(value * 0.9f);
    }

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
