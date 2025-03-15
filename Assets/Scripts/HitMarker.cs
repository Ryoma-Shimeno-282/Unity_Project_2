using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    public GameObject hitMarkerItself;
    public float existTimeSecond;

    // Start is called before the first frame update
    void Start()
    {
        DeleteThis();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeleteThis() {
        Destroy(hitMarkerItself, existTimeSecond);
    }

}
