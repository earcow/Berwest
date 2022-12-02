using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FlipendoProjectile : MonoBehaviour
{
    [SerializeField] private float _explodeIntensityLimit = 30.0f;
    [SerializeField] private float _explodeTimeStep = 0.01f;
    [SerializeField] private float _explodeIntensityStep = 1.7f;
    [SerializeField] private float _explodeRangeStep = 0.2f;

    [SerializeField] private float _fadeIntensityStep = 0.5f;
    [SerializeField] private float _fadeIntensityTimeStep = 0.002f;

    private Rigidbody _projectileRigidbody;
    private MeshRenderer _projectileRender;
    private Collider _projectileCollider;
    private ParticleSystem _projectileMagicSparks;
    //private Light _projectileLight;

    private void OnEnable()
    {
        _projectileRigidbody = this.GetComponent<Rigidbody>();
        _projectileRender = this.GetComponent<MeshRenderer>();
        _projectileCollider = this.GetComponent<Collider>();
        _projectileMagicSparks = this.GetComponent<ParticleSystem>();
        //_projectileLight = this.GetComponent<Light>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        _projectileRigidbody.isKinematic = true;
        _projectileRender.enabled = false;
        _projectileCollider.enabled = false;
        _projectileMagicSparks.Stop();

        Destroy(gameObject, 1.1f);
    }

}
