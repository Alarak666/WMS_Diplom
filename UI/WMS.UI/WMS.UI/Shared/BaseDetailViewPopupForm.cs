using Blazored.Toast.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using WMS.Core.Constants;
using WMS.Core.Constants.Enum;
using WMS.Core.Interface.ControllerInterface;
using WMS.Core.Models;
using WMS.Core.Services;
using WMS.UI.Services.UserMessages;

namespace WMS.UI.Shared;

public class BaseDetailViewPopupForm : ComponentBase, IDisposable
{
    [Inject] public IViewEventListener ViewEventListener { get; set; }
    [Inject] public IUserNotificationService UserNotificationService { get; set; }
    [Inject] public IToastService ToastService { get; set; }
    [Parameter] public EventCallback<bool> PopupClosed { get; set; }
    [Parameter] public EventCallback<ViewClosedEventArgs> ViewClosed { get; set; }
    [Parameter] public string? Width { get; set; }
    [Parameter] public bool IsVisible { get; set; }
    [Parameter] public Guid? SelectedItemId { get; set; }
    protected ValidationResult? ValidationResult;
    protected CancellationToken CancellationToken;

    protected bool IsModified { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        ViewEventListener.ViewUpdateRequest += ViewEventListener_ViewUpdateRequest;
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        ValidationResult = null;
        if (IsVisible)
        {
            await Load();
        }
    }

    protected virtual async Task Load()
    {
    }

    protected virtual async Task<bool> ValidateSave()
    {
        return true;
    }

    protected virtual async Task<bool> ValidatePost()
    {
        return true;
    }
    protected async Task ValidateObjectFailed()
    {
        foreach (var error in ValidationResult.Errors)
        {
            UserNotificationService.AddMessage(new UserMessage()
            {
                Message = error.ErrorMessage,
                Type = UserMessageType.Error
            });
        }

        ToastService.ShowError("Validation error!");
    }
    protected bool? IsFieldValid(string propertyName)
    {
        if (ValidationResult == null) return null;

        var propertyFailure = ValidationResult?.Errors?.FirstOrDefault(x => x.PropertyName == propertyName);
        return propertyFailure == null;
    }

    protected virtual async Task Save()
    {

    }

    protected virtual async Task<bool> CanBeSaved()
    {
        return true;
    }


    protected virtual async Task Cancel()
    {

    }

    protected virtual async Task<Guid?> GetModelId()
    {
        return SelectedItemId;
    }

    protected async Task SaveObject()
    {
        var canBeSaved = await CanBeSaved();
        if (!canBeSaved) return;
        try
        {
            await Save();
            IsModified = false;
            ToastService.ShowSuccess("Object saved successfully!");
        }
        catch (Exception ex)
        {
            ToastService.ShowError("Object saved failure!");
        }
    }

    private async Task CloseForm(bool saved)
    {
        await ViewClosed.InvokeAsync(new ViewClosedEventArgs()
        {
            Saved = saved, // Используйте значение параметра saved
            ObjectId = await GetModelId()
        });

        if (saved)
        {
            // Сброс выбранной строки
            SelectedItemId = null;
        }

        IsVisible = false;
    }
    protected virtual async Task Close()
    {
        await CloseForm(false);
        await PopupClosed.InvokeAsync(false);
    }

    protected async Task CloseObject()
    {
        await Close();
    }

    private async Task ViewEventListener_ViewUpdateRequest(object source, string eventId)
    {
        if (eventId == ViewEvents.ViewEventDocumentPosted)
        {
            await Load();
        }
        else if (eventId == ViewEvents.PropertyChanged)
        {
            IsModified = true;
        }
    }

    public void Dispose()
    {
        ViewEventListener.ViewUpdateRequest -= ViewEventListener_ViewUpdateRequest;
    }
}