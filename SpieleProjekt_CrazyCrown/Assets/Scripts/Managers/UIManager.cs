using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Image eddingImage;
    public Image eggImage;
    public Image vomitPowderImage;
    public Image crownImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayItemUI(string itemName)
    {
        switch (itemName)
        {
            case "Edding":
                eddingImage.enabled = true;
                eggImage.enabled = false;
                vomitPowderImage.enabled = false;
                crownImage.enabled = false;
                break;
            case "Egg":
                eggImage.enabled = true;
                eddingImage.enabled = false;
                vomitPowderImage.enabled = false;
                crownImage.enabled = false;
                break;
            case "VomitPowder":
                vomitPowderImage.enabled = true;
                eddingImage.enabled = false;
                eggImage.enabled = false;
                crownImage.enabled = false;
                break;
            case "Crown":
                crownImage.enabled = true;
                eddingImage.enabled = false;
                eggImage.enabled = false;
                vomitPowderImage.enabled = false;
                break;
        }
    }
}
