using System.Threading.Tasks;

namespace FrienddOrganizer.UI.ViewModel
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int? Id);
        bool HasChanges { get; }
    }
}