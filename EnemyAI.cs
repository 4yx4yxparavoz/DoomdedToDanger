using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform[] patrolPoints;
    public Transform player;
    public GameObject losePanel;
    public PlayerController playerController;
    public CameraController cameraController;
    public AudioSource backgroundMusic;

    [Header("Settings")]
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float losePlayerDistance = 15f;
    public float timeToLosePlayer = 3f;

    private NavMeshAgent agent;
    private Animator animator;

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private bool isAttacking = false;
    private float loseTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GoToNextPatrolPoint();
        losePanel.SetActive(false);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                agent.isStopped = true;
                agent.ResetPath();
                SetAnimation("Attack");
                Invoke(nameof(ShowLoseScreen), 0.5f);
            }
            return;
        }

        if (distanceToPlayer <= chaseRange)
        {
            StartChasing();
        }
        else if (isChasing)
        {
            HandleLosePlayer(distanceToPlayer);
        }

        PatrolUpdate();
        UpdateAnimations();
    }

    void StartChasing()
    {
        isChasing = true;
        isAttacking = false;
        loseTimer = 0f;
        agent.isStopped = false;

        if (Vector3.Distance(agent.destination, player.position) > 0.5f)
            agent.SetDestination(player.position);
    }

    void HandleLosePlayer(float distance)
    {
        loseTimer += Time.deltaTime;
        if (loseTimer >= timeToLosePlayer || distance > losePlayerDistance)
        {
            isChasing = false;
            GoToNextPatrolPoint();
        }
        else
        {
            if (Vector3.Distance(agent.destination, player.position) > 0.5f)
                agent.SetDestination(player.position);
        }
    }

    void PatrolUpdate()
    {
        if (!isChasing && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
            {
                GoToNextPatrolPoint();
            }
        }
    }

    void GoToNextPatrolPoint()
    {
        isAttacking = false;
        agent.isStopped = false;

        if (patrolPoints.Length == 0) return;

        Transform nextPoint = patrolPoints[currentPatrolIndex];
        agent.SetDestination(nextPoint.position);

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void UpdateAnimations()
    {
        float speed = agent.velocity.magnitude;

        if (isAttacking)
        {
            SetAnimation("Attack");
        }
        else if (speed > 0.1f)
        {
            SetAnimation(isChasing ? "Run" : "Walk");
        }
        else
        {
            SetAnimation("Idle");
        }
    }

    void SetAnimation(string state)
    {
        animator.SetBool("isWalking", state == "Walk");
        animator.SetBool("isRunning", state == "Run");
        animator.SetBool("isAttacking", state == "Attack");
    }

    void ShowLoseScreen()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;

        if (playerController != null)
            playerController.enabled = false;

        if (cameraController != null)
            cameraController.enabled = false;

        if (backgroundMusic != null)
            backgroundMusic.Pause();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        if (backgroundMusic != null)
            backgroundMusic.Play();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
