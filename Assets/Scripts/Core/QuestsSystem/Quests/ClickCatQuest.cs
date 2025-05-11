namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class ClickCatQuest : Quest
    {
        protected override void Subscribe()
        {
            EventManager.OnCatClick.AddListener(ClickCat);
        }
        protected override void Unsubscribe()
        {
            EventManager.OnCatClick.RemoveListener(ClickCat);
        }

        private void ClickCat()
        {
            UpdateProgress(1);
        }
    }
}
