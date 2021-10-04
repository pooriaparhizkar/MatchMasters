using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 moveVector;
    public float factor = 0.01f;
    void Start()
    {
        moveVector = new Vector3(1 * factor, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += moveVector;

           //MoveClones(moveVector, true);

            //spriteRenderer.flipX = false;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= moveVector;

            //MoveClones(moveVector, false);

            //spriteRenderer.flipX = true;
        }


    }
}
