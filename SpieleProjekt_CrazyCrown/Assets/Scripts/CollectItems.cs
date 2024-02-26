using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public string itemName; // Der Name des Items, z.B. "Edding", "Egg", usw.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(); // Ruf die Methode Collect auf, wenn der Spieler das Item berührt
        }
    }

    internal void Collect()
    {
        Inventory.Instance.AddItem(itemName); // Füge das Item dem Inventar hinzu
        Destroy(gameObject); // Zerstöre das Item-Objekt, nachdem es aufgenommen wurde
    }
}