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

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(FadeLightExplode());
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<MeshCollider>().enabled = false;
        this.GetComponent<ParticleSystem>().Stop();

    }

    IEnumerator FadeLightExplode()
    {
        var light = this.GetComponent<Light>();
        while (light.intensity < _explodeIntensityLimit)
        {
            light.intensity += _explodeIntensityStep;
            light.range += _explodeRangeStep;
            yield return new WaitForSeconds(_explodeTimeStep);
        }
        StartCoroutine(FadeLightIntensive());
    }
    IEnumerator FadeLightIntensive()
    {
        var light = this.GetComponent<Light>();
        while (light.intensity > 0.0f)
        {
            light.intensity -= _fadeIntensityStep;
            yield return new WaitForSeconds(_fadeIntensityTimeStep);
        }
    }
}
