using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject infoPanel;
    public GameObject arViewPanel;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    [Header("UI Components")]
    public Image organImage;

    [Header("Organ Prefabs")]
    public GameObject heartPrefab;
    public GameObject lungsPrefab;
    public GameObject brainPrefab;

    [Header("Organ Data")]
    public Sprite heartSprite;
    public Sprite lungsSprite;
    public Sprite brainSprite;

    [Header("Spawn Point")]
    public Transform organSpawnPoint;

    private GameObject currentOrgan;
    private string selectedOrgan;

    void Start()
    {
        if (SessionData.returnToARView && !string.IsNullOrEmpty(SessionData.selectedOrgan))
        {
            selectedOrgan = SessionData.selectedOrgan;
            SessionData.returnToARView = false; // reset flag

            mainMenuPanel.SetActive(false);
            infoPanel.SetActive(false);
            arViewPanel.SetActive(true);

            // 🧹 Destroy existing organ if any
            if (currentOrgan != null)
            {
                Destroy(currentOrgan);
            }

            // 🧠 Instantiate organ model in AR view
            InstantiateSelectedOrgan();

            // ✅ Also update info panel data so when user goes back, it shows correctly
            UpdateInfoPanel(selectedOrgan);
        }
        else
        {
            // On fresh start, show main menu only
            mainMenuPanel.SetActive(true);
            infoPanel.SetActive(false);
            arViewPanel.SetActive(false);
        }
    }

    public void OnOrganSelected(string organ)
    {
        selectedOrgan = organ;
        SessionData.selectedOrgan = organ; // ✅ persist selection globally

        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(true);
        arViewPanel.SetActive(false);

        UpdateInfoPanel(organ);
    }

    private void UpdateInfoPanel(string organ)
    {
        switch (organ)
        {
            case "Heart":
                titleText.text = "Heart";
                descriptionText.text = "The heart is a muscular organ that pumps oxygen-rich blood throughout the body and returns carbon dioxide-laden blood to the lungs for purification. It plays a crucial role in maintaining blood circulation.";
                organImage.sprite = heartSprite;
                break;
            case "Lungs":
                titleText.text = "Lungs";
                descriptionText.text = "The lungs are a pair of spongy organs responsible for gas exchange. They bring in oxygen from the air and release carbon dioxide from the bloodstream through the process of breathing.";
                organImage.sprite = lungsSprite;
                break;
            case "Brain":
                titleText.text = "Brain";
                descriptionText.text = "The brain is the control center of the body, processing sensory information, regulating vital functions, and enabling cognition, emotions, memory, and voluntary movement.";
                organImage.sprite = brainSprite;
                break;
            default:
                titleText.text = "";
                descriptionText.text = "";
                organImage.sprite = null;
                break;
        }
    }

    public void OnViewInARClicked()
    {
        SessionData.returnToARView = true;

        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(false);
        arViewPanel.SetActive(true);

        if (currentOrgan != null)
        {
            Destroy(currentOrgan);
        }

        InstantiateSelectedOrgan();
    }

    public void OnViewInVRClicked()
    {
        Debug.Log("View in VR clicked – loading VR scene...");
        SceneManager.LoadScene("VRModeScene"); // Update with actual scene name in Build Settings
    }

    public void OnBackToMainMenu()
    {
        if (currentOrgan != null)
        {
            Destroy(currentOrgan);
        }

        mainMenuPanel.SetActive(true);
        infoPanel.SetActive(false);
        arViewPanel.SetActive(false);
    }

    public void OnBackToInfoPanel()
    {
        if (currentOrgan != null)
        {
            Destroy(currentOrgan);
        }

        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(true);
        arViewPanel.SetActive(false);

        // ✅ Re-update info panel when returning
        if (!string.IsNullOrEmpty(selectedOrgan))
        {
            UpdateInfoPanel(selectedOrgan);
        }
    }

    private void InstantiateSelectedOrgan()
    {
        switch (selectedOrgan)
        {
            case "Heart":
                currentOrgan = Instantiate(heartPrefab, organSpawnPoint.position, Quaternion.identity);
                break;
            case "Lungs":
                currentOrgan = Instantiate(lungsPrefab, organSpawnPoint.position, Quaternion.identity);
                break;
            case "Brain":
                currentOrgan = Instantiate(brainPrefab, organSpawnPoint.position, Quaternion.identity);
                break;
            default:
                Debug.LogWarning("Unknown organ selected: " + selectedOrgan);
                break;
        }
    }
}
