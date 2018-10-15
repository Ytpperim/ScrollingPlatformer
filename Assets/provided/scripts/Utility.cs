using UnityEngine;
using System.Collections;

public class Utility {

    // Coroutine for moving a game object over some amount of seconds.
    // NOTICE: the game object MUST have a rigidbody component for this coroutine to succeed.
    public static IEnumerator MoveOverSeconds(GameObject obj, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        // We are using built-in physics, so we will modify position via 'move-position
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        Vector3 startingPos = obj.transform.position;
        while (elapsedTime < seconds)
        {
            // TODO 441: Move our position to some lerped vector between startingPosition and end based on the elapsed time
            rb.MovePosition(startingPos); // TODO 441: REMOVE THIS LINE and uncomment the 2 lines below
            //Vector3 lerped_vector = Vector3.Lerp(...);  
            //rb.MovePosition(lerped_vector);

            // Sync up with frame rate
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        rb.MovePosition(Vector3.Lerp(startingPos, end, Mathf.SmoothStep(0, 1, 1)));
    }

    // Coroutine for scaling a game obejct over some amount of seconds.
    public static IEnumerator ScaleOverSeconds(GameObject obj, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        // We are using built-in physics, so we will modify position via 'move-position
        Transform trans = obj.transform;
        Vector3 startingScale = trans.localScale;
        while (elapsedTime < seconds)
        {
            trans.localScale = Vector3.Lerp(startingScale, end, Mathf.SmoothStep(0, 1, elapsedTime / seconds));

            // Sync up with frame rate
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        trans.localScale = Vector3.Lerp(startingScale, end, Mathf.SmoothStep(0, 1, 1));
    }

}
