namespace BlazorWasmSplash.Shared;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public partial class MainLayout
{
    [Inject]
    private IJSRuntime JsRuntime { get; set; } = default!;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await using var splashModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/splash.js");
            await splashModule.InvokeVoidAsync("toggleSplashClass");

        }
    }
}