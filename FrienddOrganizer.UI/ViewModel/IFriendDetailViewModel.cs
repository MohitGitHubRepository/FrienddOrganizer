using System.Threading.Tasks;

namespace FrienddOrganizer.UI.ViewModel
{
    public interface IFriendDetailViewModel
    {
        Task LoadAsync(int Id);
        bool HasChanges { get; }
    }
}