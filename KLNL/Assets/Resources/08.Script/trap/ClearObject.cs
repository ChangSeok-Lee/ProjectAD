using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObject : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //클리어 관련 연출


        //현재 스테이지 프리팹 언로드


        //데이터 메니저를 통해 현재 데이터 정보 변경
        GameDataManager.gameData.stage++;
        if (GameDataManager.gameData.stage > GameDataManager.gameData.clearedStage)
            GameDataManager.gameData.clearedStage++;



        //다음 맵으로 전환(게임 데이터를 불러오고 맵을 생성시킬 객체가 필요)

        
        
        
        
        
        //or 전체를 하나의 함수로 만들고 정보만 전달시키기
    }
}
