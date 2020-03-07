using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FrienddOrganizer.UI.Services
{
    public class MessageDialogueService : IMessageDialogueService
    {
        public MessageDialogueStatus SelectOKCancelMessageBox(string text,string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.Cancel ? MessageDialogueStatus.Cancel : MessageDialogueStatus.Ok;
        }
    }
    public enum MessageDialogueStatus
    {
        Ok = 0,
        Cancel
    }
}
