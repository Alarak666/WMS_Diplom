using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WMS.UI.Shared
{
    //[Authorize]
    public class BasePage : ComponentBase
    {
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public string? UserName { get; set; }
        public bool IsAuth { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                IsAuth = true;
            }

        }
    }
}
