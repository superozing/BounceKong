﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;   // Pause UI 전체
    public GameObject clearUI;       // ClearCanvas 오브젝트 (Inspector에서 연결)

    public Button resumeButton;
    public Button exitButton;

    // 🔹 처음부터 버튼 추가
    public Button restartFromBeginningButton;

    void Start()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExitToStageSelection);

        // 🔹 처음부터 버튼 클릭 시 현재 스테이지 다시 로드
        restartFromBeginningButton.onClick.AddListener(RestartStageFromBeginning);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ✅ 1. ClearUI 떠 있으면 ESC 무시
            if (GameManager.IsStageCleared &&
                clearUI != null &&
                clearUI.activeInHierarchy)
            {
                Debug.Log("❌ ClearUI 떠 있어서 ESC 무시됨");
                return;
            }

            // ✅ 2. GameOver 상태면 ESC 무시
            if (GameOverUIScript.IsGameOver)
            {
                Debug.Log("❌ GameOver 상태에서 ESC 무시됨");
                return;
            }

            // ✅ 3. ESC로 PauseUI 토글
            if (pauseMenuUI != null && !pauseMenuUI.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }

    public void ExitToStageSelection()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StageSelection");
    }

    // 🔹 현재 스테이지 처음부터 다시 시작
    public void RestartStageFromBeginning()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
