using UnityEngine;
using System.Collections;

public class LiveboxController : MonoBehaviour {
    public Vector3 direction = new Vector3(0.0F,0.0F,0.0F);
    public float speed = 1.0F;

    void Update () {
        transform.position += direction * speed;
    }
}
