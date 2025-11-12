using UnityEngine;
using TMPro;

public class ObjectLabelManager : MonoBehaviour
{
    public GameObject labelPrefab;
    public GameObject webcamPlane;
    public float labelScale = 0.12f;
    public float labelYOffset = 0.05f;

    private GameObject currentLabel;

    void Update()
    {
        var store = DetectionStore.Instance;
        if (store == null) return;

        Detection det = store.GetDetection();
        if (det == null)
        {
            if (currentLabel != null)
            {
                Destroy(currentLabel);
                currentLabel = null;
            }
            return;
        }

        float xCenter = (det.bbox[0] + det.bbox[2]) / 2f;
        float yTop = det.bbox[1];

        float normX = xCenter / 640f;
        float normY = yTop / 480f;

        MeshRenderer mr = webcamPlane.GetComponent<MeshRenderer>();
        float planeWidth = mr.bounds.size.x;
        float planeHeight = mr.bounds.size.y;

        Vector3 labelPos = webcamPlane.transform.position
            + webcamPlane.transform.right * (-(normX - 0.5f) * planeWidth)
            + webcamPlane.transform.up * ((0.5f - normY) * planeHeight + labelYOffset)
            + webcamPlane.transform.forward * -0.02f;

        if (currentLabel == null)
        {
            currentLabel = Instantiate(labelPrefab, labelPos, Quaternion.identity, this.transform);
            currentLabel.SetActive(true);
            currentLabel.transform.localScale = Vector3.one * labelScale;
        }
        else
        {
            currentLabel.transform.position = labelPos;
        }

        TextMeshPro tmp = currentLabel.GetComponent<TextMeshPro>();
        if (tmp != null)
        {
            tmp.text = det.label;
            currentLabel.transform.LookAt(Camera.main.transform);
            currentLabel.transform.Rotate(0, 180, 0);
        }
    }
}
