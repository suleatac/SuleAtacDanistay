using App.Bus.Publisher;
using MassTransit;
using Microsoft.Extensions.FileProviders;

namespace App.UI.Consumer
{
    public class ConsumerDocument(IServiceProvider serviceProvider) : IConsumer<PublishDocument>
    {
        public async Task Consume(ConsumeContext<PublishDocument> context)
        {
            var message = context.Message;

        }
    }
}
