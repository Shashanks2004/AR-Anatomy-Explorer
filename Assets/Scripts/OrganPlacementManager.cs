using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro; // Required for TextMeshProUGUI
using UnityEngine.UI;


public class OrganPlacementManager : MonoBehaviour
{
    [Header("AR Setup")]
    public ARRaycastManager raycastManager;
    public Camera arCamera;

    [Header("Organ Prefabs")]
    public GameObject heartPrefab;
    public GameObject brainPrefab;
    public GameObject lungsPrefab;

    [Header("UI Elements")]
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Image infoImage;

    [Header("Organ Images")]
    public Sprite heartSprite;
    public Sprite brainSprite;
    public Sprite lungsSprite;
    private GameObject currentOrganInstance;
    private string selectedOrgan = "Heart"; // Default organ
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        // Block placement if finger is over UI
        if (Input.touchCount == 0 || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (currentOrganInstance == null)
            {
                currentOrganInstance = Instantiate(GetSelectedOrganPrefab(), hitPose.position, hitPose.rotation);
                currentOrganInstance.AddComponent<OrganManipulator>(); // 👈 ADD gesture script
            }
            else
            {
                currentOrganInstance.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }

            // Show organ information when placed
            ShowInfo(selectedOrgan);
        }
    }

    /// <summary>
    /// Called from UI Buttons to change organ.
    /// </summary>
    public void SelectOrgan(string organName)
    {
        selectedOrgan = organName;

        if (currentOrganInstance != null)
        {
            Destroy(currentOrganInstance);
            currentOrganInstance = null;
        }

        // Hide the panel when changing organs
        infoPanel.SetActive(false);
    }

    private GameObject GetSelectedOrganPrefab()
    {
        switch (selectedOrgan)
        {
            case "Heart": return heartPrefab;
            case "Brain": return brainPrefab;
            case "Lungs": return lungsPrefab;
            default: return heartPrefab;
        }
    }

    private void ShowInfo(string organName) 
   {
        infoPanel.SetActive(true);

        switch (organName)
       {
           case "Heart":
               infoText.text = "🫀 The heart pumps blood throughout the body.";
               infoImage.sprite = heartSprite;
               break;
           case "Brain":
               infoText.text = "🧠 The brain controls your body functions.";
               infoImage.sprite = brainSprite;
            break;
           case "Lungs":
               infoText.text = "🫁 The lungs help in breathing and oxygen exchange.";
               infoImage.sprite = lungsSprite;
               break;
           default:
               infoText.text = "ℹ️ No description available.";
               infoImage.sprite = null; // Or a default sprite
               break;
        }
    }
    public void HideInfoPanel()
    {
        infoPanel.SetActive(false);
    }

}
