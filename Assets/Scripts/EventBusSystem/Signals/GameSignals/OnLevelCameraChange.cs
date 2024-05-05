using EventBusSystem.Interfaces;
using Unity.Cinemachine;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnLevelCameraChange : ISignal
    {
        public readonly CinemachineCamera TargetCamera;

        public OnLevelCameraChange(CinemachineCamera targetCamera)
        {
            TargetCamera = targetCamera;
        }
    }
}