using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public float pickupRadius = 1f; // Radius, in dem der Spieler den Gegenstand aufnehmen kann
    public string playerTag = "Player"; // Tag des Spielers
    public GameObject itemObject; // Das GameObject des aufsammelbaren Gegenstands

    private void Update()
    {
        // Überprüfen, ob der PickUpItem-Button gedrückt wurde
        if (Input.GetButtonDown("PickUpItem"))
        {
            // Überprüfen, ob der Spieler in Reichweite des Items ist
            if (IsPlayerInRange())
            {
                // Item zerstören
                DestroyItem();
            }
        }
    }

    // Überprüft, ob der Spieler in Reichweite des Items ist
    private bool IsPlayerInRange()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, pickupRadius);
        return playerCollider != null && playerCollider.CompareTag(playerTag);
    }

    // Zerstört das Item-GameObject
    private void DestroyItem()
    {
        if (itemObject != null)
        {
            Debug.Log("Item picked up: " + itemObject.name);
            Destroy(itemObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Zeichne einen visuellen Gizmo, um den Pickup-Radius anzuzeigen
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
