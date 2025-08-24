using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiValuesText;
    [SerializeField] private Image fuelSlider;
    [SerializeField] private GameObject speedLeftArrow;
    [SerializeField] private GameObject speedRightArrow;
    [SerializeField] private GameObject speedUpArrow;
    [SerializeField] private GameObject speedDownArrow;

    private void Update()
    {
        UpdateUiText();
        UpdateFuelSlider();
    }
    
    private void UpdateUiText()
    {
        float landerSpeedX = Lander.Instance.GetSpeedX();
        float landerSpeedY = Lander.Instance.GetSpeedY();
        speedLeftArrow.SetActive(landerSpeedX < 0);
        speedRightArrow.SetActive(landerSpeedX >= 0);
        speedUpArrow.SetActive(landerSpeedY >= 0);
        speedDownArrow.SetActive(landerSpeedY < 0);

        uiValuesText.text = GameManager.Instance.GetCurrentLevel() + "\n" +
                            GameManager.Instance.Score + "\n" +
                            GameManager.Instance.Coins + "\n" +
                            GameManager.Instance.GetTime() + "\n" +
                            Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX())) + "\n" +
                            Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedY())) + "\n";
    }

    private void UpdateFuelSlider()
    {
        fuelSlider.fillAmount = Lander.Instance.GetNormalizedFuel();
    } 
}
