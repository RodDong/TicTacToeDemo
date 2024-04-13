using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum PlayerSide
    {
        Cross,
        Circle
    }
    public bool isGameOver {get; private set;}
    bool CrossPlayerWon = false;
    bool CirclePlayerWon = false;
    
    public PlayerSide CurPlayerSide;
    [SerializeField] public PlayerSide StartSide;
    [SerializeField] TMP_Text GameOverText;
    [SerializeField] Button RestartButton;
    [SerializeField] string WinText, LoseText, TieText;
    [SerializeField] GameObject EscapeMenu;

    public static GameManager instance = null;

    SlotManager[] slotManagers = new SlotManager[9];

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        isGameOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurPlayerSide = StartSide;
        this.slotManagers = FindObjectOfType<PlayerControl>().slotManagers;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            RestartButton.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !EscapeMenu.activeSelf)
        {
            EscapeMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && EscapeMenu.activeSelf)
        {
            EscapeMenu.SetActive(false);
        }
    }

    public void SwitchSide()
    {
        if(CurPlayerSide == PlayerSide.Cross)
        {
            CurPlayerSide = PlayerSide.Circle;
        }
        else if (CurPlayerSide == PlayerSide.Circle)
        {
            CurPlayerSide = PlayerSide.Cross;
        }
    }

    public void Win()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;
        if (CurPlayerSide == PlayerSide.Circle)
        {
            CirclePlayerWon = true;
            Debug.Log(PlayerSide.Circle + "Win");
        }
        else
        {
            CrossPlayerWon = true;
            Debug.Log(PlayerSide.Cross + "Win");
        }

        if(CurPlayerSide == StartSide)
        {
            GameOverText.text = WinText;
            GameOverText.gameObject.SetActive(true);
        }
        else
        {
            GameOverText.text = LoseText;
            GameOverText.gameObject.SetActive(true);
        }
    }

    public void Tie()
    {
        if (isGameOver)
        {
            return;
        }

        GameOverText.text = TieText;
        GameOverText.gameObject.SetActive(true);

        isGameOver = true;
        Debug.Log("Tie");
    }

    public void Restart()
    {
        RestartButton.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
        isGameOver = false;
        foreach(var slot in slotManagers)
        {
            slot.ResetSlot();
        }
        CurPlayerSide = StartSide;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
