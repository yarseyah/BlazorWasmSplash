# Experiment with Blazor splash screen

Most of the examples I've seen of splash screens for Blazor are either invoked after the Blazor runtime has taken over the control of the browser, or uses hardcoded pixel locations. My desire is to have an image centred on the main display (regardless to resolution of the browser/device).

## Using background CSS style

* The `html` element is used to with a `background-image` fill the available screen space 
* The image itself is Base64 encoded within the `Index.html` page itself so that no other resources are loaded.

## Removing background when Blazor is loaded

Using the default Blazor template, the background image is completely overwritten by the content on large devices, but when the display collapses the navbar the background can be seen (thus on mobile devices, the background image remains visible after the application has loaded).  To overcome this, the `class` will be removed from the `html` element after loading, for completeness I've implemented this as a toggle: 

```js
export function toggleSplashClass() {
    if ( document.documentElement.classList.contains('splash') ) {
        document.documentElement.classList.remove('splash');
    }
    else {
        document.documentElement.classList.add('splash');
    }
}
```

The `MainLayout.razor` has had its `OnAfterInitialiseAsync` method overridden and invokes the JavaScript to manipulate the DOM on first render:

```cs
public partial class MainLayout
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await using var splashModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "/js/splash.js");
            await splashModule.InvokeVoidAsync("toggleSplashClass");

        }
    }
}
```

## Define splash style

In the `index.html` file, add `class="splash"` to the `html` element and the following `style` block, use of Url path is shown here, but the content can be Base64 inlined using the `url(data:image/webp;base64,...)` style of encoding

```html
<style>
    .splash {
        width: 100%;
        height: 100%;
        margin: auto;
        background-image: url('/spash.webp');
        background-position: center;
        background-repeat: no-repeat;
    }
</style>
```