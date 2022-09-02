using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace BlazorInputMask;

public partial class InputMask : InputBase<string>
{
    [Inject] public IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter]
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public string? RawValue { get; set; }
    [Parameter] public EventCallback<string?> RawValueChanged { get; set; }
    [Parameter] public string? DataMask { get; set; }

    private IJSObjectReference? _module;


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _module ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/PPioli.BlazorInputMask/blazorinputmask.js");
            await _module.InvokeVoidAsync("ApplyMaskToElement", Id, DataMask ?? "", DotNetObjectReference.Create(this));
        }
    }

    protected override bool TryParseValueFromString(string? value, out string result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value ?? string.Empty;
        validationErrorMessage = null;
        return true;
    }

    [JSInvokable]
    public async Task ReturnUnmaskedValue(string value)
    {
        RawValue = value;
        await RawValueChanged.InvokeAsync(value);
    }
}
