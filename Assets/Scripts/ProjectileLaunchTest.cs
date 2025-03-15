using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunchTest : MonoBehaviour
{
    public GameObject hitMarker;
    public float forceZ;

    Vector3 ProjectileCurrentPosition;
    Vector3 force;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Launch();
    }

    private void OnTriggerEnter(Collider other) {
        ProjectileCurrentPosition = transform.position;
        Instantiate(hitMarker, ProjectileCurrentPosition, Quaternion.identity);
    }


    void Launch() {
        if (Input.GetMouseButtonUp(0)) {
            
            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            force = new Vector3(0f, 0f, forceZ);
            rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }

}
