using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AvadaKedavraProjectile : MonoBehaviour
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
    private Light _projectileLight;

    private void OnEnable()
    {
        _projectileRigidbody = this.GetComponent<Rigidbody>();
        _projectileRender = this.GetComponent<MeshRenderer>();
        _projectileCollider = this.GetComponent<Collider>();
        _projectileMagicSparks = this.GetComponent<ParticleSystem>();
        _projectileLight = this.GetComponent<Light>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        _projectileRigidbody.isKinematic = true;
        _projectileRender.enabled = false;
        _projectileCollider.enabled = false;
        _projectileMagicSparks.Stop();

         StartCoroutine(FadeLightExplode());

        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.contacts[i].otherCollider.TryGetComponent<Character>(out Character character))
            {
                Debug.Log("Попался!");
                character.GetComponent<Health>().DecreaseAt(gameObject, 1000000);
            }
        }

        //Debug.Log(collision.gameObject.GetComponentInChildren<Character>());
        //Debug.Log(collision.gameObject.GetComponentInParent<Character>());
    }

    IEnumerator FadeLightExplode()
    {
        while (_projectileLight.intensity < _explodeIntensityLimit)
        {
            yield return new WaitForSeconds(_explodeTimeStep);
            _projectileLight.range += _explodeRangeStep;
            _projectileLight.intensity += _explodeIntensityStep;

            //Debug.Log("intensity: " + _projectileLight.intensity + "; range: " + _projectileLight.range);
        }

        StartCoroutine(FadeLightIntensive());
    }
    IEnumerator FadeLightIntensive()
    {
        while (_projectileLight.intensity > 0.0f)
        {
            _projectileLight.intensity -= _fadeIntensityStep;
            yield return new WaitForSeconds(_fadeIntensityTimeStep);
        }
    }
}
