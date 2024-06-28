using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseController : MonoBehaviour
{
    [SerializeField] float pulseSpeed = 1.0f;
    [SerializeField] float maxRadius = 10.0f;
    private float currentRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRadius < maxRadius)
        {
            currentRadius += pulseSpeed * Time.deltaTime;
            transform.localScale = new Vector3(currentRadius, currentRadius, currentRadius);
        }
        else
        {
           Destroy(gameObject);
        }
    }
}
