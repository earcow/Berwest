using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorBehavior : MonoBehaviour
{
    private Vector3 _characterPosition;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CollectableObject>() != null)
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Collider>().enabled = false;

            StartCoroutine(CollectAnimation(other.gameObject));
        }
    }

    private IEnumerator CollectAnimation(GameObject collectableGO)
    {
        _characterPosition = gameObject.GetComponent<Transform>().position;
        var collectableRigidbody = collectableGO.GetComponent<Rigidbody>();

        while (Vector3.Distance(collectableRigidbody.position, _characterPosition) > 0.01f)
        {
            collectableRigidbody.MovePosition(_characterPosition);

            //yield return null;
            yield return new WaitForEndOfFrame();
        }

        Destroy(collectableGO, 0.1f);
    }

}
