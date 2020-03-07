namespace FrienddOrganizer.UI.Services
{
    public interface IMessageDialogueService
    {
       MessageDialogueStatus SelectOKCancelMessageBox(string text, string title);
    }
}