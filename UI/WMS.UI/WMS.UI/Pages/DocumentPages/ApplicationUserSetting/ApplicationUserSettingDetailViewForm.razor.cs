using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.IdentityModels;
using WMS.Core.Models.DocumentModels.Positions;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.ApplicationUserSetting
{
    public partial class ApplicationUserSettingDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IApplicationUserSettingService ApplicationUserSettingService { get; set; }
        [Inject] public IApplicationUserService ApplicationUserService { get; set; }
        [Inject] public IPositionService PositionService { get; set; }


        #region Form

        private ApplicationUserSettingDetailViewModel? Model { get; set; } = new ApplicationUserSettingDetailViewModel();
      
        private ApplicationUserListViewModel? ApplicationUserListViewModel { get; set; } = new();
        private IEnumerable<ApplicationUserListViewModel>? ApplicationUserListViewModels { get; set; } = new List<ApplicationUserListViewModel>();
       
        private PositionListViewModel? PositionListViewModel { get; set; } = new();
        private IEnumerable<PositionListViewModel>? PositionListViewModels { get; set; } = new List<PositionListViewModel>();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            await LoadListViewModel();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await ApplicationUserSettingService.GetDetailViewData(SelectedItemId, CancellationToken);
            ApplicationUserListViewModel = ApplicationUserListViewModels?.FirstOrDefault(x => x.Id == Model?.ApplicationUserId);
            PositionListViewModel = PositionListViewModels?.FirstOrDefault(x => x.Id == Model?.PositionId);


        }

        private async Task LoadListViewModel()
        {
            PositionListViewModels = await PositionService.GetListViewItems("", CancellationToken);
            ApplicationUserListViewModels = await ApplicationUserService.GetListViewItems("", CancellationToken);
        }

        private async Task UpdateModel()
        {
            if (ApplicationUserListViewModel.Id != Guid.Empty)
                Model.ApplicationUserId = ApplicationUserListViewModel.Id;
            if (PositionListViewModel.Id != Guid.Empty)
                Model.PositionId = PositionListViewModel.Id;
        }
        protected override async Task Save()
        {
            await UpdateModel();
            if (SelectedItemId != null)
                await ApplicationUserSettingService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await ApplicationUserSettingService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
