using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoisyObjectController : MonoBehaviour
{
    [SerializeField] private float noiseRadius = 5f;
    //[SerializeField] private float noiseDuration = 5f;
    //[SerializeField] private float noiseVolume = 1f;
    [SerializeField] private float noiseInterval = 1f;
    [SerializeField] private float exposureDuration = 0.1f;
    private bool isActivated = false;

    private MeshRenderer meshRenderer;
    public ParticleSystem noiseRing;
    public List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        //noiseRing.startSize = noiseRadius;
        StartCoroutine(MakeNoise());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MakeNoise()
    {
        while (true)
        {
            noiseRing.Play();
            yield return new WaitForSeconds(noiseInterval);
        }
    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log("Noise ring collided with " + other.name);
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("Player collided with noise ring");
    //        TemporarilyActivate();
    //    }
    //}

    public void TemporarilyActivate()
    {
        if (!isActivated)
        {
            isActivated = true;
            StartCoroutine(ActivateForDuration());
        }
    }

    private IEnumerator ActivateForDuration()
    {
        meshRenderer.enabled = true;
        yield return new WaitForSeconds(exposureDuration);
        meshRenderer.enabled = false;
        isActivated = false;
    }
}
