using NaughtyAttributes;
using UnityEngine;

namespace Utils.EditorHelpers
{
    public class RandomPosition : MonoBehaviour
    {
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 xPositionRange = new(1f, 20f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 yPositionRange = new(1f, 20f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 zPositionRange = new(1f, 20f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 xRotationRange = new(0f, 0f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 yRotationRange = new(0f, 0f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 zRotationRange = new(0f, 0f);
    }
}