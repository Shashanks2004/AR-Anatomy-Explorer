using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrganManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject infoPanel;
    public GameObject arViewPanel;

    [Header("Info Elements")]
    public TextMeshProUGUI infoText;
    public Image infoImage;
    public Button viewIn3DButton;

    [Header("AR Setup")]
    public GameObject heartPrefab, brainPrefab, lungsPrefab;
    public Sprite heartSprite, brainSprite, lungsSprite;
    public GameObject arContentParent;

    private string selectedOrgan;

    void Start()
    {
        ShowMainMenu();
    }

    public void OnOrganButtonClicked(string organName)
    {
        selectedOrgan = organName;
        ShowInfoPanel(organName);
    }

    void ShowInfoPanel(string organName)
    {
        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(true);
        arViewPanel.SetActive(false);

        switch (organName)
        {
            case "Heart":
                infoText.text = "🫀 The heart pumps blood throughout the body.";
                infoImage.sprite = heartSprite;
                break;
            case "Brain":
                infoText.text = "🧠 The brain controls body functions.";
                infoImage.sprite = brainSprite;
                break;
            case "Lungs":
                infoText.text = "🫁 The lungs help in breathing.";
                infoImage.sprite = lungsSprite;
                break;
        }
    }

    public void OnViewIn3DPressed()
    {
        infoPanel.SetActive(false);
        arViewPanel.SetActive(true);

        // Destroy existing model if any
        foreach (Transform child in arContentParent.transform)
            Destroy(child.gameObject);

        // Instantiate model
        GameObject prefab = GetSelectedPrefab();
        Instantiate(prefab, arContentParent.transform);
    }

    public void OnBackToMenu()
    {
        infoPanel.SetActive(false);
        arViewPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    void ShowMainMenu()
    {
        infoPanel.SetActive(false);
        arViewPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    GameObject GetSelectedPrefab()
    {
        switch (selectedOrgan)
        {
            case "Heart": return heartPrefab;
            case "Brain": return brainPrefab;
            case "Lungs": return lungsPrefab;
            default: return null;
        }
    }
}
