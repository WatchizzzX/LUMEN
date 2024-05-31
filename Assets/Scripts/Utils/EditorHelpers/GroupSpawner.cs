using UnityEngine;

namespace Utils.EditorHelpers
{
    public class GroupSpawner : MonoBehaviour
    {
        [SerializeField] public GameObject targetObject;
        [SerializeField] public int xCount;
        [SerializeField] public int yCount;
        [SerializeField] public int zCount;
        [SerializeField] public float xGap;
        [SerializeField] public float yGap;
        [SerializeField] public float zGap;
    }
}