using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Der Spieler, dem die Kamera folgen soll
    public Vector2 minBounds; // Der minimale Bereich, den die Kamera anzeigen soll (untere linke Ecke)
    public Vector2 maxBounds; // Der maximale Bereich, den die Kamera anzeigen soll (obere rechte Ecke)
    public float smoothTime = 0.3f; // Die Geschwindigkeit, mit der die Kamera dem Spieler folgt

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // Begrenzen Sie die Kameraposition basierend auf den minimalen und maximalen Grenzen
            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
