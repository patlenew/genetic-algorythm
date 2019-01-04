using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome : MonoBehaviour
{
    public bool hasReachTarget = false;
    public bool hasLostTarget = false;
    public float jumpForce = 2.0f;
    public float fitness;
    public int count = 0;
    public int lifespan;
    public DNA dna;

    private Rigidbody rb;
    private Vector3 jump;
    private Vector3 dir;
    private Platform targetPlatform;
    private float speed;
    private float timer = 0.1f;
    private bool isGrounded = false;

    void Start()
    {
        //Testing if any direction work
        jumpForce = 2f;
        lifespan = 300;
        speed = 10f;
        jump = new Vector3(0f,1.1f,0f);
        rb = GetComponent<Rigidbody>();

        SetDNA();
    }

    void FixedUpdate()
    {
        CheckIfOnTrackToTarget();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1f))
        {
            if (hit.collider.GetComponent<Platform>() != null)
            {
                isGrounded = true;
            }
        }
    }

    public void StartMoving()
    {
        SetDNA();
    }

    public void SetDNA(DNA newDNA = null)
    {
        if (newDNA != null)
        {
            dna = newDNA;
        }
        else
        {
            dna = new DNA(null);
            dna.SetDirection();
        }
    }

    private void CheckDNA()
    {
        timer -= Time.deltaTime;
        count++;
        if (timer < 0f && count < lifespan)
        {
            dna.SwitchDirection();
            timer = 0.1f;
        }
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
        transform.position += dir * speed * Time.deltaTime;      
    }

    void Jump()
    {
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    public void SetTargetPlatform(Platform targetPlatform)
    {
        this.targetPlatform = targetPlatform;
    }

    public void CalculateFitness()
    {
        if (targetPlatform == null)
        {
            return;
        }

        var dist = transform.position - targetPlatform.GetPos();
        float magnetude = Vector3.Distance(transform.position, targetPlatform.GetPos());
        fitness = 1 / magnetude;

        if (hasReachTarget)
        {
            fitness *= 10;
        }

        if (hasLostTarget || (dist.y < targetPlatform.GetPos().y))
        {
            fitness /= 10;
        }
    }

    public void CheckIfOnTrackToTarget()
    {
        float dist = Vector3.Distance(transform.position, targetPlatform.GetPos());
        if (dist < 3f)
        {
            hasReachTarget = true;
        }

        if(transform.position.x < -2f || transform.position.y < -10f)
        {
            hasLostTarget = true;
        }

        if (!hasReachTarget && !hasLostTarget)
        {
            CheckDNA();
            MoveInputs();
            CheckDirection();
        }
    }
}   
