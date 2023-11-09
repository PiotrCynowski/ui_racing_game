using UnityEngine;
using UnityEngine.UI;
using RaceLogic;

public class RaceView : MonoBehaviour
{
    [SerializeField] Button _backButton = null;
    [SerializeField] Button _restartButton = null;
    [SerializeField] Text _namesText = null;

    private void Start()
    {
        _backButton.onClick.AddListener(BackClick);
        _restartButton.onClick.AddListener(RestartClick);

        RaceController.Instance.myDelegate += RaceOver;
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveAllListeners();
    }

    private void BackClick()
    {
        gameObject.SetActive(false);

        _restartButton.gameObject.SetActive(false);

        RaceController.Instance.CloseRace();
    }

    private void RestartClick()
    {
        _restartButton.gameObject.SetActive(false);

        RaceController.Instance.RestartRace();
    }

    private void RaceOver()
    {
        _restartButton.gameObject.SetActive(true);
    }

    public void Open(Sprite sprite1, Sprite sprite2, Sprite sprite3)
    {
        if(sprite1 == null || sprite2 == null || sprite3 == null)
        {
            Debug.LogWarning("sprite is missing");
            return;
        }

        gameObject.SetActive(true);

        var names = $"{sprite1.name},{sprite2.name},{sprite3.name}";
        _namesText.text = names;

        //...
        //race logic

        RaceController.Instance.SpawnVehiclesNewRace(sprite1, sprite2, sprite3);
    }
}
