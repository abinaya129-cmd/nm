using UnityEngine;

public class WasteSorter : MonoBehaviour
{
    private void Update()
    {
        var store = DetectionStore.Instance;
        if (store == null) return;

        Detection det = store.GetDetection();
        if (det == null) return;

        string label = det.label.ToLower().Trim();
        string targetBin = "Unknown";

        // Simple text-based sorting logic
        if (label.Contains("plastic") || label.Contains("bottle"))
        {
            targetBin = "Plastic Bin (Blue)";
        }
        else if (label.Contains("paper") || label.Contains("cup") || label.Contains("cardboard"))
        {
            targetBin = "Paper Bin (Yellow)";
        }
        else if (label.Contains("metal") || label.Contains("can"))
        {
            targetBin = "Metal Bin (Red)";
        }
        else if (label.Contains("organic") || label.Contains("food") || label.Contains("banana") || label.Contains("apple"))
        {
            targetBin = "Organic Bin (Green)";
        }

        Debug.Log($"🟢 Detected: {label} → Sorting into: {targetBin}");
    }
}
