using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public float pickupRadius = 1f; // Radius, in dem der Spieler den Gegenstand aufnehmen kann
    public string playerTag = "Player"; // Tag des Spielers
    public GameObject itemObject; // Das GameObject des aufsammelbaren Gegenstands

    private void Update()
    {
        // �berpr�fen, ob der PickUpItem-Button gedr�ckt wurde
        if (Input.GetButtonDown("PickUpItem"))
        {
            // �berpr�fen, ob der Spieler in Reichweite des Items ist
            if (IsPlayerInRange())
            {
                // Item zerst�ren
                DestroyItem();
            }
        }
    }

    // �berpr�ft, ob der Spieler in Reichweite des Items ist
    private bool IsPlayerInRange()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, pickupRadius);
        return playerCollider != null && playerCollider.CompareTag(playerTag);
    }

    // Zerst�rt das Item-GameObject
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
