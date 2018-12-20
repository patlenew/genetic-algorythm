using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome : MonoBehaviour
{

    public enum MoveDirection { LEFT, RIGHT, JUMP }
    public MoveDirection currentDir;
    public float jumpForce = 2.0f;

    private DNA dna;
    private Rigidbody rb;
    private Vector3 jump;
    private Vector3 dir;
    private float speed = 10f;
    private bool isGrounded = false;

    void Start()
    {
        //Testing if any direction work
        jump = new Vector3(0f,0.7f,0f);
        rb = GetComponent<Rigidbody>();
        //SetRandomDirection();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.right, Color.white);
        MoveInputs();
        CheckDirection();
    }

    //Test method
    /*private void SetRandomDirection()
    {
        int range = Random.Range(0, 2);

        switch (range)
        {
            case 0:
                currentDir = MoveDirection.LEFT;
                break;
            case 1:
                currentDir = MoveDirection.RIGHT;
                break;
            default:
                break;
        }
    }*/

    public void StartMoving()
    {
        SetDNA();
    }

    private void SetDNA()
    {
        dna = GetComponent<DNA>();
    }

    private void CheckDirection()
    {
        if (isGrounded && dna.currentDir == DNA.MoveDirection.JUMP)
        {
            Jump();
        }
    }

    void MoveInputs()
    {
        dir = dna.GetDirectionValue();

        /*if (currentDir == MoveDirection.LEFT)
        {
            dir = new Vector3(Random.Range(-0.1f, -0.3f), 0f, 0f);
        }
        else if (currentDir == MoveDirection.RIGHT)
        {
            dir = new Vector3(Random.Range(0.1f, 0.3f), 0f, 0f);
        }
        else if (currentDir == MoveDirection.JUMP)
        {
            //dir = new Vector3(0f,0.2f,0f);
            Jump();
        }*/

        transform.position += dir * speed * Time.deltaTime;      
    }

    void Jump()
    {
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    public void IgnoreColliders(List<Genome> genomes)
    {
        foreach (Genome genome in genomes)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), genome.GetComponent<Collider>());
        }
    }

    //le fitness rate dans ce cas ci
    //serait le plus proche que le character peut arrivé du goal
    public void FitnessRate()
    {

    }
}
