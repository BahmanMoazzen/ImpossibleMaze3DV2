/*
 * public relation manager V1.1
 * manages the messages to persuade player do certain actions on the market
 */


using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class BAHMANPublicRelation : MonoBehaviour
{
    // use to save scene count on PlayerPref
    const string PRPREFIX = "BPR";

    [Header("Message Settings")]
    [Tooltip("Uses to show proper messages")]
    [SerializeField]
    PublicRelationMessageInfo[] _Messages;
    [Tooltip("Settings for page count")]
    [SerializeField] PublicRelationPageCountSettingInfo _PageCountSettings;

    [Header("Share Settings")]
    [Tooltip("sets the subject (primarily used in e-mail applications)")]
    [SerializeField] string _subject;
    [Tooltip("sets the shared text. Note that the Facebook app will omit text, if exists")]
    [SerializeField] string _text;
    [Tooltip("sets the title of the share dialog on Android platform.Has no effect on iOS")]
    [SerializeField] string _title;
    
    [Header("Donate Settings")]
    [SerializeField]
    string _donateURL;

    [Header("Intent Settings")]
    [SerializeField] PublicRelationMarketIntentInfo _MarketIntentSettings;

    private void Awake()
    {

        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
    private void SceneManager_sceneLoaded(Scene iLoadedScene, LoadSceneMode iLoadMode)
    {


        StopAllCoroutines();
        StartCoroutine(_scenLoadRoutine(iLoadedScene.name));

    }
    IEnumerator _scenLoadRoutine(string iSceneName)
    {
        string saveName = $"{PRPREFIX}_{iSceneName}_count";
        int newCount = PlayerPrefs.GetInt(saveName, 0) + 1;
        PlayerPrefs.SetInt(saveName, newCount);
        foreach (var prs in _PageCountSettings._PageCountSettings)
        {
            if (prs.SceneName.Equals(iSceneName))
            {
                if (newCount == prs.SceneCount)
                {

                    switch (prs.RelationType)
                    {
                        case PublicRelationType.Share:
                            BAHMANMessageBoxManager._INSTANCE._ShowYesNoBox(
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Title),
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Message),
                                _ShareClicked);

                            break;
                        case PublicRelationType.Rate:
                            BAHMANMessageBoxManager._INSTANCE._ShowYesNoBox(
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Title),
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Message), _RateClicked);

                            break;
                        case PublicRelationType.OtherProduct:
                            BAHMANMessageBoxManager._INSTANCE._ShowYesNoBox(
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Title),
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Message),
                                _OtherProductClicked);

                            break;
                        case PublicRelationType.Donate:
                            BAHMANMessageBoxManager._INSTANCE._ShowYesNoBox(
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Title),
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Message),
                                _DonateClicked);
                            break;
                        case PublicRelationType.Message:
                            BAHMANMessageBoxManager._INSTANCE._ShowConfirmBox(
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Title),
                                BAHMANLanguageManager._Instance._Translate(_Messages[(int)prs.RelationType].Message)
                                );
                            break;
                    }
                    break;
                }

            }
            yield return null;
        }
    }
    public void _ShareClicked()
    {
        string nsSubject = BAHMANLanguageManager._Instance._Translate(_subject);
        string nsText =string.Format( BAHMANLanguageManager._Instance._Translate(_text)
            ,Application.identifier //{0}
            ,BAHMANLanguageManager._Instance._Translate(_MarketIntentSettings.MarketName) //{1}
            );
        string nsTitle = BAHMANLanguageManager._Instance._Translate(_title);
        NativeShare NS = new NativeShare()
            .SetSubject(nsSubject)
            .SetText(nsText)
            .SetTitle(nsTitle);
        NS.Share();

    }
    public void _OtherProductClicked()
    {
        Application.OpenURL(string.Format(_MarketIntentSettings.MarketOtherProductURL, Application.identifier));

    }
    public void _RateClicked()
    {
        Application.OpenURL(string.Format(_MarketIntentSettings.MarketRatingURL, Application.identifier));
    }
    public void _DonateClicked()
    {
        Application.OpenURL(_donateURL);
    }
    public void _MessageClicked()
    {
        //Application.OpenURL(_donateURL);
        //_ClosePanel();
    }




}



