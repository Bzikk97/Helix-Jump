using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour

{
    private Vector2 lastTapPos;
    private Vector3 startRotation;
    public float Sensitivity;

    // Start is called before the first frame update
    void Awake ()
    {
        startRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;

            if(lastTapPos == Vector2.zero)
            {
                lastTapPos = curTapPos;
            }

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta * Sensitivity);
        }

        if(Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
    }
}
