using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    const string START_BUTTON_SAVE_TAG = "TitleScreenSaveTag";
    [SerializeField] Text _startButtonText;
    [SerializeField] Text _ballNameText;
    [SerializeField] Image _logoImage;
    [SerializeField] Image _startButtonImage;
    [SerializeField] Transform _startButtonPlace;
    [SerializeField] AssetReferenceSprite _logoAssetReference;
    [SerializeField] AssetReferenceSprite _startButtonAssetReference;

    bool IsFirstTime
    {
        get { return PlayerPrefs.GetInt(START_BUTTON_SAVE_TAG, 1) == 1; }
        set { PlayerPrefs.SetInt(START_BUTTON_SAVE_TAG, value ? 1 : 0); }
    }
    private void Awake()
    {
        if (IsFirstTime)
        {

            _startButtonText.text = BAHMANLanguageManager._Instance._Translate("Start");
        }
    }
    void Start()
    {
        _logoAssetReference.LoadAssetAsync<Sprite>().Completed += logoAssetReference_Completed;
        _startButtonAssetReference.LoadAssetAsync<Sprite>().Completed += startButtonAssetReference_Completed;
        BallSpawner._Instance._SpawnBall(GameSettingInfo.Instance.CurrentBallInfo.BallMesh, _startButtonPlace, true);
        _ballNameText.text = GameSettingInfo.Instance.CurrentBallInfo.BallName;
    }

    private void startButtonAssetReference_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> iAsyncResult)
    {
        if(iAsyncResult.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            _startButtonImage.sprite = iAsyncResult.Result;
        }
    }

    private void logoAssetReference_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> iAsyncResult)
    {
        if (iAsyncResult.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            _logoImage.sprite = iAsyncResult.Result;
        }
    }

    public void _StartGame()
    {
        IsFirstTime = false;
        BAHMANLoadingManager._INSTANCE._LoadScene(AllScenes.GameScene);
    }

    private void OnDestroy()
    {
        _logoAssetReference.ReleaseAsset();
        _startButtonAssetReference.ReleaseAsset();
    }
}
