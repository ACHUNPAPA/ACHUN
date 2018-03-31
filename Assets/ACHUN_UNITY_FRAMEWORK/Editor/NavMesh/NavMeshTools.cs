using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public abstract class NavMeshTools
{
    [MenuItem("Assets/Export NavMesh Data")]
    private static void ExportNavMeshData()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();
    }
}