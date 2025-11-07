using System.Collections.Generic;
using UnityEngine;

public class SavedControlPoints : MonoBehaviour
{
    public int numLinks, linkControlPoints;
    public GameObject controlPointPrefab;
    public GameObject bezierLinkPrefab;

    public BezierCurve curve;

    public static List<List<Transform>> controlPoints = new List<List<Transform>>();

    [SerializeField]  // Esto lo guarda Unity en el archivo de escena o prefab
    private List<List<Vector3>> savedControlPoints = new List<List<Vector3>>();

    void OnDisable()  // Se llama al salir del modo Play
    {
        SaveControlPoints();
    }

    void OnEnable()  // Se llama al volver a activar el script / cargar la escena
    {
        if (savedControlPoints.Count > 0 && controlPoints.Count == 0)
            LoadControlPoints();
    }

    void SaveControlPoints()
    {
        savedControlPoints.Clear();

        foreach (var link in controlPoints)
        {
            List<Vector3> positions = new List<Vector3>();
            foreach (var point in link)
            {
                if (point != null)
                    positions.Add(point.position);
            }
            savedControlPoints.Add(positions);
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);  // Para guardar en el editor
#endif
    }

    void LoadControlPoints()
    {
        controlPoints.Clear();

        foreach (var savedLink in savedControlPoints)
        {
            List<Transform> linkPoints = new List<Transform>();

            foreach (var pos in savedLink)
            {
                GameObject obj = Instantiate(controlPointPrefab, pos, Quaternion.identity);
                linkPoints.Add(obj.transform);
            }

            controlPoints.Add(linkPoints);
        }
    }
}

