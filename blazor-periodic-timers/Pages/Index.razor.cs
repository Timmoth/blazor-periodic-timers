using Microsoft.AspNetCore.Components;

namespace blazor_periodic_timers.Pages
{
    public class IndexBase : ComponentBase, IDisposable
    {
        public static readonly string[] Greetings = new string[] {
           "Hello",
           "Hi",
           "Hey there",
           "Good morning",
           "Good afternoon",
           "Good evening",
           "Howdy",
           "What's up",
           "Yo",
           "Greetings",
           "Salutations",
           "Nice to see you",
           "How are you doing",
           "How's it going",
           "What's new",
           "Long time no see",
           "Pleasure to meet you",
           "Hey, how's everything",
           "Hi there",
           "Lovely day, isn't it"
        };

        public int GreetingIndex { get; private set; }

        private readonly CancellationTokenSource _timerCancellationToken = new();
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
                while (!_timerCancellationToken.IsCancellationRequested && await timer.WaitForNextTickAsync())
                {
                    GreetingIndex = (GreetingIndex + 1) % Greetings.Length;
                    await InvokeAsync(StateHasChanged);
                }
                // Make sure base.OnAfterRenderAsync is not called after the component is disposed
                return;
            }

            await base.OnAfterRenderAsync(firstRender);
        }
        public void Dispose()
        {
            _timerCancellationToken.Cancel();
            _timerCancellationToken.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
