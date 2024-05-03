using NaughtyAttributes;
using UnityEngine;

namespace Utils
{
    public class GroupRandomPosition : MonoBehaviour
    {
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 xPositionRange = new(1f, 20f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 yPositionRange = new(1f, 20f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 zPositionRange = new(1f, 20f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 xRotationRange = new(0f, 0f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 yRotationRange = new(0f, 0f);
        [SerializeField, MinMaxSlider(0f, 100f)] public Vector2 zRotationRange = new(0f, 0f);
        [SerializeField, MinMaxSlider(-360f, 360f)] public Vector2 xRotationLimit = new(-10f, 10f);
        [SerializeField, MinMaxSlider(-360f, 360f)] public Vector2 yRotationLimit = new(-10f, 10f);
        [SerializeField, MinMaxSlider(-360f, 360f)] public Vector2 zRotationLimit = new(-10f, 10f);
    }
}