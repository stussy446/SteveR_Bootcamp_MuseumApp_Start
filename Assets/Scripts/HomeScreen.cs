using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MuseumApp
{
    public class HomeScreen : MonoBehaviour
    {
        public GameObject loginButton;
        public GameObject deleteButton;
        public TMP_Text username;

        public RectTransform attractionEntriesParent;

        public AttractionEntryGraphics attractionPrefab;
        public List<AttractionConfig> attractions;
        public List<AttractionEntryGraphics> attractionEntries;

        public void Signup()
        {
            SceneManager.LoadScene("SignupPopup", LoadSceneMode.Additive);
        }

        public void LogOff()
        {
            // LogOff
            User.LogOff();

            Refresh();
        }

        public void Refresh()
        {
            SetupUsername();

            foreach (var attraction in attractionEntries)
                attraction.Refresh();
        }

        private void Awake()
        {
            attractionEntries = new List<AttractionEntryGraphics>(attractions.Count);
            foreach (var attraction in attractions)
            {
                var newAttraction = Instantiate(attractionPrefab, attractionEntriesParent);
                newAttraction.Setup(attraction);
                attractionEntries.Add(newAttraction);
            }

            SetupUsername();
        }

        private void SetupUsername()
        {
            if (!User.IsLoggedIn)
            {
                loginButton.SetActive(true);
                username.gameObject.SetActive(false);
                deleteButton.SetActive(false);
                return;
            }

            loginButton.SetActive(false);
            deleteButton.SetActive(true);
            username.gameObject.SetActive(true);

            username.text = User.LoggedInUsername;
        }

        /// <summary>
        /// Deletes current User from Usertable and all of their AttractionRating records,
        /// also logs off from the current session and reopens signup page
        /// </summary>
        public void DeleteUserRecords()
        {
            string currentUsername = User.LoggedInUsername;
            
            Debug.Log($"deleting {currentUsername} and their records...");
            Database.DeleteUserRatings(currentUsername);
            Debug.Log("Deletion complete, logging off...");
            User.LogOff();
            Database.DeleteUser(currentUsername);
            SceneManager.LoadScene("SignupPopup", LoadSceneMode.Additive);
        }
    }
}