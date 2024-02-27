using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Der zu verfolgende Charakter
    public float smoothTime = 0.3f; // D�mpfungsfaktor f�r die Kamerabewegung
    public float leftBoundary; // Der linke Grenzwert f�r die Kamerabewegung entlang der X-Achse
    public float rightBoundary; // Der rechte Grenzwert f�r die Kamerabewegung entlang der X-Achse

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
