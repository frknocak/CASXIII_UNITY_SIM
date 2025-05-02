using UnityEngine;

public class RadarUI : MonoBehaviour
{
    public Transform radarObject;
    public RectTransform radarPanel;
    public GameObject radarDotPrefab;
    public float uiScale = 3f;

    public void ShowPing(Vector3 worldPosition)
    {
        Vector3 offset = worldPosition - radarObject.position;
        Vector2 flatOffset = new Vector2(offset.x, offset.z);
        Vector2 radarPos = flatOffset * uiScale;

        GameObject dot = Instantiate(radarDotPrefab, radarPanel);
        dot.GetComponent<RectTransform>().anchoredPosition = radarPos;

        Destroy(dot, 0.5f); // 0.5 saniye sonra yok et
    }
}
