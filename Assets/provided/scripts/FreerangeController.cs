using UnityEngine;
using System.Collections;

public class FreerangeController : MonoBehaviour {

    public string perimeter_tag;
    public GameObject player;
    public float speed = 1.0F;
    private Rigidbody m_rb;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
    }

    void move_to_player()
    {
        // TODO 441: update m_rb.velocity to be direction * speed
        //      direction is the direction to the player GameObject
        //      speed is the provided speed float value
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(perimeter_tag))
        {
            move_to_player();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(perimeter_tag))
        {
            move_to_player();
        }
    }
}
