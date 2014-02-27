using UnityEngine;

//using System.Collections;

public class Player : MonoBehaviour {
    const string HorizontalAxis = "Horizontal";
    const string VerticalAxis   = "Vertical";
    const string JumpAxis       = "Jump";

    public Terrain m_terrain;
    
    void Begin()
    {
        
    }
    
    void Update()
    {
        if (Input.GetAxis(JumpAxis) > 0)
            Jump();

        PerformMovement();
    }

    void Jump()
    {
        // todo...
        Debug.Log("TODO: Jump!");
    }

    void PerformMovement()
    {
        float dx = Input.GetAxis(HorizontalAxis);
        float dz = Input.GetAxis(VerticalAxis);

        float desiredSpeed = 2.0f; // meter / sec
        float actualSpeed = desiredSpeed * Time.deltaTime;
        gameObject.transform.Translate(dx*actualSpeed, 0.0f, dz*actualSpeed);
    }
}