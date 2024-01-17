using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoStickWalls : MonoBehaviour
{
    PhysicsMaterial2D myMaterial;
    PetController petMovement;
    public Collider2D myPoly;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = new PhysicsMaterial2D();
        petMovement = GetComponent<PetController>();
        myPoly.sharedMaterial = myMaterial;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (petMovement.isGrounded)
        {
            myMaterial.friction = 0.4f;
            myPoly.enabled = false;
            myPoly.enabled = true;

        }
        else if (petMovement.isInAir)
        {
            myMaterial.friction = 0.0f;
            myPoly.enabled = false;
            myPoly.enabled = true;
        }
    }
}
