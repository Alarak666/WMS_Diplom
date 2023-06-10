using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Constants.Enum;
using WMS.Core.Interface.ControllerInterface;

namespace WMS.UI.Shared
{
    public partial class Footer
    {
        [Inject] public IUserNotificationService UserNotificationService { get; set; }

        private int _height = 50;
        private bool _show;
        private string _searchText;
        private UserMessageType? _messageTypeFilter;
        private string ButtonText => (_show != true) ? "Show" : "Close";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                DoFilter();
            }
        }

        private ICollection<UserMessage> _messages = new List<UserMessage>();
        private ICollection<UserMessage> _filteredMessages = new List<UserMessage>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            UserNotificationService.OnMessageAdded += UserMessageServiceOnOnMessageAdded;
            DoFilter();
        }

        private async Task UserMessageServiceOnOnMessageAdded(UserMessage msg)
        {
            _messages = UserNotificationService.GetMessages();
            DoFilter();
            // if (!_show)
            // {
            //     await InvokeAsync(HandleClickShow);
            // }
            // else
            // {
                await InvokeAsync(StateHasChanged);
            // }
        }

        private void HandleClickShow()
        {
            _show = !_show;

            if (_show)
            {
                _height = 300;
            }
            else
            {
                _height = 50;
            }

            StateHasChanged();
        }

        private void DoFilter()
        {
            _filteredMessages = _messages.ToList();
            if (!string.IsNullOrWhiteSpace(_searchText))
            {
                _filteredMessages = _filteredMessages.Where(x => x.Message.Contains(_searchText)).ToList();
            }
            if (_messageTypeFilter != null)
            {
                _filteredMessages = _filteredMessages.Where(x => x.Type == _messageTypeFilter).ToList();
            }
        }

        private void HandleClickFilter(UserMessageType messageType)
        {
            if (!_show)
            {
                HandleClickShow();
            } 
            
            if (_messageTypeFilter != messageType)
            {
                _messageTypeFilter = messageType;
                DoFilter();
                return;
            }

            _messageTypeFilter = null;
            DoFilter();
        }

        private async Task HandleClickClear()
        {
            _messages.Clear();
            DoFilter();
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            UserNotificationService.OnMessageAdded -= UserMessageServiceOnOnMessageAdded;
        }
    }
}
