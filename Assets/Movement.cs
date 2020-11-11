using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public List<Transform> _processTree;
    private GameHandler A;
    private Transform player;
    private int indexOfPlayer;
    private bool _isWon = false;
    private bool _isLost = false;
    private double _timeOut = 30;
    private Slider _timeBar = null;
    public Canvas WinLabel;
    public Canvas LoseLabel;
    
    // Start is called before the first frame update
    void Start(){
        Debug.Log("START THE GAME!");
        List<Transform> list = new List<Transform>();
        Transform[] listOfTemp = GetComponentsInChildren<Transform>();
        for(int i = 0; i < listOfTemp.Length; i++){
            if(listOfTemp[i].gameObject.name != "Cubes"){
                list.Add(listOfTemp[i]);
            }
        }

        List<int> randomCube = GameHandler.RandomInt(list.Count);
        foreach (var item in randomCube)
        {
            _processTree.Add(list[item]);
        }

        A = new GameHandler(_processTree);
        _timeOut = A.GetTimeOut();
        player = A.FindPlayer();
        player.gameObject.SetActive(_isWon);
        A.DisplayProcessTree();
    }


    public void RunTimeOut(){
        _isWon = A.CheckWinning();
        _timeOut -= 1 * Time.deltaTime;
        if(_isWon){
            Debug.Log("WIN ROI");
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
        }
    }

    public void BindingToSlider(){
        _timeBar = GameObject.FindObjectOfType<Slider>();
        _timeBar.maxValue = (float)A.GetTimeOut();
        _timeBar.value = (float)_timeOut;
        Debug.Log(_timeBar.value);
        if(_timeBar.value <= 0){
            _isLost = true;
            Debug.Log("THUA ROI");
            Time.timeScale = 0;
        }
        var star_good = GameObject.Find("star_good");
        //star_good.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        
        var star_great = GameObject.Find("star_great");

        var star_perfect = GameObject.Find("star_perfect");
        
        float starPosGood = (_timeBar.GetComponent<RectTransform>().rect.width * 5)/100;
        float starPosGreat = (_timeBar.GetComponent<RectTransform>().rect.width * 40)/100 - 13;
        float starPosPerfect = (_timeBar.GetComponent<RectTransform>().rect.width * 80)/100 - 25;
        star_good.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, starPosGood, star_good.GetComponent<RectTransform>().rect.width);
        star_great.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, starPosGreat, star_good.GetComponent<RectTransform>().rect.width);
        star_perfect.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, starPosPerfect, star_good.GetComponent<RectTransform>().rect.width);

        if(_timeBar.value < _timeBar.maxValue * 80 / 100 && _timeBar.value > _timeBar.maxValue * 80 / 100 - 1f){
            A.DownTheStar(2);
        }
        if(_timeBar.value < _timeBar.maxValue * 40 / 100 && _timeBar.value > _timeBar.maxValue * 40 / 100 - 1f){
            A.DownTheStar(1);
        }
        if(_timeBar.value < _timeBar.maxValue * 5 / 100 && _timeBar.value > _timeBar.maxValue * 5 / 100 - 1f){
            A.DownTheStar(0);
        }
        if(_timeBar.value <= 0){
            _isLost = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("START");
        if(!_isLost && !_isWon){
            A.MoveManagement();
            A.DisplayProcessTree();
            RunTimeOut();
            BindingToSlider();
        }
        
        if(_isLost){
            LoseLabel.gameObject.SetActive(true);
        }

        if(_isWon){
            WinLabel.gameObject.SetActive(true);
        }
    }
}
