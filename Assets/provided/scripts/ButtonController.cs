using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

    // --- How to make a "Trap Door"
    // The math here is pretty simple, just set the scale in the X axis to however much
    // the trapdoor should shrink -- e.g. 0.5 will result in half of the trap door dissapearing.
    // Then you have to transform the platform half as far as you're scale changes it.
    // A good transformation_time is 1 second for most trapdoors.
    // Some examples:
    // 8 wide platform 1/4 trapdoor:  transform = {1,0,0} scale = {0.75,1,1}
    // 8 wide platform half trapdoor: transform = {2,0,0} scale = {0.5, 1,1}
    // 8 wide platform 3/4 trapdoor:  transform = {3,0,0} scale = {0.25,1,1}
    // 8 wide platform full trapdoor: transform = {4,0,0} scale = {0,   1,1}
    // 4 wide platform half trapdoor: transform = {1,0,0} scale = {0.5, 1,1}
    // 4 wide platform full trapdoor: transform = {2,0,0} scale = {0,   1,1}
    // 
    // --- How to make a "Moving Platform"
    // This one is rather simple, fix the scale at {1,1,1} and just use the transform.
    // Here the transformation_time variable is important to make the player's movements 
    // smooth. Using approximately 4transform-to-1second is a good ratio.
    // Some Examples:
    // transform down 10 clicks:  time = 2.5 transform = {0,-10,0} scale = {1,1,1}
    // transform down 20 clicks:  time = 5   transform = {0,-20,0} scale = {1,1,1}
    // transform down 32 clicks:  time = 8   transform = {0,-32,0} scale = {1,1,1}
    // transform right 10 clicks: time = 2.5 transform = {10,0,0}  scale = {1,1,1}

    public float transformation_time = 2.0F;
    public Vector3 platform_movement;
    public Vector3 platform_scaling = Vector3.one;
    public GameObject platform;
    public string player_tag;

    private Vector3 button_movement = new Vector3(0, -0.15f, 0);
    private float button_movement_time = 0.5F;
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(player_tag) && !activated)
        {
            Debug.Log("Button Pressed! Moving platform: " + platform.name);
            activated = true;
            move_object(gameObject, button_movement, button_movement_time);
            scale_object(platform, platform_scaling, transformation_time);
            move_object(platform, platform_movement, transformation_time);
        }
    }

    private void scale_object(GameObject obj, Vector3 scale, float delay_seconds)
    {
        Vector3 end = new Vector3(
            obj.transform.localScale.x * scale.x,
            obj.transform.localScale.y * scale.y,
            obj.transform.localScale.z * scale.z);
        StartCoroutine(Utility.ScaleOverSeconds(obj, end, delay_seconds));
    }

    private void move_object(GameObject obj, Vector3 movement, float delay_seconds)
    {
        Vector3 end = obj.transform.position + movement;
        // TODO 441: Call our MoveOverSeconds coroutine on the provided game object 
        //StartCoroutine(...);
    }
}