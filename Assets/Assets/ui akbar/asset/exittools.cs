using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exittools : MonoBehaviour
{
    public string mainMenuSceneName = "main menu"; // Nama scene main menu

    void Update()
    {
        // Cek jika tombol ESC ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Kembali ke main menu
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
