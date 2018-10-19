using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody m_rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        /* get user input to apply a horizontal
         * force to our player object */

        float movement = Input.GetAxis("Horizontal");
        //add force
        m_rb.AddForce(new Vector3(movement, 0.0F, 0.0F));

    }
}
