using TMPro;
using UnityEngine;

public class SeedPlanting : MonoBehaviour
{
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private Transform plantingPosition;
    [SerializeField] private TextMeshProUGUI textPlanting;
    [SerializeField] private TextMeshProUGUI harvestText;

    private GameObject currentSeed;
    private int harvestCount = 0;
    private bool isPlanted = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textPlanting.text = "Нажмите Е";

            if (Input.GetKeyDown(KeyCode.E) && !isPlanted)
            {
                PlantSeed();
            }

            if (Input.GetKeyDown(KeyCode.R) && isPlanted && currentSeed != null)
            {
                HarvestPlant();
                UpdateHarvestText();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Planting"))
        {
            textPlanting.text = "";
        }
    }

    private void PlantSeed()
    {
        currentSeed = Instantiate(seedPrefab, plantingPosition.position, Quaternion.identity);
        isPlanted = true;
        Invoke("GrowPlant", 10f); // Вызываем метод GrowPlant спустя 10 секунд
    }

    private void GrowPlant()
    {
        if (currentSeed != null)
        {
            currentSeed.transform.localScale *= 2f; // Увеличиваем размер в 2 раза
            currentSeed.GetComponent<Renderer>().material.color = Color.green; // Меняем цвет на зеленый
        }
    }

    private void HarvestPlant()
    {
        Destroy(currentSeed);
        isPlanted = false;
        harvestCount++;
    }

    private void UpdateHarvestText()
    {
        if (harvestText != null)
        {
            harvestText.text = "Урожай: " + harvestCount;
        }
    }
}
