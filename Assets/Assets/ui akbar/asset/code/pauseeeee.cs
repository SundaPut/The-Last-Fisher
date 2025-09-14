using UnityEngine.SceneManagement;
using UnityEngine;

// 1. Nama kelas diubah menjadi 'PauseMenu' untuk mengikuti konvensi penamaan PascalCase.
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private bool isPaused = false;

    private void Update()
    {
        // 2. Menambahkan logika untuk membuka/menutup menu jeda dengan tombol 'Escape'.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        // 3. Menambahkan Time.timeScale = 0f untuk benar-benar menjeda permainan.
        Time.timeScale = 0f;
        isPaused = true;
    }

    // 4. Mengganti nama metode 'continue' menjadi 'Resume' karena 'continue' adalah kata kunci.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        // 5. Mengembalikan Time.timeScale ke 1f untuk melanjutkan permainan.
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Quit()
    {
        // Praktik yang baik untuk memastikan skala waktu kembali normal saat keluar dari scene.
        Time.timeScale = 1f;
        SceneManager.LoadScene("main menu");

    }
}
