using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace DragonCoreBlazorGames;

//public class Square
//{
//    public int Id { get; set; }
//    public string Image { get; set; }
//    public bool IsFlipped { get; set; }
//    public bool IsRemoved { get; set; }

//    public void Remove()
//    {
//        IsRemoved = true;
//    }

//    public void Reset()
//    {
//        IsFlipped = false;
//        IsRemoved = false;
//    }
//}


public class Square : ComponentBase
{
    [Parameter]
    public string Image { get; set; }
    [Parameter]
    public EventCallback OnClick { get; set; }

    [Parameter]
    public EventCallback OnRemove { get; set; }

    private bool isFlipped = false;
    private bool isRemoved = false;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        int sequence = 0;

        builder.OpenElement(sequence++, "div");
        builder.AddAttribute(sequence++, "class", "square");

        if (!isRemoved)
        {
            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", "flip-container");
            builder.AddAttribute(sequence++, "onclick", EventCallback.Factory.Create(this, HandleClick));

            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", "flipper");

            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", $"front {(isFlipped ? "flipped" : "")}");
            builder.CloseElement();

            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", $"back {(isFlipped ? "" : "flipped")}");
            builder.AddAttribute(sequence++, "style", $"background-image: url('{Image}')");
            builder.CloseElement();

            builder.CloseElement();
            builder.CloseElement();
        }

        builder.CloseElement();
    }

    private async Task HandleClick()
    {
        if (!isFlipped && !isRemoved)
        {
            isFlipped = true;
            StateHasChanged();

            await OnClick.InvokeAsync();

            if (isRemoved)
            {
                await OnRemove.InvokeAsync();
            }
        }
    }

    public void Reset()
    {
        isFlipped = false;
        isRemoved = false;
    }

    public void Remove()
    {
        isRemoved = true;
        StateHasChanged();
    }
}