using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
   private const float NORMAL_OTOGRAPHIC_ZOOM = 10f;
   
   [SerializeField] private CinemachineCamera cinemachineCamera;
   private float zoomSpeed = 2f;
   
   private float targetOrthographicSize = 10f;
   
   public static CinemachineCameraZoom2D Instance;

   
   public void Awake()
   {
      Instance = this;
   }

   private void Update()
   {
      cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
   }
   public void SetTargetOrthographicSize(float newOrthographicSize)
   {
      targetOrthographicSize = newOrthographicSize;
   }

   public void SetNormalOrthographicSize()
   {
      targetOrthographicSize = NORMAL_OTOGRAPHIC_ZOOM;
   }
}
