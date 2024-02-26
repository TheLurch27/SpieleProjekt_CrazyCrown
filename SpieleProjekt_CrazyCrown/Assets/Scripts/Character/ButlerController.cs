using UnityEngine;

public class ButlerController : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float stoppingDistance = 2f;
    public float waitTimeBeforeReturning = 5f; // Wartezeit, bevor der Butler zum Startpunkt zurückkehrt

    private Transform playerTransform;
    private Vector3 initialPosition;
    private bool isMovingTowardsPlayer = false;
    private bool isWaiting = false;
    private bool isReturning = false;
    private float waitTimer = 0f;

    private SpriteRenderer spriteRenderer;
    private Animator animator; // Animator des Butlers

    private void Start()
    {
        initialPosition = transform.position; // Speichern Sie die Startposition des Butlers

        // Finden Sie den Spieler und starten Sie die Bewegung zum Spieler
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            isMovingTowardsPlayer = true;
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        // Holen Sie sich den SpriteRenderer-Komponenten des Butlers
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Holen Sie sich den Animator-Komponenten des Butlers
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMovingTowardsPlayer && playerTransform != null)
        {
            // Berechnen Sie die Richtung zum Spieler
            Vector3 direction = playerTransform.position - transform.position;
            direction.z = 0f; // Stellen Sie sicher, dass die Bewegung nur in der Ebene bleibt

            // Überprüfen Sie, ob der Butler die gewünschte Entfernung zum Spieler erreicht hat
            if (direction.magnitude > stoppingDistance)
            {
                // Bewegen Sie den Butler in Richtung des Spielers
                transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);

                // Flippen des Sprites, wenn der Butler nach links geht
                spriteRenderer.flipX = direction.x < 0f;

                // Spielen Sie die WALK-Animation ab
                animator.SetBool("isWalking", true);
            }
            else
            {
                // Stoppen Sie die Bewegung, wenn die gewünschte Entfernung erreicht ist
                isMovingTowardsPlayer = false;
                isWaiting = true;
                waitTimer = 0f;

                // Spielen Sie die IDLE-Animation ab
                animator.SetBool("isWalking", false);
            }
        }
        else if (isWaiting)
        {
            // Butler wartet, bis er zurückkehrt
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeBeforeReturning)
            {
                isWaiting = false;
                isReturning = true;
            }
        }
        else if (isReturning)
        {
            // Butler kehrt zum Startpunkt zurück
            Vector3 directionToInitialPosition = initialPosition - transform.position;
            directionToInitialPosition.z = 0f; // Stellen Sie sicher, dass die Bewegung nur in der Ebene bleibt

            // Überprüfen Sie, ob der Butler den Startpunkt erreicht hat
            if (directionToInitialPosition.magnitude > 0.1f) // 0.1f für kleine Toleranz, um Rundungsfehler zu vermeiden
            {
                // Bewegen Sie den Butler zurück zur Startposition
                transform.Translate(directionToInitialPosition.normalized * movementSpeed * Time.deltaTime);

                // Flippen des Sprites, wenn der Butler nach rechts geht
                spriteRenderer.flipX = directionToInitialPosition.x < 0f;

                // Spielen Sie die WALK-Animation ab
                animator.SetBool("isWalking", true);
            }
            else
            {
                // Bewegung beenden, wenn der Startpunkt erreicht ist
                isReturning = false;

                // Überprüfen, ob der Butler außerhalb der Kameraansicht liegt
                if (!IsVisibleByCamera())
                {
                    Destroy(gameObject); // Zerstören Sie den Butler, wenn er außerhalb der Kameraansicht liegt
                }

                // Spielen Sie die IDLE-Animation ab
                animator.SetBool("isWalking", false);
            }
        }
    }

    // Überprüfen, ob der Butler von der Kamera sichtbar ist
    private bool IsVisibleByCamera()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1;
    }
}
