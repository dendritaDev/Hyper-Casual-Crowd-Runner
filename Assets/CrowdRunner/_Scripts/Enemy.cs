using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    enum State { Idle, Running }

    [Header(" Settings ")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State state;
    private Transform targetRunner;

    [Header("Events")]
    public static Action onRunnerDied;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                SearchForTarget();
                break;
            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void RunTowardsTarget()
    {
        if(targetRunner == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);

            onRunnerDied?.Invoke(); 
            
        }
    }

    private void StartRunningTowardsTarget()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void SearchForTarget()
    {
        Collider[] detectedColiders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColiders.Length; i++)
        {
            if (detectedColiders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                    continue;

                runner.SetTarget();
                targetRunner = runner.transform;

                StartRunningTowardsTarget();
            }
        }
    }
}
