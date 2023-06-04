using Blazored.Toast.Services;
using DevExpress.Blazor;
using WMS.Core.Constants;
using WMS.Core.Services;
using WMS.Core.Services.UserMessages;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WMS.UI.Shared
{
    [Authorize]
    public class BaseListPage : BasePage
    {
        [Inject] public IViewEventListener ViewEventListener { get; set; }
        [Inject] public IUserNotificationService UserNotificationService { get; set; }
        [Inject] public IToastService ToastService { get; set; }

        [Inject] public IJSRuntime JsRuntime { get; set; }
        protected DxGrid? _dataGrid;
        protected Guid? _selectedItemId;
        protected string _searchText = string.Empty;
        protected bool _isLoading;
        protected bool _isFailure;
        protected string _errorMessage = string.Empty;
        protected CancellationToken cancellationToken;
        protected ValidationResult? ValidationResult;

        protected override async Task OnInitializedAsync()
        {
            ViewEventListener.ViewUpdateRequest += ViewEventListener_ViewUpdateRequest;
            await RefreshData();
        }

        private async Task ViewEventListener_ViewUpdateRequest(object source, string eventId)
        {
            if (eventId == ViewEvents.ViewEventDocumentPosted)
            {
                await RefreshData();
            }
        }

        private async Task RefreshData()
        {
            try
            {
                _isLoading = true;
                _isFailure = false;
                _errorMessage = string.Empty;
                await LoadData();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                _isFailure = true;
            }
            finally
            {
                _isLoading = false;
            }
        }

        protected virtual async Task LoadData()
        {
        }

        protected virtual async Task Save()
        {
        }

        protected virtual async Task<bool> CanBeSaved()
        {
            return true;
        }

        protected virtual async Task Post()
        {

        }

        protected virtual async Task Cancel()
        {

        }

        protected virtual async Task<bool> Validate()
        {
            return true;
        }


        private async Task<bool> ValidateObject()
        {
            var result = await Validate();
            if (result) return result;

            UserNotificationService.AddMessage(new UserMessage()
            {
                Message = "Validation error!",
                Type = UserMessageType.Error
            });
            foreach (var error in ValidationResult.Errors)
            {
                UserNotificationService.AddMessage(new UserMessage()
                {
                    Message = error.ErrorMessage,
                    Type = UserMessageType.Error
                });
            }

            ToastService.ShowError("Validation error!");

            return result;
        }

        protected async Task SaveObject()
        {
            var canBeSaved = await CanBeSaved();
            if (!canBeSaved) return;

            var validated = await ValidateObject();
            if (validated)
            {
                await Save();
            }
        }

        protected async Task PostObject()
        {
            var canBeSaved = await CanBeSaved();
            if (!canBeSaved) return;

            var validated = await ValidateObject();
            if (!validated)
            {
                return;
            }

            try
            {
                await Save();
                await Post();
                ToastService.ShowSuccess("Document posted successfully!");
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Document posted failure!");
            }
        }

        protected virtual async Task HandleSearchTextChanged(string newSearchText)
        {
            _searchText = newSearchText;
            await RefreshData();
        }

        protected virtual void HandleClickColumnChooser()
        {
            _dataGrid?.ShowColumnChooser(".column-chooser-button");
        }

        public void Dispose()
        {
            ViewEventListener.ViewUpdateRequest -= ViewEventListener_ViewUpdateRequest;
        }

        protected virtual async Task HandleShowTransactions()
        {
            await JsRuntime.InvokeVoidAsync("open", $"/transactions/{_selectedItemId}", "_blank");
        }
    }
}