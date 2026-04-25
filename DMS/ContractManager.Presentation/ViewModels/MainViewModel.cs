using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;

namespace ContractManager.Presentation.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IContractService _contractService;

        [ObservableProperty]
        private ObservableCollection<Contract> _contracts = new();

        [ObservableProperty]
        private string _newContractTitle = string.Empty;

        [ObservableProperty]
        private string _newContractNumber = string.Empty;

        public MainViewModel(IContractService contractService)
        {
            _contractService = contractService;
            LoadContractsCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadContractsAsync()
        {
            var data = await _contractService.GetAllAsync();
            Contracts = new ObservableCollection<Contract>(data);
        }

        [RelayCommand]
        private async Task AddContractAsync()
        {
            if (string.IsNullOrWhiteSpace(NewContractTitle) || string.IsNullOrWhiteSpace(NewContractNumber))
                return;

            var contract = new Contract
            {
                Title = NewContractTitle,
                ContractNumber = NewContractNumber,
                CreatedAt = DateTime.Now
            };

            await _contractService.AddAsync(contract);

            // پاک کردن فرم و بروزرسانی لیست
            NewContractTitle = string.Empty;
            NewContractNumber = string.Empty;
            await LoadContractsAsync();
        }
    }
}
