using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Der zu verfolgende Charakter
    public float smoothTime = 0.3f; // Dämpfungsfaktor für die Kamerabewegung
    public float leftBoundary; // Der linke Grenzwert für die Kamerabewegung entlang der X-Achse
    public float rightBoundary; // Der rechte Grenzwert für die Kamerabewegung entlang der X-Achse

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        if (target != null)
        {
            float targetX = Mathf.Clamp(target.position.x, leftBoundary, rightBoundary); // Begrenze die Ziel-X-Position basierend auf den Grenzwerten

            Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
