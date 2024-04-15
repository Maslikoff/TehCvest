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
            textPlanting.text = "������� �";

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
        Invoke("GrowPlant", 10f); // �������� ����� GrowPlant ������ 10 ������
    }

    private void GrowPlant()
    {
        if (currentSeed != null)
        {
            currentSeed.transform.localScale *= 2f; // ����������� ������ � 2 ����
            currentSeed.GetComponent<Renderer>().material.color = Color.green; // ������ ���� �� �������
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
            harvestText.text = "������: " + harvestCount;
        }
    }
}
