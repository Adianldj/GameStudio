using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	public float speed = 55f;

	private float damage = 20f;

    public bool shockBullets;

	public void Seek (Transform _target)
	{
		target = _target;
	}

	void Update () {

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);

	}

	void HitTarget ()
	{
		Destroy (gameObject);
		Damage(target);
	}

	void Damage (Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy>();

		if (e != null && !shockBullets)
		{
			e.TakeDamage(damage);
		}

        else if (e != null && shockBullets)
		{
		    e.Shocked();
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 0f);
	}
}
