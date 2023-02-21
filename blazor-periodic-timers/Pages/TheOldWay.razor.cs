using Microsoft.AspNetCore.Components;

namespace blazor_periodic_timers.Pages
{
    public class TheOldWayBase : ComponentBase, IDisposable
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

        private static readonly System.Timers.Timer _timer = new System.Timers.Timer(1000);
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _timer.Elapsed += Timer_Elapsed;
                _timer.Enabled = true;
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            GreetingIndex = (GreetingIndex + 1) % Greetings.Length;
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
