using UnityEngine;

[CreateAssetMenu(fileName = "BasePoint", menuName = "Custom/BasePoint")]
public class BasePoint : ScriptableObject
{
    public enum DiskColor
    {
        Orange,
        Red,
        DeepBlue,
        Black
    }
    void OnEnable()
    {
        if (colorPoints == null || colorPoints.Length == 0)
        {
            colorPoints = new ColorPoint[4];
            colorPoints[0].color = DiskColor.Orange;
            colorPoints[1].color = DiskColor.Red;
            colorPoints[2].color = DiskColor.DeepBlue;
            colorPoints[3].color = DiskColor.Black;

            colorPoints[0].baseColor = new Color(1.0f, 0.5f, 0.0f);
            colorPoints[1].baseColor = Color.red;
            colorPoints[2].baseColor = new Color(0.0f, 0.0f, 0.5f);
            colorPoints[3].baseColor = Color.black;

        }
    }
    [System.Serializable]
    public struct ColorPoint
    {
        public DiskColor color;
        public Color baseColor;
        public int baseScore;
    }

    [SerializeField]
    public ColorPoint[] colorPoints;

    
}