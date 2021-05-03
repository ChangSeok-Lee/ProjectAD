using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageUIManager : MonoBehaviour
{
    private float X = 0;
    private int chapterNum = 4;//스크롤 뷰의 컨텐츠를 늘렸을 때 함께 증가 시켜줘야함

    private List<List<Image>> chapter_stage_image = new List<List<Image>>();
    public int chapterPointer = 1;
    public int stagePointer = 1;
    private int clearedChapter = GameDataManager.gameData.clearedChapter;
    private int clearedStage = GameDataManager.gameData.clearedStage;
    public RectTransform content;

    private GameObject leftArrow_chapter, rightArrow_chapter, leftArrow_stage, rightArrow_stage;

    private int[] stage = SaveLoadManager.Instance.stage;

    private bool IsScroll;
    // Start is called before the first frame update
    void Awake()
    {
        content = transform.Find("BackGround/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        content.localPosition = new Vector2(0, 0);
        init();
    }



    void init()
    {
        string path = "BackGround/Scroll View";
        IsScroll = false;
        //리스트에 각 스테이지의 이미지 객체를 저장(현재 만든 챕터가 4까지라서 4까지만 돌림)
        for (int j = 0; j < 4; j++)
        {
            List<Image> temp = new List<Image>();
            for (int i = 1; i <= stage[j]; i++)
            {
                temp.Add(transform.Find(path + "/Viewport/Content/Chapter" + (j + 1) + "/Stage" + i).GetComponent<Image>());
                if (clearedChapter > (j + 1) ||
                    clearedChapter == (j + 1) && clearedStage >= i)
                    transform.Find(path + "/Viewport/Content/Chapter" + (j + 1) + "/Stage" + i + "/LockedImage").gameObject.SetActive(false);
            }
            chapter_stage_image.Add(temp);
        }

        leftArrow_chapter = transform.Find("BackGround/Scroll View/ChapterLeft").gameObject;
        rightArrow_chapter = transform.Find("BackGround/Scroll View/ChapterRight").gameObject;
        leftArrow_stage = transform.Find("BackGround/Scroll View/StageLeft").gameObject;
        rightArrow_stage = transform.Find("BackGround/Scroll View/StageRight").gameObject;

    }


    // Update is called once per frame
    void Update()
    {
        KeyCheck();

    }

    void KeyCheck()
    {
        if (clearedChapter >= chapterPointer)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameDataManager.gameData.chapter = chapterPointer;
                GameDataManager.gameData.stage = stagePointer;
                SceneChangeManager.Instance.SceneChange("0" + chapterPointer);
            }
        }

        if (!IsScroll) {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && (X != 0))
            {
                IsScroll = true;
                X += 900;
                StartCoroutine(Scroll(X));

                //content.localPosition = new Vector2(X, 0);
                chapterPointer--;
                stagePointer = 1;
                UpdateUI();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && X > (-900 * (chapterNum - 1)))
            {
                IsScroll = true;
                X -= 900;
                StartCoroutine(Scroll(X));
                //content.localPosition = new Vector2(X, 0);
                chapterPointer++;
                stagePointer = 1;
                UpdateUI();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.A) && (stagePointer != 1))
        {
            Debug.Log("Input A");
            stagePointer--;
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.D) && (stagePointer != chapter_stage_image[chapterPointer - 1].Count)
            && ((clearedChapter > chapterPointer) ||
            (clearedChapter == chapterPointer && clearedStage > stagePointer)))
        {
            stagePointer++;
            UpdateUI();
        }
    }


    /// <summary>
    /// 현재 챕터에 따라 오브젝트를 찾고, 스테이지 정보에 따라 활성화 된 부분 이미지 변경
    /// </summary>
    void UpdateUI()
    {
        //현재 포인터에 따른 stage의 색상 변경(나중에 이미지 변경으로 재구현 가능성 있음)
        for (int i = 0; i < chapter_stage_image[chapterPointer - 1].Count; i++)
        {
            if (stagePointer == (i + 1))
                chapter_stage_image[chapterPointer - 1][i].color = new Color(255, 0, 0, 255);
            else
                chapter_stage_image[chapterPointer - 1][i].color = new Color(0, 0, 0, 255);
        }

        //화살표 표시와 백그라운드 이미지 로드는 모든 스테이지가 완성되고 구현 방식을 변경

        //화살표 표시 결정
        if (chapterPointer != 1)
            leftArrow_chapter.SetActive(true);
        else
            leftArrow_chapter.SetActive(false);

        if (chapterPointer != 4)
            rightArrow_chapter.SetActive(true);
        else
            rightArrow_chapter.SetActive(false);


        if (clearedChapter >= chapterPointer)
        {
            if (stagePointer != 1)
                leftArrow_stage.gameObject.SetActive(true);
            else
                leftArrow_stage.SetActive(false);

            if (stagePointer != chapter_stage_image[chapterPointer - 1].Count)
                rightArrow_stage.SetActive(true);
            else
                rightArrow_stage.SetActive(false);
        }
        else {
            leftArrow_stage.SetActive(false);
            rightArrow_stage.SetActive(false);
        }



        //백그라운드 이미지 로드

        switch (chapterPointer) {
            case 1:
                transform.Find("BackGround").GetComponent<Image>().sprite = Resources.Load("04.Image/BackGround/1chapter_1122_ui", typeof(Sprite)) as Sprite;
                break;
            case 2:
                transform.Find("BackGround").GetComponent<Image>().sprite = Resources.Load("04.Image/BackGround/2-Chapter_ui", typeof(Sprite)) as Sprite;
                break;
            case 3:
                transform.Find("BackGround").GetComponent<Image>().sprite = Resources.Load("04.Image/BackGround/3-Chapter_1122", typeof(Sprite)) as Sprite;
                break;
            case 4:
                transform.Find("BackGround").GetComponent<Image>().sprite = Resources.Load("04.Image/BackGround/4-Chapter", typeof(Sprite)) as Sprite;
                break;
        }
        //클리어 되지 않은 부분은 비활성화 시켜야함

    }
    IEnumerator Scroll(float position)
    {
        while (IsScroll)
        {
            content.localPosition = Vector2.Lerp(content.localPosition, new Vector2(position, 0), Time.deltaTime * 13);
            if (Vector2.Distance(content.localPosition, new Vector2(position, 0)) < 0.05f)
            {
                IsScroll = false;
            }
            yield return null;
        }
    }
}

