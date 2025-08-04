using UnityEngine;
using UnityEngine.SceneManagement;

// ✅ Optional: Use this class to load AR scene from VR
public class SceneLoader : MonoBehaviour
{
    public void LoadARScene()
    {
        // Replace "SampleScene" with your actual AR scene name
        SceneManager.LoadScene("SampleScene"); 
    }
}

// ✅ Use this class for the VR scene's back button
public class VRBackButton : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        Debug.Log("Returning from VR to AR scene...");
        
        // ✅ Replace this with your AR scene name (NOT "YourMainSceneName")
        SceneManager.LoadScene("ARViewPanel");
    }
}
