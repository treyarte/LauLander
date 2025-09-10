using UnityEngine;

public class CargoRopeVisuals : MonoBehaviour
{
    [SerializeField] private SpriteRenderer iconSpriteRenderer;

    private void Start()
    {
        iconSpriteRenderer.sprite = Lander.Instance.GetCargoSO().sprite;
    }
 }
