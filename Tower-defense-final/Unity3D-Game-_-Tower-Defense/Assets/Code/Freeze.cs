using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class Freeze : MonoBehaviour
{
    private Turret turret;
    public LineRenderer lineRenderer;
    public float slowFactor = 0.5f;
    public ParticleSystem iceBeam;

    public int damageOverTime = 300;

    void Start()
    {
        turret = GetComponent<Turret>();
    }
    void IceBeam()
    {
        turret.targetEnemy.TakeDamage(damageOverTime * Time.deltaTime * 5);
        turret.targetEnemy.Slow(slowFactor);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            iceBeam.Play();
        }

        lineRenderer.SetPosition(0, turret.firePoint.position);
        lineRenderer.SetPosition(1, turret.target.position);

        Vector3 dir = turret.firePoint.position - turret.target.position;

        iceBeam.transform.position = turret.target.position + dir.normalized * 0.2f;

        iceBeam.transform.rotation = Quaternion.LookRotation(dir);


    }
}
