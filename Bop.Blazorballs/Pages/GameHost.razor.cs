using System.Diagnostics;
using Bop.Blazorballs.Code;
using Microsoft.JSInterop;

namespace Bop.Blazorballs.Pages;

public partial class GameHost
{
    private Game? _game;

    DotNetObjectReference<GameHost>? _objectReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        Debug.WriteLine("OnAfterRenderAsync enter");
        
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            Debug.WriteLine("OnAfterRenderAsync firstRender");
            _objectReference = DotNetObjectReference.Create(this);
            
            _game = new Game();
            _game.Init();
            StateHasChanged();

            await JsRuntime.InvokeVoidAsync("Blazorballs.startGameLoop", _objectReference);
        }
    }
    
    [JSInvokable("TickGame")]
    public async Task TickGame()
    {
        await _game!.Tick();
        StateHasChanged();
    }

    public void Dispose()
    {
        Debug.WriteLine("GameHost Dispose");
        GC.SuppressFinalize(this);
        _objectReference?.Dispose();
    }
}