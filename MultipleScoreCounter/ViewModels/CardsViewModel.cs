using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.CompilerServices;
using MultipleScoreCounter.Views;
using ReactiveUI;

namespace MultipleScoreCounter.ViewModels
{
    public sealed class CardsViewModel : ViewModelBase
    {
        private CardViewModel? _selectedCard;

        public event PropertyChangedEventHandler? PropertyChangedEvent;
        public ObservableCollection<CardViewModel> SearchResults { get; } = new();

        public CardViewModel? SelectedCard
        {
            get => _selectedCard;
            set => this.RaiseAndSetIfChanged(ref _selectedCard, value);
        }
        
        public ReactiveCommand<Unit, CardViewModel?> SelectCardCommand { get; }
        
        public CardsViewModel()
        {
            SearchResults.Add(new CardViewModel());
            SearchResults.Add(new CardViewModel());
            SearchResults.Add(new CardViewModel());
            
            OnPropertyChanged("SearchResults");

            SelectCardCommand = ReactiveCommand.Create(() =>
            {
                return SelectedCard;
            });
        }
        
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}